using AutoMapper;
using Entities.Concrete;
using Entities.Dto;
using Entities.Enums;
using Entities.Vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class BagProfile:Profile
    {
        public BagProfile()
        {
            CreateMap<Bag, BagVm>()
                 .ForMember(dest => dest.BagStatus,
                   act => act.MapFrom(src => (EnumBagStatus)src.BagStatus))
                 .ForMember(dest => dest.DeliveryPointForUnloading,
                   act => act.MapFrom(src => (EnumBagStatus)src.DeliveryPointForUnloading));
            CreateMap<Bag, BagDto>().ReverseMap()
                .ForMember(dest => dest.BagStatus,
                   act => act.MapFrom(src => (byte)src.BagStatus))
                 .ForMember(dest => dest.DeliveryPointForUnloading,
                   act => act.MapFrom(src => (byte)src.DeliveryPointForUnloading));
        }
    }
}
