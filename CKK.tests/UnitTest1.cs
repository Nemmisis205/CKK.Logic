using System;
using Xunit;
using CKK.Logic.Models;

namespace CKK.tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddingProduct()
        {
            //Arrange
            Customer custard = new Customer();
            ShoppingCart cart = new ShoppingCart(custard);
            Product prod1 = new Product();
            Product prod2 = new Product();
            Product prod3 = new Product();

            //Act
            cart.AddProduct(prod1, 4);
            cart.AddProduct(prod2);
            cart.AddProduct(prod1, 2);
            cart.AddProduct(prod3, -4);

            //Assert
            Assert.Equal(prod1, cart.GetProduct(1).GetProduct());
            Assert.Equal(6, cart.GetProduct(1).GetQuantity());
            Assert.Equal(1, cart.GetProduct(2).GetQuantity());
            Assert.Null(cart.GetProduct(3));
        }

        [Fact]
        public void RemovingProduct()
        {
            //Arrange
            Customer custard = new Customer();
            ShoppingCart cart = new ShoppingCart(custard);
            Product prod1 = new Product();
            Product prod2 = new Product();

            //Act
            cart.AddProduct(prod1, 4);
            cart.AddProduct(prod2);
            cart.RemoveProduct(prod1, 2);
            cart.RemoveProduct(prod2, 3);

            //Assert
            Assert.Equal(2, cart.GetProduct(1).GetQuantity());
            Assert.Null(cart.GetProduct(2));
            Assert.Null(cart.GetProduct(3));
        }

        [Fact]
        public void GettingTotal()
        {
            //Arrange
            Customer custard = new Customer();
            ShoppingCart cart = new ShoppingCart(custard);
            Product prod1 = new Product();
            Product prod2 = new Product();

            //Act
            prod1.SetPrice(12.99m);
            prod2.SetPrice(50.49m);
            cart.AddProduct(prod1, 4);
            cart.AddProduct(prod2);

            //Assert
            Assert.Equal(102.45m, cart.GetTotal());

        }
    }
}
