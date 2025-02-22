using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.DTO
{
    public class ServiceDTO
    {
        public Guid Id { get; set; }  // Keep as Guid in DTO
        public string Name { get; set; }
        public string Category { get; set; } // Add Category
        public decimal Price { get; set; } // Keep as decimal in DTO
        public int Duration { get; set; } // Add Duration
        public string Provider { get; set; } // Provider's name
        public bool IsActive { get; set; } // Add IsActive
    }

    public class ServiceResponseDTO // For the overall response
    {
        public List<ServiceDTO> Data { get; set; } = new List<ServiceDTO>();
        public int TotalRecords { get; set; }
    }
}
