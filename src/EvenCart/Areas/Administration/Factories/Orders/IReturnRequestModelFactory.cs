using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Orders
{
    public interface IReturnRequestModelFactory : IModelFactory<ReturnRequest, ReturnRequestModel>
    {
        
    }
}