using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;

namespace CKK.DB.Interfaces
{
    public interface IShoppingCartRepository
    {
        ShoppingCartItem AddToCart(string itemName, int quantity);
        int ClearCart(int ShoppingCartId);
        decimal GetTotal(int ShoppingCartId);
        List<ShoppingCartItem> GetProducts(int ShoppingCartId);
        void Ordered(int ShoppingCartId);
        Task<int> Update(ShoppingCartItem entity);
        Task<int> Add(ShoppingCartItem entity);
        int GetCartItemCount(int ShoppingCartId);
    }
}
