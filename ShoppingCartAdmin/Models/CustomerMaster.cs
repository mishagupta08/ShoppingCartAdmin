using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public class CustomerMaster
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public string StateName { get; set; }
        public string MobileNo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}