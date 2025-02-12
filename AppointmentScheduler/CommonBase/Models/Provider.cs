using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Models
{
    public class Provider
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string Specialization { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
