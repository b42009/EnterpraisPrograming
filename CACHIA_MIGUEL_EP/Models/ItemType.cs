using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CACHIA_MIGUEL_EP.Models
{
    public class ItemType
    {
        [Key]
        public int ItemTypeID { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public String Name { get; set; }
        public String Image { get; set; }
        public virtual ICollection<Item> Item { get; set; }
    }
}