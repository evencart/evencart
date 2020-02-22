namespace EvenCart.Infrastructure.Mvc.Validator
{
    public interface IRequiresValidations<T>
    {
        void SetupValidationRules(ModelValidator<T> v);
    }
}