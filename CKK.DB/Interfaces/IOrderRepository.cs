using System;
using CKK.Logic.Models;

namespace CKK.DB.Interfaces
{
	public interface IOrderRepository : IGenericRepository<Order>
	{
		Order GetOrderByCustomerId(int id);
        int? GetLastId();
    }
}