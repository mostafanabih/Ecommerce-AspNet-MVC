using Ecommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Data.Services
{
    public interface IOrderServices
    {
       Task StoreOrderAsync(List<ShoppingCartItem> items, int userId);
        Task<List<Order>> GetOrderByUserIdAsync(int userId);    
    }
}
