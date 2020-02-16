using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArthasPub.Models
{
    public class Order
    {
        //[Key]
        public int OrderId { get; set; }      
        public decimal Total { get; set; }
        public DateTime CreateDate { get; set; }
        [StringLength(128)]
        public String UserId { get; set; }
        //[ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }

}