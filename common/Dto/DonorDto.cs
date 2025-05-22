using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace common.Dto
{
    public class DonorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool ShowMe { get; set; } = true;
    }
}
