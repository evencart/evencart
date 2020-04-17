using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cleanup
{
    class Program
    {
        private static string FilesToDelete = "DotEntity.*;DotLiquid.*;EvenCart.dll;EvenCart.Core.dll;EvenCart.Data.dll;EvenCart.Services.dll;EvenCart.Infrastructure.dll;*.pdb;*.dll.config;DotEntity.*;DryIoc.*;dotnet-bundle.dll;Newtonsoft.*;Microsoft.*;System.*;BraintreeHttp-Dotnet.dll;FluentScheduler.dll;FluentValidation.*;HtmlAgilityPack.dll;NUglify.dll;SixLabors.*;*.deps.json;DinkToPdf.dll;HtmlToPdf*.dll;MySqlConnector.dll;NuGet.Frameworks.dll;dotnet-*;Swashbuckle.*;NPOI.*;StackExchange.*;BouncyCastle.*;ICSharpCode.*";

        private static string DirectoriesToDelete = "App_Data;NativeLibs;it;Areas;runtimes";

        static void Main(string[] args)
        {
            var filesToPreserve = "";
            var filesToDelete = "";
            var directory = "";
            var directoriesToDelete = "";
            if (args.Length > 0)
                directory = args[0];

            if (args.Length > 1)
            {
                filesToPreserve = args[1];
            }

            if (args.Length > 2)
            {
                filesToDelete = args[2];
            }
            if (args.Length > 3)
            {
                directoriesToDelete = args[3];
            }

            filesToDelete = FilesToDelete + ";" + filesToDelete;

            var coreSoftwareFiles = ParseFilesStr(filesToDelete);
            var preservedFiles = ParseFilesStr(filesToPreserve);

            var directoryInfo = new DirectoryInfo(directory);
            foreach (var file in coreSoftwareFiles)
            {
                var files = directoryInfo.GetFiles(file);
                foreach (var fileInfo in files)
                {
                    if (preservedFiles.Contains(fileInfo.Name))
                    {
                        Console.WriteLine("Skipping " + fileInfo.FullName);
                        continue; //skip the files
                    }
                    File.Delete(fileInfo.FullName);
                    Console.WriteLine("Deleted  " + fileInfo.FullName);
                }
            }

            directoriesToDelete = DirectoriesToDelete + ";" + directoriesToDelete;
            var directories = ParseFilesStr(directoriesToDelete);
            Console.WriteLine("Found " + directories.Count + " dir(s) to delete");
            foreach (var dir in directories)
            {
                var dirInfo = new DirectoryInfo(Path.Combine(directory, dir));
                if (dirInfo.Exists)
                {
                    Console.WriteLine("Deleting directory " + dirInfo.FullName);
                    Directory.Delete(dirInfo.FullName, true);
                }
            }
        }

        private static IList<string> ParseFilesStr(string filesStr)
        {
            return filesStr.Split(';').Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
