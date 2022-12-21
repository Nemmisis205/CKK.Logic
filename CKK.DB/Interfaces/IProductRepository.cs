﻿using System;
using System.Collections.Generic;
using CKK.Logic.Models;

namespace CKK.DB.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetByName(string name);
    }
}
