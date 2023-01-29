using Microsoft.AspNetCore.Mvc;
using CKK.DB.Interfaces;
using CKK.Online.Models;
using CKK.Logic.Models;
using System.Data;
using System;
using System.Security.Cryptography;

namespace CKK.Online.Controllers
{
    public class ShopController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public string OrderNumber = "205XXX205XXX1";
        Customer Customer = new Customer(205, "Kohle", "123 Woah Street", 205);


        public ShopController(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        [HttpGet]
        [Route("/Shop/ShoppingCart")]
        public IActionResult Index()
        {
            var model = new ShopModel { UOW = _UOW };
            ViewBag.ShoppingCartId= Customer.ShoppingCartId;

            return View(model);
        }

        [HttpGet]
        [Route("/Shop/ClearCart")]
        public IActionResult ClearCart()
        {
            var model = new ShopModel { UOW = _UOW };
            _UOW.ShoppingCarts.ClearCart(Customer.ShoppingCartId);

            Response.Redirect("/Shop/ShoppingCart");
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewCart([FromRoute]int shoppingCartId)
        {
            var model = new ShopModel { UOW = _UOW };
            ViewBag.OrderNumber = OrderNumber;
            ViewBag.ShoppingCartId = Customer.ShoppingCartId;
            ViewBag.CustomerId = Customer.Id;


            return View(model);
        }

        [HttpGet]
        [Route("Shop/Checkout/{customerId}")]
        public IActionResult CheckOut([FromRoute]int customerId)
        {
            var model = new ShopModel { UOW = _UOW };
            var model2 = new CheckOutModel { StatusMessage = ""};

            var newOrderId = _UOW.Orders.GetLastId() + 1;
            var productList = _UOW.ShoppingCarts.GetProducts(Customer.ShoppingCartId);

            foreach (ShoppingCartItem i in productList)
            {
                Product edittedProduct = _UOW.Products.GetById(i.ProductId).Result;
                if (edittedProduct.Quantity >= i.Product.Quantity)
                {
                    edittedProduct.Quantity = edittedProduct.Quantity - i.Product.Quantity;
                    _UOW.Products.Update(edittedProduct);
                }
            }

            if (_UOW.Orders.Add(new Order(newOrderId, OrderNumber, customerId, Customer.ShoppingCartId)).Result == -1)
            {
                model2.StatusMessage = "Order failed, please try again.";
                return View(model2);
            }
            else
            {
                model2.StatusMessage = "Order successfully placed!";
                Customer.ShoppingCartId += 1;
            }
            
            return View(model2);
        }

        [HttpGet]
        [Route("Shop/Add/{productId}")]
        public IActionResult Add([FromRoute]int productId, [FromQuery(Name = "quantity")]int quantity)
        {
            var model = new ShopModel { UOW = _UOW };
            var test = quantity;
            ViewBag.CustomerId = Customer.Id;
            ViewBag.ShoppingCartId = Customer.ShoppingCartId;

            var addedProduct = model.UOW.Products.GetById(productId);
            var shoppingCartItem = new ShoppingCartItem(addedProduct.Result, Customer.ShoppingCartId, 1, productId, quantity);

            model.UOW.ShoppingCarts.Add(shoppingCartItem);

            return View(model);
        }

        [HttpGet]
        [Route("Shop/Edit/{productId}")]
        public IActionResult Edit([FromRoute]int productId, [FromQuery(Name ="Quantity")]int quantity)
        {
            var model = new ShopModel { UOW = _UOW };
            var priceHold = _UOW.Products.GetById(productId).Result.Price;
            var nameHold = _UOW.Products.GetById(productId).Result.Name;
            ShoppingCartItem item = new ShoppingCartItem(productId, priceHold, quantity, nameHold);

            _UOW.ShoppingCarts.Update(item);
            return RedirectToAction("ViewCart");
        }
    }
}
