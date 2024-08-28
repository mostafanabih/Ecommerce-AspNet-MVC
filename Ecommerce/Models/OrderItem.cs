﻿using Ecommerce.Data.Base;
using System;

namespace Ecommerce.Models
{
    public class OrderItem : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        // Navigational Property
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        
    }

}
