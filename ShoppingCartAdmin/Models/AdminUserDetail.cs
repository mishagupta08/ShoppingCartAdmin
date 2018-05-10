using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public class AdminUserDetail
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}