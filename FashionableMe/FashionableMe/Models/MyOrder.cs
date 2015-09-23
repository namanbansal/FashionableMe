using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionableMe.Models
{
    public class MyOrder
    {
        public string UserID { get; set; }
        public string TransactionID { get; set; }
        public string ProductName { get; set; }
        public string SizeOfApparel { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }
}