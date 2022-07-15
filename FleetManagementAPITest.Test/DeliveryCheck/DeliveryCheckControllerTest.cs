using Business.Abstract;
using Entities.Dto;
using FleetManagementAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FleetManagementAPITest.Test.DeliveryCheck
{
    public class DeliveryCheckControllerTest
    {
        private readonly Mock<IPackageService> _mockRepo;
        private readonly DeliveryCheckController _controller;
        private DeliveryCheckDto deliveryCheckDto;
        public DeliveryCheckControllerTest()
        {
            _mockRepo = new Mock<IPackageService>();
            _controller = new DeliveryCheckController(_mockRepo.Object);
            deliveryCheckDto = new DeliveryCheckDto()
            {
                Plate = "Test",
                Route = new List<RouteDto> { new RouteDto() }
            };
        }
        [Fact]
        public void PostPackageToBag_ActionExecutes_ReturnCreatedAtAction()
        {
            _mockRepo.Setup(x => x.DeliveryCheck(deliveryCheckDto));

            var result = _controller.DeliveryCheck(deliveryCheckDto);
            var createdActionResult = Assert.IsType<OkObjectResult>(result);
            _mockRepo.Verify(x => x.DeliveryCheck(deliveryCheckDto), Times.Once());
        }
    }
}
