using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common.enums;

namespace DL.Entities
{
    
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int GiftId { get; set; }
        public Gift Gift { get; set; }

        public DateTime OrderDate { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.Pending;

    }
}
