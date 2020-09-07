using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Common.Models
{
    public class CustomerPurchase
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int BookId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal Price { get; set; }

        public Customer Customer { get; set; }

        public Book Book { get; set; }
    }
}
