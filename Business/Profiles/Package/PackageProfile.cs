using AutoMapper;
using Entities.Concrete;
using Entities.Dto;
using Entities.Enums;
using Entities.Vm;

namespace Business.Profiles
{
    public class PackageProfile:Profile
    {
        public PackageProfile()
        {
            CreateMap<Package, PackageVm>()
                .ForMember(dest => dest.PackageStatus,
                   act => act.MapFrom(src => (EnumPackageStatus)src.PackageStatus))
                 .ForMember(dest => dest.DeliveryPointForUnloading,
                   act => act.MapFrom(src => (EnumDeliveryPoint)src.DeliveryPointForUnloading));

            CreateMap<Package, PackageDto>().ReverseMap()
                 .ForMember(dest => dest.PackageStatus,
                   act => act.MapFrom(src => (byte)src.PackageStatus))
                 .ForMember(dest => dest.DeliveryPointForUnloading,
                   act => act.MapFrom(src => (byte)src.DeliveryPointForUnloading));

            CreateMap<Package, BagInformationVm>()
              .ForMember(dest => dest.Barcode,
                 act => act.MapFrom(src => src.Barcode))
               .ForMember(dest => dest.BagBarcode,
                 act => act.MapFrom(src => src.Bag.Barcode));
        }
    }
}
