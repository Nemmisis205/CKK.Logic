using System;
using CKK.Logic;

namespace CKK.DB.Interfaces
{
	public interface IOrderRepository : IGenericRepository<Order>
	{
		Order GetOrderByCustomerId(int id);
	}
}