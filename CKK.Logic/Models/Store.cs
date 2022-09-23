using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Models
{
    public class Store : IStore
    {
        private List<StoreItem> _items = new List<StoreItem>();
        private int FreshId { get; set; }
        public StoreItem AddStoreItem(Product prod, int quantity)
        {
            
            if (quantity <= 0) { throw new InventoryItemStockTooLowException(); }
            else
            {
                var storeCheck =
                    from item in _items
                    where item.Product == prod
                    select item;

                if (storeCheck.Any() == false)
                {
                    _items.Add(new StoreItem(prod, quantity));
                    if(prod.Id == 0)
                    {
                        prod.Id = FreshId;
                        FreshId += 1;
                    }
                    return _items.Last();
                        
                }
                else
                {
                    var choice = storeCheck.First();

                    choice.Quantity += quantity;
                    return choice;
                }
            }
        }
        public StoreItem RemoveStoreItem(int productNumber, int quantity)
        {
            //try
            //{
                var removeCheck =
                    from item in _items
                    where item.Product.Id == productNumber
                    select item;

                if (removeCheck.Any() == false) { throw new ProductDoesNotExistException(); }
                var choice = removeCheck.First();

                
                if(quantity < 0) { throw new ArgumentOutOfRangeException(); }
                else
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
            //}
            //catch (ProductDoesNotExistException) { Console.WriteLine("Product does not exist. Please try a different ID."); return null; }
            //catch (ArgumentOutOfRangeException) { Console.WriteLine("Quantity must not be negative."); return null; }
        }
        public List<StoreItem> GetStoreItems()
        {
            return _items;
        }
        public StoreItem FindStoreItemById(int id)
        {
            try 
            { 
                if (id < 0) { throw new InvalidIdException(); }
                var idPull =
                    from item in _items
                    where id == item.Product.Id
                    select item;

                if (idPull.Any() == false) { throw new ProductDoesNotExistException(); }
                else { return idPull.First(); }
            }
            //catch (InvalidIdException) { Console.WriteLine("ID must not be negative."); return null; }
            catch (ProductDoesNotExistException) { return null; }
        }
    }
}
