using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Models
{
    public class Service
    {
        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public decimal Price { get; set; }
        //public int DurationInMinutes { get; set; }
        //public Guid ProviderId { get; set; }
        //public Provider Provider { get; set; }
        //public bool IsActive { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        [Required]
        public Guid ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
    }
}
