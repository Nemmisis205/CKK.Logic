using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    class ShoppingCartItem
    {
        private Product _product;
        private int _quantity;

        public ShoppingCartItem(Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
        }
        public int GetQuantity() { return _quantity; }
        public SetQuantity(int quantity) { _quantity = quantity; }
        public Product GetProduct() { return _product; }
        public SetProduct(Product product) { _product = product; }
    }
}
