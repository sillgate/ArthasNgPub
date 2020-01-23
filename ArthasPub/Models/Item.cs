using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArthasPub.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string ItemImageUrl { get; set; }
        public byte[] InternalImage { get; set; }
        public bool Visible { get; set; }

        //public virtual List<CartItem> CartItem { get; set; }

    }
}