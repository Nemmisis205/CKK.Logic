using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        private Customer _customer;
        private List<ShoppingCartItem> _products = new List<ShoppingCartItem>();

        public ShoppingCart(Customer cust)
        {
            _customer = cust;
        }

        public int GetCustomerId()
        {
            return _customer.GetId();
        }
        public ShoppingCartItem AddProduct(Product prod, int quantity)
        {
            if(quantity > 0)
            {
                var cartCheck =
                    from item in _products
                    where prod == item.GetProduct()
                    select item;
                var choice = cartCheck.First();

                if (cartCheck.Any() == false)
                {
                    _products.Add(new ShoppingCartItem(prod, quantity));
                    return _products.Last();
                }
                else
                {
                    choice.SetQuantity(choice.GetQuantity() + quantity);
                    return choice;
                }
            }
            else { return null; }
        }
        public ShoppingCartItem RemoveProduct(int prodId, int quantity)
        {
            var cartCheck =
                from item in _products
                where prodId == item.GetProduct().GetId()
                select item;
            var choice = cartCheck.First();

            if (cartCheck.Any() == false)
            {
                return null;
            }
            else
            {
                if (choice.GetQuantity() > quantity)
                {
                    choice.SetQuantity(choice.GetQuantity() - quantity);
                    return choice;
                }
                else
                {
                    choice.SetQuantity(0);
                    _products.Remove(choice);
                    return choice;
                }
            }
        }

        public ShoppingCartItem GetProductById(int id)
        {
            var cartCheck =
                from item in _products
                where id == item.GetProduct().GetId()
                select item;

            return cartCheck.First();
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            
            foreach (var item in _products)
            {
                total += item.GetProduct().GetPrice() * item.GetQuantity();
            }

            return total;
        }

        public List<ShoppingCartItem> GetProducts()
        {
            return _products;
        }
    }
}
