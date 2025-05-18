using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using DL.Entities;

namespace BLL
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Donation,DonationDto>().ReverseMap();   
            CreateMap<Donor,DonorDto>().ReverseMap();                           
            CreateMap<Gift, GiftDto>().ReverseMap();        
            CreateMap<Purchase, PurchaseDto>().ReverseMap();        
        }
    }
}
