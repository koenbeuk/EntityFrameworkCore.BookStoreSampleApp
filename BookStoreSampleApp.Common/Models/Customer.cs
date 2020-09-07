using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Common.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public string DisplayName { get; set; }

        public DateTime? SignupDate { get; set; }

        public ICollection<CustomerPurchase> Purchases { get; set; }
    }
}
