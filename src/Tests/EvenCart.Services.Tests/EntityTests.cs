using DotEntity;
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure.Utils;
using NUnit.Framework;

namespace EvenCart.Services.Tests
{
    public abstract class EntityTests : BaseTest
    {
        [Test]
        public void All_Entities_Versioned_Succeeds()
        {
            //first get all the entities 
            var classTypes = TypeFinder.ClassesOfType<FoundationEntity>();
            foreach (var c in classTypes)
            {
                var entitySetType = typeof(EntitySet<>).MakeGenericType(c);
                var methodInfo = entitySetType.GetMethod("Count");
                methodInfo.Invoke(null, null);
            }

        }
    }
}