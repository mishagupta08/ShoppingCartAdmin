using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public class BrandMaster
    {
        public string BrandId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}