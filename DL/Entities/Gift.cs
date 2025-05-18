using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public class Gift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
  
        public int PricePerTicket { get; set; }
        public int? WinnerId { get; set; }  
        public User? Winner { get; set; }
        public int? DonationId { get; set; } 
        public Donation Donation { get; set; }
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}
