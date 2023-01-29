using System;
using CKK.Logic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CKK.DB.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
