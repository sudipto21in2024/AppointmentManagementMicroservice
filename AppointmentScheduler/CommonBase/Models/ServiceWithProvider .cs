using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Models
{
    public class ServiceWithProvider // Data Transfer Object (DTO)
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProviderName { get; set; }
        public string ProviderEmail { get; set; }
        // ... any other properties you need
    }
}
