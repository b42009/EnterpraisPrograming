using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CACHIA_MIGUEL_EP.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public virtual ICollection<ItemType> ItemType { get; set; }
    }
}