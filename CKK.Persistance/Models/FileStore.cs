using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Persistance.Interfaces;
using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Logic.Exceptions;

namespace CKK.Persistance.Models
{
    public class FileStore : ISavable, ILoadable, IStore
    {
        readonly string FilePath = @"C:\Users\4400113787\Documents\Persistance\StoreItems.dat";
        private List<StoreItem> _items = new List<StoreItem>();
        private int IdCounter { get; set; }
        

        public FileStore()
        {
            CreatePath();
            Load();
            
        }
        public StoreItem AddStoreItem(Product prod, int quantity)
        {

            if (quantity <= 0) { throw new InventoryItemStockTooLowException(); }
            else
            {
                var storeCheck =
                    from item in _items
                    where item.Product.Id == prod.Id
                    select item;

                if (storeCheck.Any() == false)
                {
                    _items.Add(new StoreItem(prod, quantity));
                    if (prod.Id == 0)
                    {
                        bool idCheck = false;
                        while (idCheck == false)
                        {
                            IdCounter += 1;
                            var idChecker =
                                from item in _items
                                where item.Product.Id == IdCounter
                                select item;

                            if (idChecker.Any() == false)
                            {
                                prod.Id = IdCounter;
                                idCheck = true;
                            }
                        }
                    }
                    Save();
                    return _items.Last();

                }
                else
                {
                    var choice = storeCheck.First();

                    choice.Quantity += quantity;
                    Save();
                    return choice;
                }
            }
        }
        public StoreItem RemoveStoreItem(int productNumber, int quantity)
        {
            var removeCheck =
                from item in _items
                where item.Product.Id == productNumber
                select item;

            if (removeCheck.Any() == false) { throw new ProductDoesNotExistException(); }
            var choice = removeCheck.First();


            if (quantity < 0) { throw new ArgumentOutOfRangeException(); }
            else
            {
                if (quantity < choice.Quantity)
                {
                    choice.Quantity -= quantity;
                    Save();
                    return choice;
                }
                else
                {
                    choice.Quantity = 0;
                    Save();
                    return choice;
                }
            }
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
            catch (ProductDoesNotExistException) { return null; }
        }

        public void DeleteStoreItem(int id)
        {
            var idPull =
                from item in _items
                where id == item.Product.Id
                select item;
            var choice = idPull.First();
            _items.Remove(choice);
            Save();

        }

        public void Save()
        {
            var output = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
            var binarySerialize = new BinaryFormatter();
            binarySerialize.Serialize(output, GetStoreItems());
            output.Close();
        }
        public void Load()
        {
            var input = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            var binaryDeserialize = new BinaryFormatter();
            List<StoreItem> savedItems = (List<StoreItem>)binaryDeserialize.Deserialize(input);
            foreach (StoreItem i in savedItems)
            {
                _items.Add(i);
            }
            input.Close();
        }

        private void CreatePath()
        {
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
            }
        }
    }
}