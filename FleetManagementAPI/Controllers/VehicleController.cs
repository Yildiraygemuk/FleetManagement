using Business.Abstract;
using Entities.Dto;
using Entities.Vm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [ProducesResponseType(typeof(VehicleVm), 200)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _vehicleService.GetListQueryable();
            return Ok(result.Data);
        }
        [ProducesResponseType(typeof(VehicleDto), 200)]
        [HttpPost]
        public IActionResult Add(VehicleDto vehicleDto)
        {
            var result = _vehicleService.Add(vehicleDto);
            return Ok(result);
        }
    }
}
