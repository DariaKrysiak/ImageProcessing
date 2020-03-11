using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageProcessingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingApplication.Model.Tests
{
    [TestClass()]
    public class ImageProcessingModelTests
    {
        private ImageProcessingModel imageProcessing;
        private Dictionary<string, string> fileNames;

        [TestInitialize()]
        public void Setup()
        {
            imageProcessing = new ImageProcessingModel();
            fileNames = new Dictionary<string, string>()
            {
                { "test.jpg", "test_converted.jpg" },
                { "test.png", "test_converted.png" },
                { "test.bmp", "test_converted.bmp" }
            };
        }

        [TestMethod()]
        [DeploymentItem(@"test_images")]
        public void ProcessImageTest()
        {
            foreach (KeyValuePair<string, string> fileName in fileNames) 
            {
                string convertedFileName = imageProcessing.ProcessImage(fileName.Key);
                
                Assert.AreEqual(fileName.Value, convertedFileName, false);
            }
        }

        [TestMethod()]
        [DeploymentItem(@"test_images")]
        public void ProcessImageDurationTimeSpanTest()
        {
            imageProcessing.ProcessImage(fileNames.First().Key);

            Assert.IsTrue(imageProcessing.durationTimeSpan.TotalMilliseconds > 0);
        }
    }
}
