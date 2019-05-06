namespace EvenCart.Infrastructure.Mvc.ModelFactories
{
    public interface IModelFactory
    {

    }
    public interface IModelFactory<in TEntity, out TModel> : IModelFactory
    {
        TModel Create(TEntity entity);
    }
}