using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EvenCart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static void PluginDebuggerMain(string[] args)
        {
            CreateWebHostBuilder(args, true).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args, bool useIIS = false)
        {
            var builder = WebHost.CreateDefaultBuilder(args);
            if (useIIS)
                builder.UseIIS();
            else
                builder.UseKestrel();
            builder.UseStartup<Startup>();
            return builder;
        }
    }
}
