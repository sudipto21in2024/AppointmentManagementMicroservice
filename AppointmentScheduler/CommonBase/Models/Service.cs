using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }
        public bool IsActive { get; set; }
    }
}
