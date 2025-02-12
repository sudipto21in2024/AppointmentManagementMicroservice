using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCancelled { get; set; }
    }
}
