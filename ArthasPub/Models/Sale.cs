using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArthasPub.Models
{
    public class Sale
    {
        public ICollection<Item> Item { get; set; }
        public ICollection<ApplicationUser> User { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<CartItem> CartItem { get; set; }

    }
}