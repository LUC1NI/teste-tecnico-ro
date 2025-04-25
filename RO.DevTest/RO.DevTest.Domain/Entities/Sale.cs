using System;
using System.Collections.Generic;

namespace RO.DevTest.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }

    public class SaleItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SaleId { get; set; }
        public Sale Sale { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
