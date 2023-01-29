using System;
using System.Collections.Generic;
using CKK.Logic.Models;
using System.Threading.Tasks;

namespace CKK.DB.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetByName(string name);
    }
}
