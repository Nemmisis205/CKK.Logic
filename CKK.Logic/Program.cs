using System;
using CKK.Logic.Models;
using CKK.Logic.Interfaces;

namespace CKK.Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            var shop = new Store();
            var prod1 = new Product();
            var prod2 = new Product();

            prod1.SetId(1);
            prod2.SetId(2);

            //Act
            shop.AddStoreItem(prod1, 1);
            shop.AddStoreItem(prod2, 2);

            Console.WriteLine(shop.GetStoreItems());
            Console.WriteLine(shop.GetStoreItems().Count);

            Console.WriteLine("/////////\n\n");

            var steve = new Customer();
            var cart = new ShoppingCart(steve);

            cart.AddProduct(prod1, 1);
            cart.AddProduct(prod2, 2);

            Console.WriteLine(cart.GetProducts());
            Console.WriteLine(cart.GetProducts().Count);

            Console.WriteLine("/////////\n\n");

            shop.RemoveStoreItem(1, 2);
            Console.WriteLine(shop.GetStoreItems().Count);

            cart.RemoveProduct(1, 2);
            Console.WriteLine(cart.GetProducts().Count);

        }
    }
}
