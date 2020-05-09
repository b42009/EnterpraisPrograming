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
        [Required]
        public int ItemTypeID { get; set; }
      
        public ItemType ItemType { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Quantity { get; set; }
        [Required]
        public int Qualityid { get; set; }
        public Quality Quality { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public float Price { get; set; }
       
    }
}