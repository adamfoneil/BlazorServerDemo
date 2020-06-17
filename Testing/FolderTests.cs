using BlazorServerDemo.Queries;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Testing
{
    [TestClass]
    public class FolderTests
    {
        [TestMethod]
        public void BuildFolderStructure()
        {
            var folders = new MyFolderTreeResult[]
            {
                new MyFolderTreeResult() { Id = -23, Name = "root", ParentId = 0 },
                // some random states
                new MyFolderTreeResult() { Id = 454, Name = "Wyoming", ParentId = -23 },
                new MyFolderTreeResult() { Id = 594, Name = "Virginia", ParentId = -23 },
                new MyFolderTreeResult() { Id = 996, Name = "Nebraska", ParentId = -23 },
                // random cities in WY
                new MyFolderTreeResult() { Id = 29, Name = "Buford", ParentId = 454 },
                new MyFolderTreeResult() { Id = 37, Name = "Lusk", ParentId = 454 },
                new MyFolderTreeResult() { Id = 927, Name = "Farson", ParentId = 454 },
                // random cities in VA
                new MyFolderTreeResult() { Id = 64, Name = "Bodtyon", ParentId = 594 },
                new MyFolderTreeResult() { Id = 68, Name = "Goshen", ParentId = 594 },
                new MyFolderTreeResult() { Id = 713, Name = "Wise", ParentId = 594 },
                // random cities in NE
                new MyFolderTreeResult() { Id = 436, Name = "Gothenburg", ParentId = 996 },
                new MyFolderTreeResult() { Id = 413, Name = "Wahoo", ParentId = 996 },
                new MyFolderTreeResult() { Id = 217, Name = "Neligh", ParentId = 996 }
            };

            var structure = FolderStructure.FromFolders(folders);

            var json = JsonConvert.SerializeObject(structure, Formatting.Indented);
            var expected = GetResourceText("Testing.Resources.FolderStructure.txt");
            Assert.IsTrue(json.Equals(expected));
        }

        private static string GetResourceText(string resourceName)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
