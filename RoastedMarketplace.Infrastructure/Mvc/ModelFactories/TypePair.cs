using System;
using System.Diagnostics;

namespace RoastedMarketplace.Infrastructure.Mvc.ModelFactories
{
    [DebuggerDisplay("{SourceType.Name}, {DestinationType.Name}")]
    public struct TypePair : IEquatable<TypePair>
    {
        public static bool operator ==(TypePair left, TypePair right) => left.Equals(right);
        public static bool operator !=(TypePair left, TypePair right) => !left.Equals(right);

        public TypePair(Type sourceType, Type destinationType)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
        }

        public static TypePair Create<TSource>(TSource source, Type sourceType, Type destinationType)
        {
            if (source != null)
            {
                sourceType = source.GetType();
            }
            return new TypePair(sourceType, destinationType);
        }

        public static TypePair Create(Type sourceType, Type destinationType)
        {
            return new TypePair(sourceType, destinationType);
        }

        public Type SourceType { get; }

        public Type DestinationType { get; }

        public bool Equals(TypePair other) => SourceType == other.SourceType && DestinationType == other.DestinationType;

        public override bool Equals(object other) => other is TypePair && Equals((TypePair)other);

        public override int GetHashCode() => HashCodeCombiner.Combine(SourceType, DestinationType);

        public TypePair CloseGenericTypes(TypePair closedTypes)
        {
            var sourceArguments = closedTypes.SourceType.GetGenericArguments();
            var destinationArguments = closedTypes.DestinationType.GetGenericArguments();
            if (sourceArguments.Length == 0)
            {
                sourceArguments = destinationArguments;
            }
            else if (destinationArguments.Length == 0)
            {
                destinationArguments = sourceArguments;
            }
            var closedSourceType = SourceType.IsGenericTypeDefinition ? SourceType.MakeGenericType(sourceArguments) : SourceType;
            var closedDestinationType = DestinationType.IsGenericTypeDefinition ? DestinationType.MakeGenericType(destinationArguments) : DestinationType;
            return new TypePair(closedSourceType, closedDestinationType);
        }
    }

    public static class HashCodeCombiner
    {
        public static int Combine<T1, T2>(T1 obj1, T2 obj2) =>
            CombineCodes(obj1.GetHashCode(), obj2.GetHashCode());

        public static int CombineCodes(int h1, int h2) => ((h1 << 5) + h1) ^ h2;
    }
}