using Entities.Enums;
using System.Collections.Generic;

namespace Entities.Vm
{
    public class BagVm
    {
        public string Barcode { get; set; }
        public List<PackageVm> Packages { get; set; }
        public EnumBagStatus BagStatus { get; set; }
        public byte BagStatusValue { get; set; }
        public EnumDeliveryPoint DeliveryPointForUnloading { get; set; }
        public byte DeliveryPointForUnloadingValue { get; set; }
    }
}
