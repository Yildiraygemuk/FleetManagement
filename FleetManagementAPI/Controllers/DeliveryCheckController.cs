using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryCheckController : ControllerBase
    {
        private readonly IPackageService _packageService;
        public DeliveryCheckController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [ProducesResponseType(typeof(DeliveryCheckDto), 200)]
        [HttpPost]
        public IActionResult DeliveryCheck(DeliveryCheckDto deliveryCheckDto)
        {
            var result = _packageService.DeliveryCheck(deliveryCheckDto);
            return Ok(result);
        }
    }
}
