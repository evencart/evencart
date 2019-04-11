using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Countries
{
    public class StateSearchModel : AdminSearchModel
    {
        public int CountryId { get; set; }
    }
}