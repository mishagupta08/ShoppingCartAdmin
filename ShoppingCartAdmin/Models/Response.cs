using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public class Response
    {
        /// <summary>
        /// gets or sets status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// gets or sets response value
        /// </summary>
        public string ResponseValue { get; set; }
    }
}