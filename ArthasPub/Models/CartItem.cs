using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArthasPub.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }

    }


}