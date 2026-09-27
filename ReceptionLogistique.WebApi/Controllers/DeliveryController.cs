using Microsoft.AspNetCore.Mvc;

namespace ReceptionLogistique.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {
        const int OrderId = 123456;

        [HttpGet(Name = "GetDelivery")]
        public int Get()
        {
            return OrderId;
        }
    }
}
