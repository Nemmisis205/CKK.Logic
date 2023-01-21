using CKK.DB.Interfaces;
using CKK.DB.UOW;
using CKK.Logic.Models;

namespace CKK.Online.Models
{
    public class ShopModel
    {
        public Order Order { get; set; }
        public IUnitOfWork UOW { get; set; }
    }
}
