using Microsoft.AspNetCore.Mvc;
using CKK.DB.Interfaces;
using CKK.Online.Models;
using CKK.Logic.Models;
using System.Data;

namespace CKK.Online.Controllers
{
    public class ShopController : Controller
    {
        private readonly IUnitOfWork _UOW;
        int CustomerId = 205;
        int ShoppingCartId = 205;
        string OrderNumber = "205XXX205XXX1";


        public ShopController(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        [HttpGet]
        [Route("/Shop/ShoppingCart")]
        public IActionResult Index()
        {
            var model = new ShopModel { UOW = _UOW };

            return View(model);
        }

        [HttpGet]
        public IActionResult ViewCart([FromRoute]int shoppingCartId)
        {
            var model = new ShopModel { UOW = _UOW };
            ViewBag.OrderNumber = OrderNumber;
            ViewBag.ShoppingCartId = ShoppingCartId;
            ViewBag.CustomerId = CustomerId;


            return View(model);
        }

        [HttpGet]
        [Route("Shop/Checkout/{customerId}")]
        public IActionResult CheckOut([FromRoute]int customerId)
        {
            var model = new ShopModel { UOW = _UOW };

            var newOrderId = _UOW.Orders.GetLastId() + 1;

            _UOW.Orders.Add(new Order(newOrderId, OrderNumber, customerId, ShoppingCartId));
            

            return View(model);
        }

        [HttpGet]
        [Route("Shop/Add/{productId}")]
        public IActionResult Add([FromRoute]int productId, [FromQuery(Name = "quantity")]int quantity)
        {
            var model = new ShopModel { UOW = _UOW };
            var test = quantity;
            ViewBag.CustomerId = CustomerId;
            ViewBag.ShoppingCartId = ShoppingCartId;

            var addedProduct = model.UOW.Products.GetById(productId);
            var shoppingCartItem = new ShoppingCartItem(addedProduct, ShoppingCartId, 1, productId, quantity);

            model.UOW.ShoppingCarts.Add(shoppingCartItem);

            return View(model);
        }
    }
}
