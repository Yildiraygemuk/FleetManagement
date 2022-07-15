 using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Entities.Enums;
using Entities.Vm;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Controllers;
using Xunit;

namespace FleetManagementAPITest.Test
{
    public class PackageControllerTest
    {
        private readonly Mock<IPackageService> _mockRepo;
        private readonly PackageController _controller;
        private List<PackageVm> packages;
        private PackageDto packageDto;
        private BagInformationDto bagInformationDto;
        public PackageControllerTest()
        {
            _mockRepo = new Mock<IPackageService>();
            _controller = new PackageController(_mockRepo.Object);
            packages = new List<PackageVm>()
            {
                new PackageVm() { 
                    Barcode = "P7988000121",
                    VolumetricWeight = 5,
                    BagId = Guid.Empty,
                    PackageStatus= (EnumPackageStatus)1,
                    DeliveryPointForUnloading= (EnumDeliveryPoint)1 
                },
                new PackageVm() {
                    Barcode = "P7988000122",
                    VolumetricWeight = 35,
                    BagId = Guid.Empty,
                    PackageStatus= (EnumPackageStatus)1,
                    DeliveryPointForUnloading= (EnumDeliveryPoint)1
                }
            };
            packageDto = new PackageDto
            {
                Barcode = "P9999999",
                VolumetricWeight = 5,
                PackageStatus = (EnumPackageStatus)1,
                DeliveryPointForUnloading = (EnumDeliveryPoint)1
            };
            bagInformationDto = new BagInformationDto
            {
                BagBarcode = "P9999999",
                PackageBarcode = "C45645"
            };
        }
        [Fact]
        public void Package_ActionExecutes_ReturnPackageListQueryable()
        {
            _mockRepo.Setup(repo => repo.GetListQueryable().Data).Returns(packages.AsQueryable());

            var result = _controller.GetList();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnPackages = Assert.IsAssignableFrom<IQueryable<PackageVm>>(okResult.Value);
            Assert.Equal<int>(2, returnPackages.Count());
        } 
        [Fact]
        public void PostPackage_ActionExecutes_ReturnCreatedAtAction()
        {
            _mockRepo.Setup(x => x.Add(packageDto));

            var result = _controller.Add(packageDto);
            var createdActionResult = Assert.IsType<OkObjectResult>(result);
            _mockRepo.Verify(x => x.Add(packageDto), Times.Once());
        }
        [Fact]
        public void PostPackageToBag_ActionExecutes_ReturnCreatedAtAction()
        {
            _mockRepo.Setup(x => x.AddPackageToBag(bagInformationDto));

            var result = _controller.AddPackageToBag(bagInformationDto);
            var createdActionResult = Assert.IsType<OkObjectResult>(result);
            _mockRepo.Verify(x => x.AddPackageToBag(bagInformationDto), Times.Once());
        }
    }
}
