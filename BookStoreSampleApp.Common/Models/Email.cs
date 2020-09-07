using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Common.Models
{
    public class Email
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime QueueDate { get; set; }

        public DateTime? SentDate { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Customer Customer { get; set; }
    }
}
