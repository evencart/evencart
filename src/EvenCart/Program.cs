#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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
