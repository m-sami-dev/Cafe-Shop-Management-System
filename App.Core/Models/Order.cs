using System;
using System.Collections.Generic;

namespace App.Core.Models
{
    public class Order
    {
        public string OrderId       { get; set; }
        public string CustomerId    { get; set; }
        public string CustomerName  { get; set; }
        public decimal TotalAmount  { get; set; }
        public string Status        { get; set; }
        public string Notes         { get; set; }
        public DateTime OrderDate   { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order()
        {
            OrderId      = string.Empty;
            CustomerId   = string.Empty;
            CustomerName = string.Empty;
            Status       = "Pending";
            Notes        = string.Empty;
            OrderDate    = DateTime.Now;
            Items        = new List<OrderItem>();
        }

        public override string ToString() => $"{OrderId} - {CustomerName} - Rs. {TotalAmount:N0}";
    }

    public class OrderItem
    {
        public string ItemId      { get; set; }
        public string OrderId     { get; set; }
        public string ProductId   { get; set; }
        public string ProductName { get; set; }
        public int Quantity       { get; set; }
        public decimal UnitPrice  { get; set; }
        public decimal SubTotal   => Quantity * UnitPrice;

        public OrderItem()
        {
            ItemId      = string.Empty;
            OrderId     = string.Empty;
            ProductId   = string.Empty;
            ProductName = string.Empty;
        }
    }
}
