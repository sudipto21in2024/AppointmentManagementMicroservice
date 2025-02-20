using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid AppointmentId { get; set; }

        public Guid ServiceId { get; set; } // Add ServiceId

        public string ServiceName { get; set; } // Add ServiceName

        [Column(TypeName = "decimal(18, 2)")] // For currency
        public decimal Amount { get; set; }

        public string Status { get; set; } // "Pending", "Paid", "Refunded", etc.

        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }


        // ... other properties (e.g., payment method, transaction ID)
    }
}
