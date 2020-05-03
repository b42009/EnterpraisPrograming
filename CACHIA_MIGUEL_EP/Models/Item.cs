using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CACHIA_MIGUEL_EP.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public String owner { get; set; }
        public int ItemTypeID { get; set; }
        public ItemType ItemType { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Quantity { get; set; }
        public String Quality { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public float Price { get; set; }
       
    }
}