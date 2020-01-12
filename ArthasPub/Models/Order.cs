using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArthasPub.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual List<CartItem> CartItem { get; set; }
    }

}