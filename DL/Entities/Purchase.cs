using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int GiftId { get; set; }
        public Gift Gift { get; set; } = null!;

        public int Quantity { get; set; }

        public bool IsConfirmed { get; set; }  

        public DateTime Date { get; set; }
    }
}
