using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CACHIA_MIGUEL_EP.Models
{
    public class Quality
    {
        public int QualityId { get; set; }
        public String QualityName { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}