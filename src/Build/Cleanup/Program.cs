using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cleanup
{
    class Program
    {
        private static string FilesToDelete = "DotEntity.*;DotLiquid.*;EvenCart.dll;EvenCart.Core.dll;EvenCart.Data.dll;EvenCart.Services.dll;EvenCart.Infrastructure.dll;*.pdb;*.dll.config;DotEntity.*;DryIoc.*;dotnet-bundle.dll;Newtonsoft.*;Microsoft.*;System.*;BraintreeHttp-Dotnet.dll;FluentScheduler.dll;FluentValidation.*;HtmlAgilityPack.dll;NUglify.dll;SixLabors.*;Source.dll;*.deps.json";

        static void Main(string[] args)
        {
            var filesToPreserve = "";
            var filesToDelete = "";
            var directory = "";
            
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
                        continue; //skip the files
                    File.Delete(fileInfo.FullName);
                }
            }

        }

        private static IList<string> ParseFilesStr(string filesStr)
        {
            return filesStr.Split(';').Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
