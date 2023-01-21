using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Interfaces
{
    [Serializable]
    public abstract class InventoryItem
    {
        private int _quantity;

        public Product Product { get; set; }
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0) { throw new InventoryItemStockTooLowException(); }
                else { _quantity = value; }
            }
        }

        public InventoryItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public InventoryItem(int id, decimal price , int quantity , string name)
        {
            Product = new Product(id, price, quantity, name);
            Quantity = quantity;
        }
    }
}
