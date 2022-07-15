using Business.Abstract;
using Entities.Dto;
using Entities.Enums;
using Entities.Vm;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;

namespace FleetManagementAPITest.Test.Bag
{
    public class BagControllerTest
    {
        private readonly Mock<IBagService> _mockRepo;
        private readonly BagController _controller;
        private List<BagVm> bags;
        private BagDto bagDto;
        public BagControllerTest()
        {
            _mockRepo = new Mock<IBagService>();
            _controller = new BagController(_mockRepo.Object);
            bags = new List<BagVm>()
            {
                new BagVm() {
                    Barcode = "C725799",
                    BagStatus= (EnumBagStatus)1,
                    DeliveryPointForUnloading= (EnumDeliveryPoint)1
                },
                new BagVm() {
                    Barcode = "C725798",
                    BagStatus= (EnumBagStatus)1,
                    DeliveryPointForUnloading= (EnumDeliveryPoint)1
                }
            };
            bagDto = new BagDto
            {
                Barcode = "C725786",
                DeliveryPointForUnloading = (EnumDeliveryPoint)1
            };
        }
        [Fact]
        public void Bag_ActionExecutes_ReturnBagListQueryable()
        {
            _mockRepo.Setup(repo => repo.GetListQueryable().Data).Returns(bags.AsQueryable());

            var result = _controller.GetList();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBags = Assert.IsAssignableFrom<IQueryable<BagVm>>(okResult.Value);
            Assert.Equal<int>(2, returnBags.Count());
        }
        [Fact]
        public void PostPackage_ActionExecutes_ReturnCreatedAtAction()
        {
            _mockRepo.Setup(x => x.Add(bagDto));

            var result = _controller.Add(bagDto);

            var createdActionResult = Assert.IsType<OkObjectResult>(result);
            _mockRepo.Verify(x => x.Add(bagDto), Times.Once());
        }
    }
}
