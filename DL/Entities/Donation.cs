using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public class Donation
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public Donor Donor { get; set; }   
      public ICollection<Gift> Gifts { get; set; } = new List<Gift>();
        public int Amount { get; set; }
    }
}
