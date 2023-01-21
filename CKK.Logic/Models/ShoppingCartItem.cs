using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Models
{
    [Serializable]
    public class ShoppingCartItem : InventoryItem
    {
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public ShoppingCartItem(Product product, int cartId, int custId, int prodId, int quantity) : base(product, quantity)
        {
            ShoppingCartId = cartId;
            CustomerId = custId;
            ProductId = prodId;
            Quantity = quantity;
        }

        public ShoppingCartItem(int id, decimal price, int quantity, string name): base(id, price, quantity, name)
        {
            CustomerId = 205;
            ProductId = id;
            ShoppingCartId = 205;
        }

        public decimal GetTotal()
        {
            return Product.Price * Quantity;
        }

    }
}
