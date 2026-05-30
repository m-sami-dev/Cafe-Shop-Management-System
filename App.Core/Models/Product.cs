using System;

namespace App.Core.Models
{
    public class Product
    {
        public string ProductId   { get; set; }
        public string Name        { get; set; }
        public string Category    { get; set; }
        public decimal Price      { get; set; }
        public int StockQty       { get; set; }
        public string Description { get; set; }
        public bool IsAvailable   { get; set; }
        public DateTime CreatedAt { get; set; }

        public Product()
        {
            ProductId   = string.Empty;
            Name        = string.Empty;
            Category    = string.Empty;
            Description = string.Empty;
            IsAvailable = true;
            CreatedAt   = DateTime.Now;
        }

        public override string ToString() => $"{Name} - Rs. {Price:N0}";
    }
}
