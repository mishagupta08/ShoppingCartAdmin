using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public class OfferImageMaster
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string BannerUrl { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}