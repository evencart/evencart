using EvenCart.Core.Data;

namespace Ui.SearchPlus.Data
{
    public class SearchTerm : FoundationEntity
    {
        public string Term { get; set; }

        public string TermCategory { get; set; }

        public int Score { get; set; }
    }
}