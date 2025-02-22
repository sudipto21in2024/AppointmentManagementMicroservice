using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.DTO
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCancelled { get; set; }
        public string ServiceName { get; set; } // Add ServiceName
        public string CustomerName { get; set; } // Add CustomerName
        // ... other properties you need
    }

    public class AppointmentResponseDTO
    {
        public List<AppointmentDTO> Data { get; set; } = new List<AppointmentDTO>();
        public int TotalRecords { get; set; }
    }
}
