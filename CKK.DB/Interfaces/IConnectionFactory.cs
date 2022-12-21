using System;

namespace CKK.DB.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
