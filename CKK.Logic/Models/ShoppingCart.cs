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
        private ShoppingCartItem _product1 = null;
        private ShoppingCartItem _product2 = null;
        private ShoppingCartItem _product3 = null;

        public ShoppingCart(Customer cust)
        {
            _customer = cust;
        }

        public int GetCustomerId()
        {
            return _customer.GetId();
        }
        public void AddProduct(Product prod, int quantity)
        {
            if (_product1 == null)
            {
                _product1 = new ShoppingCartItem(prod, quantity);
            }
            else if (_product2 == null)
            {
                _product2 = new ShoppingCartItem(prod, quantity);
            }
            else if (_product3 == null)
            {
                _product3 = new ShoppingCartItem(prod, quantity);
            }
        }
        public ShoppingCartItem AddProduct(Product prod)
        {
            if (_product1 != null && prod == _product1.GetProduct())
            {
                _product1.SetQuantity(_product1.GetQuantity() + 1);
                return _product1;
            }
            else if(_product2 != null && prod == _product2.GetProduct())
            {
                _product2.SetQuantity(_product2.GetQuantity() + 1);
                return _product2;
            }
            else if(_product3 != null && prod == _product3.GetProduct())
            {
                _product3.SetQuantity(_product3.GetQuantity() + 1);
                return _product3;
            }
            else if (_product1 == null)
            {
                _product1 = new ShoppingCartItem(prod, 1);
                return _product1;
            }
            else if (_product2 == null)
            {
                _product2 = new ShoppingCartItem(prod, 1);
                return _product2;
            }
            else if (_product3 == null)
            {
                _product3 = new ShoppingCartItem(prod, 1);
                return _product3;
            }
            else { return null; }
        }
        public void RemoveProduct(Product prod, int quantity)
        {
            if (_product1.GetProduct() == prod)
            {
                if (_product1.GetQuantity() <= quantity)
                {
                    _product1 = null;
                }
                else
                {
                    _product1.SetQuantity(_product1.GetQuantity() - quantity);
                }
            }
            else if (_product2.GetProduct() == prod)
            {
                if (_product2.GetQuantity() <= quantity)
                {
                    _product2 = null;
                }
                else
                {
                    _product2.SetQuantity(_product2.GetQuantity() - quantity);
                }
            }
            else if (_product3.GetProduct() == prod)
            {
                if (_product3.GetQuantity() <= quantity)
                {
                    _product3 = null;
                }
                else
                {
                    _product3.SetQuantity(_product3.GetQuantity() - quantity);
                }
            }
        }

        public ShoppingCartItem GetProductById(int id)
        {
            if (_product1.GetProduct().GetId() == id)
            {
                return _product1;
            }
            else if (_product2.GetProduct().GetId() == id)
            {
                return _product2;
            }
            else if (_product3.GetProduct().GetId() == id)
            {
                return _product3;
            }
            else { return null; }
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            if (_product1 != null) { total += _product1.GetTotal(); }
            if (_product2 != null) { total += _product2.GetTotal(); }
            if (_product3 != null) { total += _product3.GetTotal(); }
            return total;
        }

        public ShoppingCartItem GetProduct(int productNum)
        {
            if (productNum == 1) { return _product1; }
            else if (productNum == 2) { return _product2; }
            else if (productNum == 3) { return _product3; }
            else { return null; }
        }
    }
}
