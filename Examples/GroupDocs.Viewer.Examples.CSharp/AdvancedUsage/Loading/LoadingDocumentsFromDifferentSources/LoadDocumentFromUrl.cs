﻿using System;
using System.IO;
using System.Net;
using GroupDocs.Viewer.Options;

namespace GroupDocs.Viewer.Examples.CSharp.AdvancedUsage.Loading.LoadingDocumentsFromDifferentSources
{
    /// <summary>
    /// This example demonstrates how to download and render document.
    /// </summary>
    class LoadDocumentFromUrl
    {
        public static void Run()
        {
            string url = "https://cms.admin.containerize.com/templates/groupdocs/images/logos/groupdocs-logo.png";
            string outputDirectory = Utils.GetOutputDirectoryPath();
            string pageFilePathFormat = Path.Combine(outputDirectory, "page_{0}.html");

            using (Viewer viewer = new Viewer(() => GetRemoteFile(url)))
            {
                HtmlViewOptions options = HtmlViewOptions.ForEmbeddedResources(pageFilePathFormat);                
                viewer.View(options);
            }

            Console.WriteLine($"\nSource document rendered successfully.\nCheck output in {outputDirectory}.");
        }
                
        private static Stream GetRemoteFile(string url)
        {
            WebRequest request = WebRequest.Create(url);

            using (WebResponse response = request.GetResponse())
                return GetFileStream(response);
        }

        private static Stream GetFileStream(WebResponse response)
        {
            MemoryStream fileStream = new MemoryStream();

            using (Stream responseStream = response.GetResponseStream())
                responseStream.CopyTo(fileStream);

            fileStream.Position = 0;
            return fileStream;
        }
    }
}
