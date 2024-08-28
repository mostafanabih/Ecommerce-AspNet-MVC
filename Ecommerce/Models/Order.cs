using Ecommerce.Data.Base;
using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class Order : IBaseEntity
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}