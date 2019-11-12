using EvenCart.Data.Entity.Shop;
using Ui.SearchPlus.Data;

namespace Ui.SearchPlus.Factories
{
    public interface ISearchTermModelFactory
    {
        AutoCompleteResultModel Create(Product product);

        AutoCompleteResultModel Create(SearchTerm searchTerm);
    }
}