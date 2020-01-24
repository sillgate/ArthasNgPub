using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArthasPub.Models
{
    public class Sale
    {
        public List <CartItem> CartItem { get; set; }
        public List <Item> Item { get; set; }
        public List <Order> Order { get; set; }
        //public double Total        {       }
    }
}