using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArthasPub.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int? OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Cancel { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
        [StringLength(128)]
        public String UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Total")]
        public decimal Total
        {
            get
            {
                return Quantity * Price;
            }
        }
        public decimal Profit
        {
            get {
                return (Price - Item.Cost) * Quantity;
            }
        
        }

    }


}