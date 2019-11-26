using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Areas.Administration.Extensions
{
    public static class HttpFileExtensions
    {
        public static async Task<byte[]> GetBytesAsync(this IFormFile formFile)
        {
            using (var stream = new MemoryStream())
            {
               await formFile.CopyToAsync(stream);
               return stream.ToArray();
            }
        }
    }
}