using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class DonorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        
    }
}
