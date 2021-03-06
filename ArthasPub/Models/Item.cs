﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
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
        public string ItemImageUrl { get; set; } = "~/Content/img/nophoto.png";
        [NotMapped]
        [JsonIgnore]
        [IgnoreDataMember]
        public HttpPostedFileBase Upload { get; set; }
        public bool Disable { get; set; }
    }
}