using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Models
{
    public class Store : Entity, IStore
    {
        private List<StoreItem> _items = new List<StoreItem>();

        //public Store(int id, string name) : base(id, name) { }
        public StoreItem AddStoreItem(Product prod, int quantity)
        {
            if(quantity <= 0) { throw new InventoryItemStockTooLowException(); }
            else if (quantity > 0) 
            { 
                var storeCheck =
                    from item in _items
                    where item.Product == prod
                    select item;

                if (storeCheck.Any() == false)
                {
                    _items.Add(new StoreItem(prod, quantity));
                    return _items.Last();
                }
                else
                {
                    var choice = storeCheck.First();

                    choice.Quantity += quantity;
                    return choice;
                }
            }
            else { return null; }
        }
        public StoreItem RemoveStoreItem(int productNumber, int quantity)
        {
            if (quantity < 0) { throw new ArgumentOutOfRangeException(); }

            var removeCheck =
                from item in _items
                where item.Product.Id == productNumber
                select item;

            var choice = removeCheck.First();

            if (removeCheck.Any() == false) { throw new ProductDoesNotExistException(); }
            if (removeCheck.Any() == true)
            {
                if (quantity < choice.Quantity)
                {
                    choice.Quantity -= quantity;
                    return choice;
                }
                else
                {
                    choice.Quantity = 0;
                    return choice;
                }
            }
            else { return null; }
        }
        public List<StoreItem> GetStoreItems()
        {
            return _items;
        }
        public StoreItem FindStoreItemById(int id)
        {
            if (id < 0) { throw new InvalidIdException(); }
            var idPull =
                from item in _items
                where id == item.Product.Id
                select item;

            if (idPull.Any() == false)
            {
                return null;
            }
            else { return idPull.First(); }
           
        }
    }
}
