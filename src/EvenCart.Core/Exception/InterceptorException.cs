using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Core.Exception
{
    public class InterceptorException : EvenCartException
    {
        public IActionResult OriginalResult { get; set; }
    }
}