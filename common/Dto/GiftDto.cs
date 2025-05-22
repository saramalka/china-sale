using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace common.Dto
{
    public class GiftDto
    {
        public string GiftName { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public int DonorId { get; set; }
        public string? Details { get; set; }
        public int? WinnerId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
