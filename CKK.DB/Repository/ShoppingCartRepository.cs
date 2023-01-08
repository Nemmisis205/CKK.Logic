using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;

namespace CKK.DB.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ShoppingCartRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }


        public int Add(ShoppingCartItem entity)
        {
            var sql = "Insert into ShoppingCartItems(ShoppingCartId, ProductId, Quantity) VALUES (@ShoppingCartId, @ProductId, @Quantity";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public ShoppingCartItem AddToCart(string name, int quantity)
        {
            throw new NotImplementedException();
        }

        public int ClearCart(int ShoppingCartId)
        {
            var sql = "Delete FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, new {ShoppingCartId =ShoppingCartId});
                return result;
            }
        }

        public List<ShoppingCartItem> GetProducts(int ShoppingCartId)
        {
            var sql = "Select * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<ShoppingCartItem>(sql, new {ShoppingCartId = ShoppingCartId}).ToList();
                return result;
            }
        }

        public decimal GetTotal(int ShoppingCartId)
        {
            var sql = "Select * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            decimal total = 0;
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<ShoppingCartItem>(sql, new { ShoppingCartId = ShoppingCartId }).ToList();
                foreach (var i in result)
                {
                    total += i.Product.Price * i.Quantity;
                }
            }
            return total;
        }

        public void Ordered(int ShoppingCartId)
        {
            throw new NotImplementedException();
        }

        public int Update(ShoppingCartItem entity)
        {
            var sql = "UPDATE ShoppingCartItems Set Quantity = @Quantity WHERE ShoppingCartId = @ShoppingCartId AND ProductId = @ProductId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }
    }
}
