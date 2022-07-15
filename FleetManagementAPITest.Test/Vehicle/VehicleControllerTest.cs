using Business.Abstract;
using Entities.Dto;
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

namespace FleetManagementAPITest.Test.Vehicle
{
    public class VehicleControllerTest
    {
        private readonly Mock<IVehicleService> _mockRepo;
        private readonly VehicleController _controller;
        private List<VehicleVm> vehicles;
        private VehicleDto vehicleDto;
        public VehicleControllerTest()
        {
            _mockRepo = new Mock<IVehicleService>();
            _controller = new VehicleController(_mockRepo.Object);
            vehicles = new List<VehicleVm>()
            {
                new VehicleVm() {
                    LicancePlate = "34 TL 34"
                },
                new VehicleVm() {
                    LicancePlate = "34 YG 36"
                }
            };
            vehicleDto = new VehicleDto
            {
                LicancePlate = "34 AS 45"
            };
        }
        [Fact]
        public void Vehicle_ActionExecutes_ReturnVehicleListQueryable()
        {
            _mockRepo.Setup(repo => repo.GetListQueryable().Data).Returns(vehicles.AsQueryable());

            var result = _controller.GetList();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnVehicles = Assert.IsAssignableFrom<IQueryable<VehicleVm>>(okResult.Value);
            Assert.Equal<int>(2, returnVehicles.Count());
        }
        [Fact]
        public void PostPackage_ActionExecutes_ReturnCreatedAtAction()
        {
            _mockRepo.Setup(x => x.Add(vehicleDto));

            var result = _controller.Add(vehicleDto);

            var createdActionResult = Assert.IsType<OkObjectResult>(result);
            _mockRepo.Verify(x => x.Add(vehicleDto), Times.Once());
        }
    }
}
