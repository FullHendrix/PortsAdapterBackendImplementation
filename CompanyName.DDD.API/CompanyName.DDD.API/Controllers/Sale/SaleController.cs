using CompanyName.DDD.Domain.SaleAggregate.Application.DTO;
using CompanyName.DDD.Domain.SaleAggregate.Application.Interface;
using Microsoft.AspNetCore.Mvc;
namespace CompanyName.DDD.API.Controllers.Sale
{
    [ApiController]
    [Route("ddd/[controller]")]
    public class SaleController : Controller
    {
        private readonly ISaleAggregate _sale;
        public SaleController(ISaleAggregate sale)
        {
            _sale = sale;
        }
        [HttpPost]
        public IActionResult  Create( SaleCommand command)
        {
            _sale.Create(command);
            return Ok(true);
        }
    }
}