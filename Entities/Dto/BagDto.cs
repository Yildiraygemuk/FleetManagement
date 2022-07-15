using Entities.Enums;
using Entities.Vm;
using System;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class BagDto
    {
        public BagDto()
        {
            this.BagStatus = EnumBagStatus.Created;
        }
        public Guid Id { get; set; }
        public string Barcode { get; set; }
        public List<PackageVm> Packages { get; set; }
        public EnumBagStatus BagStatus { get; set; }
        public byte BagStatusValue { get; set; }
        public EnumDeliveryPoint DeliveryPointForUnloading { get; set; }
        public byte DeliveryPointForUnloadingValue { get; set; }
    }
}
