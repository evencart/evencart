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

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BundlerMinifier;
using EvenCart.Core;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Services.Extensions;
using EvenCart.Services.Logger;
using EvenCart.Services.Security;

namespace EvenCart.Infrastructure.Bundle
{
    public class BundleService : IBundleService
    {
        private readonly ILocalFileProvider _localFileProvider;
        private readonly ILogger _logger;
        private readonly ICryptographyService _cryptographyService;
        public BundleService(ILocalFileProvider localFileProvider, ILogger logger, ICryptographyService cryptographyService)
        {
            _localFileProvider = localFileProvider;
            _logger = logger;
            _cryptographyService = cryptographyService;
        }

        public string GenerateCssBundle(string[] inputFiles)
        {
            return GetBundle(inputFiles, "css");
        }

        public string GenerateJsBundle(string[] inputFiles)
        {
            return GetBundle(inputFiles, "js");
        }

        private void BundleMinifier_ErrorMinifyingFile(object sender, MinifyFileEventArgs e)
        {
            var errors = string.Join(Environment.NewLine, e.Result.Errors.Select(x => x.Message));
            var files = string.Join(Environment.NewLine, e.Result.Errors.Select(x => x.FileName));
            _logger.LogError<BundleService>(new Exception($"An error occurred while bundling {files}"), errors);
            e.Bundle.OutputFileName = null;
        }

        private string GetBundle(string[] inputFiles, string type)
        {
            var bundleFileProcessor = new BundleFileProcessor();
            var bundle = new BundlerMinifier.Bundle();
            //we wil store the file names with date of modification to keep track whether a new bundle should be generated or not
            //any minor modification will cause regeneration of the bundle
            //we do this because it is a performance intensive operation and the we should only do it when needed

            var distinctFiles = inputFiles.Distinct().ToList();
            var fileNamesWithDates = new List<string>();
            foreach (var inputFile in distinctFiles)
            {
                //the input file can either be 1. theme-css 2. plugin-css 3. admin-css 4. common-css
                //check them one by one
                var file = ServerHelper.MapPath("~/Content/Themes" + inputFile);
                if (!_localFileProvider.FileExists(file))
                {
                    //plugins
                    file = ServerHelper.MapPath("~/" + inputFile);
                    if (!_localFileProvider.FileExists(file))
                    {
                        //administration & common
                        file = ServerHelper.MapPath("~/" + inputFile, true);
                        if (!_localFileProvider.FileExists(file))
                        {
                            continue; //can't do anything...the file doesn't exist
                        }
                    }
                }
                bundle.InputFiles.Add(file);
                var modDate = _localFileProvider.GetLastModifiedDateTime(file);
                fileNamesWithDates.Add($"{file}:{modDate}");
            }

            var outputFilePart1 = GetOutputFileName(fileNamesWithDates);
            var outputFilePart2 = GetOutputFileName(distinctFiles);
            var outputFile = outputFilePart1 + "_" + outputFilePart2;
            var bundlesDirectory = ServerHelper.MapPath(ApplicationConfig.BundlesDirectory, true);
            var bundleFileName = bundlesDirectory + "/" + outputFile + $".min.{type}";
            bundle.OutputFileName = bundleFileName;
            //do we need to generate the bundle?
            if (!_localFileProvider.FileExists(bundleFileName))
            {
                //delete the existing bundles of these files
                try
                {
                    _localFileProvider.DeleteFiles(bundlesDirectory, $"*_{outputFilePart2}.min.{type}*");
                }
                catch
                {
                    //do nothing...the file must be locked...will try next time
                }
                bundle.FileName = bundleFileName + ".json";
                BundleMinifier.ErrorMinifyingFile += BundleMinifier_ErrorMinifyingFile;
                bundleFileProcessor.Process(bundle.FileName, new List<BundlerMinifier.Bundle>() { bundle });
            }
            //if operation succeeded, return the url, else null
            if (bundle.OutputFileName == null)
                return null;
            //also create a gzipped version as well
            using(var bundleFileStream = File.OpenRead(bundleFileName))
            using (var compressedFileStream = File.Create(bundleFileName + ".gz"))
            {
                using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                {
                    bundleFileStream.CopyTo(compressionStream);
                }
            }
            return bundle.OutputFileName == null ? null : ApplicationEngine.MapUrl(bundleFileName);

        }

        private string GetOutputFileName(IList<string> inputFiles)
        {
            var files = string.Join(",", inputFiles);
            var hashFile = _cryptographyService.GetMd5Hash(files);
            return hashFile;
        }
    }
}