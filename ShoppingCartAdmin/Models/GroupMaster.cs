using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public class GroupMaster
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}