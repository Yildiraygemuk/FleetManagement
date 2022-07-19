using Business.Abstract;
using Entities.Dto;
using Entities.Vm;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly IBagService _bagService;
        public BagController(IBagService bagService)
        {
            _bagService = bagService;
        }
        [ProducesResponseType(typeof(IQueryable<BagVm>), 200)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _bagService.GetListQueryable();
            return Ok(result.Data);
        }
        [ProducesResponseType(typeof(BagDto), 200)]
        [HttpPost]
        public IActionResult Add(BagDto bagDto)
        {
            var result = _bagService.Add(bagDto);
            return Ok(result);
        }
    }
}
