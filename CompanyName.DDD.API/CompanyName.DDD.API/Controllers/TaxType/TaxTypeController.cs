using CompanyName.DDD.Domain.TaxTypeAggregate.Application.Interface;
using Microsoft.AspNetCore.Mvc;
namespace CompanyName.DDD.API.Controllers.TaxType
{
    [ApiController]
    [Route("material/v1/[controller]")]
    public class TaxTypeController : Controller
    {
        private readonly ITaxTypeAggregate _taxType;
        public TaxTypeController(ITaxTypeAggregate taxType)
        {
            _taxType = taxType;
        }
        [HttpGet]
        public IActionResult GetSimpleResponse()
        {
            var rows = _taxType.GetSimpleResponse();
            return Ok(rows);
        }
    }
}