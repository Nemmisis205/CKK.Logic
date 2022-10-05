using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Interfaces
{
    public interface IStore
    {
        StoreItem AddStoreItem(Product prod, int quantity);
        StoreItem RemoveStoreItem(int id, int quantity);
        List<StoreItem> GetStoreItems();
        StoreItem FindStoreItemById(int id);
        void DeleteStoreItem(int id);

        public List<StoreItem> GetAllProductsByName(string name)
        {
            List<StoreItem> returnItems = new List<StoreItem>();
            List<StoreItem> storeItems = this.GetStoreItems();
            foreach(StoreItem i in storeItems)
            {
                if (i.Product.Name.Contains(name))
                {
                    returnItems.Add(i);
                }
            }

            return returnItems;
        }

        public List<StoreItem> GetProductsByQuantity(List<StoreItem> passedStoreItems)
        {
            List<StoreItem> storeItems = passedStoreItems;

            int i = 0;
            while (i+1 < storeItems.Count)
            {
                if (storeItems[i].Quantity < storeItems[i + 1].Quantity)
                {
                    var temp = storeItems[i];
                    storeItems[i] = storeItems[i + 1];
                    storeItems[i + 1] = temp;
                }
                i++;
            }
            for (int j = 0; j + 1 < storeItems.Count; j++)
            {
                if (storeItems[j].Quantity < storeItems[j + 1].Quantity)
                {
                    return GetProductsByQuantity(storeItems);
                }
            }
            return storeItems;
        }
        public List<StoreItem> GetProductsByPrice(List<StoreItem> passedStoreItems)
        {
            List<StoreItem> storeItems = passedStoreItems;

            int i = 0;
            while (i + 1 < storeItems.Count)
            {
                if (storeItems[i].Product.Price < storeItems[i + 1].Product.Price)
                {
                    var temp = storeItems[i];
                    storeItems[i] = storeItems[i + 1];
                    storeItems[i + 1] = temp;
                }
                i++;
            }
            for (int j = 0; j + 1 < storeItems.Count; j++)
            {
                if (storeItems[j].Product.Price < storeItems[j + 1].Product.Price)
                {
                    return GetProductsByPrice(storeItems);
                }
            }
            return storeItems;
        }
    }
}
