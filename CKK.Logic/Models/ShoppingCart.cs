using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private List<ShoppingCartItem> _products = new List<ShoppingCartItem>();

        public Customer Customer { get; set; }
        public List<ShoppingCartItem> Products { get => _products; set => _products = value; }

        public ShoppingCart(Customer cust)
        {
            Customer = cust;
        }

        public int GetCustomerId()
        {
            return Customer.Id;
        }
        public ShoppingCartItem AddProduct(Product prod, int quantity)
        {
           
            if (quantity <= 0) { throw new InventoryItemStockTooLowException(); }
            else if (quantity > 0)
            {
                var cartCheck =
                        from item in Products
                        where prod == item.Product
                        select item;

                if (cartCheck.Any() == false)
                {
                    Products.Add(new ShoppingCartItem(prod, quantity));
                    return Products.Last();
                }
                else
                {
                    var choice = cartCheck.First();
                    choice.Quantity += quantity;
                    return choice;
                }
            }
            else { return null; }
            
        }
        public ShoppingCartItem RemoveProduct(int prodId, int quantity)
        {
            if (quantity < 0) { throw new ArgumentOutOfRangeException(); }

            var cartCheck =
                from item in Products
                where prodId == item.Product.Id
                select item;

            if (cartCheck.Any() == false)
            {
                throw new ProductDoesNotExistException();
            }
            else
            {

                var choice = cartCheck.First();

                if (choice.Quantity > quantity)
                {
                    choice.Quantity -= quantity;
                    return choice;
                }
                else
                {
                    choice.Quantity = 0;
                    Products.Remove(choice);
                    return choice;
                }
            }
        }

        public ShoppingCartItem GetProductById(int id)
        {
            var cartCheck =
                from item in Products
                where id == item.Product.Id
                select item;

            if (id < 0) { throw new InvalidIdException(); }
            else if (cartCheck.Any() == false)
            {
                return null;
            }
            else
            {
                return cartCheck.First();
            }
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            
            foreach (var item in Products)
            {
                total += item.GetTotal();
            }

            return total;
        }

        public List<ShoppingCartItem> GetProducts()
        {
            return Products;
        }
    }
}
