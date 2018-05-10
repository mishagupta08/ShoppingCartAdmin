using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public partial class OrderDetail
    {
        public string OrderNo { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string ManufacturerComapany { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string MobileNo { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<decimal> Wallet { get; set; }
        public string OnlineDD { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string DDno { get; set; }
        public Nullable<decimal> DDAmount { get; set; }
        public Nullable<System.DateTime> DDDate { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public string StateName { get; set; }
        public decimal OrderQuantity { get; set; }
        public Int32 OrderItems { get; set; }
        public IList<ProductOrderDetail> orderProduct { get; set; }
    }

    public partial class ProductOrderDetail
    {
        public Nullable<decimal> Amount { get; set; }
        public string ProductName { get; set; }
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string OrderId { get; set; }
    }
}