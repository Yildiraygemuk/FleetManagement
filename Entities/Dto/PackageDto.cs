using Entities.Enums;
using System;

namespace Entities.Dto
{
    public class PackageDto
    {
        public PackageDto()
        {
            this.PackageStatus = EnumPackageStatus.Created;
        }
        public Guid Id { get; set; }
        public string Barcode { get; set; }
        public short VolumetricWeight { get; set; }
        public Guid? BagId { get; set; }
        public EnumPackageStatus PackageStatus { get; set; }
        public EnumDeliveryPoint DeliveryPointForUnloading { get; set; }
    }
}
