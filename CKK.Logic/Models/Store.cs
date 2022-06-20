using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store
    {
        private int _id;
        private string _name;
        private List<StoreItem> _items = new List<StoreItem>();

        public int GetId() { return _id; }
        public void SetId(int id) { _id = id; }
        public string GetName() { return _name; }
        public void SetName(string name) { _name = name; }
        public StoreItem AddStoreItem(Product prod, int quantity)
        {
            if (quantity > 0) 
            { 
                var storeCheck =
                    from item in _items
                    where item.GetProduct() == prod
                    select item;

                if (storeCheck.Any() == false)
                {
                    _items.Add(new StoreItem(prod, quantity));
                    return _items.Last();
                }
                else
                {
                    var choice = storeCheck.First();

                    choice.SetQuantity(choice.GetQuantity() + quantity);
                    return choice;
                }
            }
            else { return null; }
        }
        public StoreItem RemoveStoreItem(int productNumber, int quantity)
        {
            var removeCheck =
                from item in _items
                where item.GetProduct().GetId() == productNumber
                select item;

            var choice = removeCheck.First();
            if (removeCheck.Any() == true)
            {
                if (quantity < choice.GetQuantity())
                {
                    choice.SetQuantity(choice.GetQuantity() - quantity);
                    return choice;
                }
                else
                {
                    choice.SetQuantity(0);
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
            var idPull =
                from item in _items
                where id == item.GetProduct().GetId()
                select item;

            if (idPull.Any() == false)
            {
                return null;
            }
            else { return idPull.First(); }
           
        }
    }
}
