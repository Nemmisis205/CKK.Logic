using System;
using CKK.Logic.Models;
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;


namespace CKK.Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            var me = new Customer();
            var shop = new Store();
            var cart = new ShoppingCart(me);
            var prod1 = new Product();
            var prod2 = new Product();
            var prod3 = new Product();

            cart.AddProduct(prod1, 3);
            cart.AddProduct(prod2, 2);
            cart.AddProduct(prod3, 1337);

            Console.WriteLine($"{cart.GetProducts().Count}");

            //shop.AddStoreItem(prod1, 2);
            //shop.AddStoreItem(prod2, 1337);
            //shop.AddStoreItem(prod3, 4);
            //prod1.Id = 1;
            //prod3.Id = 2;

            //Console.WriteLine($"{shop.GetStoreItems().Count}");
            //Console.WriteLine($"{shop.FindStoreItemById(1)?.Quantity}");

            //shop.RemoveStoreItem(1, 1338);
            //Console.WriteLine($"{shop.FindStoreItemById(1)?.Quantity}");
            //Console.WriteLine($"{shop.GetStoreItems().Count}");

        }
    }
}
