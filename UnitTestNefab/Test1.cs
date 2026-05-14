using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEFAB.Domains;
using NEFAB.Services;
using System;

namespace UnitTestNefab
{
    [TestClass]
    public sealed class PackageServiceTests
    {
        [TestMethod]
        public void AddPackage_WhenContainerNoIsInvalid_ThrowsArgumentException()
        {
            //arrange
            PackageService packageService = new PackageService();
            
            Package newPackage = new Package()
            {
                PackageId = 1,
                ProjectNo = 1,
                ProjectItemNo = 1,
                PackageWeight = 1,
                Amount = 1,
                InnerQuantity = 1,
                PackageLength = 1,
                PackageWidth = 1,
                PackageHeight = 1,
                Comment = null,
                Image = null,
                ContainerNo = "FAKE1234567", 
                SupplierName = "TSP"
            };

            //act
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => 
            {
                packageService.Add(newPackage);
            });

            //assert
            Assert.AreEqual("Pakken kunne ikke oprettes", ex.Message); 
        }
    }
}
