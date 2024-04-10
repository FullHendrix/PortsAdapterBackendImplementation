using Microsoft.AspNetCore.Mvc;

namespace CompanyName.DDD.API.Controllers.TaxType
{
    [ApiController]
    [Route("material/v1/[controller]")]
    public class TaxTypeController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return Ok(_accesory.Get());
            return Ok();
        }
    }
}
