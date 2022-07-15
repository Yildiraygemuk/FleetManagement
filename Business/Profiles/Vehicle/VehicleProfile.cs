using AutoMapper;
using Entities.Concrete;
using Entities.Dto;
using Entities.Vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class VehicleProfile:Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleVm>().ReverseMap();
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
        }
    }
}
