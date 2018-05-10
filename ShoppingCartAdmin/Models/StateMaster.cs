using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public class StateMaster
    {
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}