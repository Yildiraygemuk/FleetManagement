using Entities.Enums;
using System;

namespace Entities.Vm
{
    public class PackageVm
    {
        public string Barcode { get; set; }
        public short VolumetricWeight { get; set; }
        public Guid BagId { get; set; }
        public EnumPackageStatus PackageStatus { get; set; }
        public EnumDeliveryPoint DeliveryPointForUnloading { get; set; }
    }
}
