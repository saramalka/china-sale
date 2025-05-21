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
            CreateMap<TicketDto,Donation>()
                .ForMember(d=>d.Gifts,o=>o.Ignore())
                .ForMember(d=>d.Donor,o=>o.Ignore())    
                .ReverseMap();
            CreateMap<DonorDto, Donor>().ForMember(dest => dest.Donations, opt => opt.Ignore()).ReverseMap();

            CreateMap<Gift, GiftDto>().ReverseMap()
                .ForMember(dest=>dest.Winner,opt=>opt.Ignore())
                .ForMember(dest=>dest.Purchases,opt=>opt.Ignore())
                .ForMember(d=>d.Donation,opt=>opt.Ignore()) 
                .ReverseMap();        
            CreateMap<PurchaseDto, Purchase>()
                .ForMember(d=>d.User,o=>o.Ignore())
                .ForMember(d=>d.Gift,o=>o.Ignore())
                .ReverseMap();        
        }
    }
}
