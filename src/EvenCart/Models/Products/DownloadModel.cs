using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Products
{
    public class DownloadModel : FoundationModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string DownloadUrl { get; set; }

        public string FileType { get; set; }

        public bool Published { get; set; }
    }
}