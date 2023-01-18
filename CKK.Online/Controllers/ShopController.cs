using Microsoft.AspNetCore.Mvc;
using CKK.DB.Interfaces;

namespace CKK.Online.Controllers
{
    public class ShopController : Controller
    {
        private readonly IUnitOfWork _UOW;

        public ShopController(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        [HttpGet]
        [Route("/Shop/ShoppingCart")]
        public IActionResult Index()
        {


            return View();
        }

        public IActionResult CheckOutCustomer([FromQuery]int orderId)
        {
            return View();
        }

        [HttpGet]
        [Route("Shop/ShoppingCart/Add/{productId}")]
        public IActionResult Add([FromRoute]int productId, [FromQuery]int quantity)
        {
            return View();
        }
    }
}
