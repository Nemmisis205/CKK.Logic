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
    public class Product : Entity
    {
        private decimal _price;

        public decimal Price 
        {
            get => _price;
            set
            {
                if (value < 0m) { throw new ArgumentOutOfRangeException(); }
                else { _price = value; }
            }
        }

        public Product(int id, string name, decimal price) : base(id, name) { Price = price; }


    }
}
