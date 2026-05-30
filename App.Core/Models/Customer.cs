using System;

namespace App.Core.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FullName   { get; set; }
        public string Phone      { get; set; }
        public string Email      { get; set; }
        public string Address    { get; set; }
        public int LoyaltyPts   { get; set; }
        public DateTime CreatedAt { get; set; }

        public Customer()
        {
            CustomerId = string.Empty;
            FullName   = string.Empty;
            Phone      = string.Empty;
            Email      = string.Empty;
            Address    = string.Empty;
            CreatedAt  = DateTime.Now;
        }

        public override string ToString() => $"{FullName} ({Phone})";
    }
}
