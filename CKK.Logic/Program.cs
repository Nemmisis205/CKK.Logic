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

            var shop = new Store();
            var prod1 = new Product();
            var prod2 = new Product();
            var prod3 = new Product();


            shop.AddStoreItem(prod1, -1);
            shop.AddStoreItem(prod2, 1337);
            shop.AddStoreItem(prod3, 4);
            prod1.Id = 1;
            prod3.Id = 2;

            Console.WriteLine($"{shop.GetStoreItems().Count}");
            Console.WriteLine($"{shop.FindStoreItemById(1)?.Quantity}");

            shop.RemoveStoreItem(1, 1338);
            Console.WriteLine($"{shop.FindStoreItemById(1)?.Quantity}");
            Console.WriteLine($"{shop.GetStoreItems().Count}");

        }
    }
}
