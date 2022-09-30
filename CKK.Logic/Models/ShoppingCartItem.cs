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
        public ShoppingCartItem(Product product, int quantity) : base(product, quantity)
        {
        }
        
        public decimal GetTotal()
        {
            return Quantity * Product.Price;
        }
    }
}
