namespace EvenCart.Infrastructure.Mvc.UI
{
    public interface IMenuBuilder
    {
        Menu BuildMenu();

        void Clear();

        void Add(MenuItem menuItem);
    }
}