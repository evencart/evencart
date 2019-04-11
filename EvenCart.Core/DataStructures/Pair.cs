#region Author Information
// Pair.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion
namespace EvenCart.Core.DataStructures
{
    /// <summary>
    /// Represents a pair of two objects
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class Pair<T1, T2>
    {
        public T1 First { get; set; }

        public T2 Second { get; set; }
    }

    public class Pair
    {
        public static Pair<T1, T2> Create<T1, T2>(T1 first, T2 second)
        {
            return new Pair<T1, T2>()
            {
                First = first,
                Second = second
            };
        }
    }
}