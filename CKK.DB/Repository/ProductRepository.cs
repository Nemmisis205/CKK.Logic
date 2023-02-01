using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.DB.Interfaces;
using CKK.Logic.Models;
using System.Data;
using System.Data.Common;
using System.Configuration;
using Dapper;

namespace CKK.DB.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ProductRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }


        public async Task<int> Add(Product entity)
        {
            var sql = "Insert into Products(Price,Quantity,Name) VALUES (@Price,@Quantity,@Name)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = await Task.Run(() => connection.Execute(sql, entity));
                return result;
            }
        }

        public async Task<int> Delete(Product entity)
        {
            var sql = "Delete FROM Products WHERE (Id = @Id)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = await Task.Run(() => connection.Execute(sql, entity));
                return result;
            }

        }

        public async Task<List<Product>> GetAll()
        {
            var sql = "Select * From Products";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = await Task.Run(() => connection.Query<Product>(sql).ToList());
                return result;
            }
        }

        public async Task<Product> GetById(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = await Task.Run(() => connection.QuerySingleOrDefault<Product>(sql, new { Id = id }));
                return result;
            }
        }

        public async Task<List<Product>> GetByName(string name)
        {
            var sql = "SELECT * FROM Products WHERE NAME LIKE ('%' + @Name + '%')";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = await Task.Run(() => connection.Query<Product>(sql, new { Name = name }).ToList());
                return result;
            }
        }

        public async Task<int> Update(Product entity)
        {
            var sql = "UPDATE Products Set Price=@Price, Quantity=@Quantity, Name=@Name WHERE Id = @Id";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = await Task.Run(() => connection.Execute(sql, entity));
                return result;
            }
        }
    }
}
