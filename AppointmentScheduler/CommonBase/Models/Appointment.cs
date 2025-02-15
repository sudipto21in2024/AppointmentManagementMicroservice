using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Models
{
    public class Appointment
    {
        //public Guid Id { get; set; }
        //public DateTime StartTime { get; set; }
        //public Guid ServiceId { get; set; }
        //public Service Service { get; set; }
        //public Guid CustomerId { get; set; }
        //public User Customer { get; set; }
        //public bool IsConfirmed { get; set; }
        //public bool IsCancelled { get; set; }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; } = null!;

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public User Customer { get; set; } = null!;

        public bool IsConfirmed { get; set; }

        public bool IsCancelled { get; set; }
    }
}
