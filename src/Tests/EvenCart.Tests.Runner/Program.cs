using EvenCart.Common.Tests;
using EvenCart.Services.Tests;
using NUnitLite;

namespace EvenCart.Tests.Runner
{
    public class Program
    {
        static void Main(string[] args)
        {
            new AutoRun(typeof(BaseTest).Assembly).Execute(args);
            new AutoRun(typeof(StringTests).Assembly).Execute(args);
        }
    }
}