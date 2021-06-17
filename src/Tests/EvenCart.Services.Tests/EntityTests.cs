using DotEntity;
using Genesis.Data;
using Genesis.Infrastructure.Types;
using NUnit.Framework;

namespace EvenCart.Services.Tests
{
    public abstract class EntityTests : BaseTest
    {
        [Test]
        public void All_Entities_Versioned_Succeeds()
        {
            //first get all the entities 
            var classTypes = TypeFinder.ClassesOfType<GenesisEntity>();
            foreach (var c in classTypes)
            {
                var entitySetType = typeof(EntitySet<>).MakeGenericType(c);
                var methodInfo = entitySetType.GetMethod("Count");
                methodInfo.Invoke(null, null);
            }

        }
    }
}