using Business.Abstract;
using Entities.Dto;
using Entities.Vm;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [ProducesResponseType(typeof(PackageVm), 200)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _packageService.GetListQueryable();
            return Ok(result.Data);
        }
        [HttpGet("GetPackagesWithBagListQueryable")]
        public IActionResult GetPackagesWithBagListQueryable()
        {
            var result = _packageService.GetPackagesWithBagListQueryable();
            return Ok(result.Data);
        }
        [ProducesResponseType(typeof(PackageDto), 200)]
        [HttpPost]
        public IActionResult Add(PackageDto packageDto)
        {
            var result = _packageService.Add(packageDto);
            return Ok(result);
        }
        [ProducesResponseType(typeof(PackageDto), 200)]
        [HttpPost("AddPackageToBag")]
        public IActionResult AddPackageToBag(BagInformationDto bagInformationDto)
        {
            var result = _packageService.AddPackageToBag(bagInformationDto);
            return Ok(result);
        }
    }
}
