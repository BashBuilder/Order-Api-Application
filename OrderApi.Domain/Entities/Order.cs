using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApi.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public required int ProductId { get; set; }

        public required int ClientId { get; set; }

        public required int PurchaseQuantity { get; set; }

        public DateTime OrderedDate { get; set; } = DateTime.UtcNow;
    }
}
