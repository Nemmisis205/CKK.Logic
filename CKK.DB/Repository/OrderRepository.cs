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
    public class OrderRepository : IOrderRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public OrderRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }


        public int Add(Order entity)
        {
            var sql = "Insert into Orders(OrderID, OrderNumber, CustomerId, ShoppingCartId) VALUES (@OrderId, @OrderNumber, @CustomerId, @ShoppingCartId)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public int Delete(Order entity)
        {
            var sql = "Delete FROM Orders WHERE (OrderId = @OrderId)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public List<Order> GetAll()
        {
            var sql = "Select * From Orders";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Order>(sql).ToList();
                return result;
            }
        }

        public Order GetById(int id)
        {
            var sql = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Order>(sql, new { OrderId = id });
                return result;
            }
        }

        public Order GetOrderByCustomerId(int id)
        {
            var sql = "SELECT * FROM Orders WHERE CustomerId = @CustomerId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Order>(sql, new { CustomerId = id });
                return result;
            }
        }

        public int Update(Order entity)
        {
            var sql = "UPDATE Orders Set OrderNumber=@OrderNumber, CustomerId=@CustomerId, ShoppingCartId=@ShoppingCartId WHERE OrderId = @OrderId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public int GetLastId()
        {
            var sql = "SELECT MAX(OrderId) FROM Orders";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingle<int>(sql);
                return result;
            }
        }

    }
}
