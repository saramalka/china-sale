using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class DonationDto
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
      
        public int Amount { get; set; }
    }
}
