using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Bag : BaseEntity
    {
        public string Barcode { get; set; }
        public List<Package> Packages { get; set; }

        #region Enum
        public byte DeliveryPointForUnloading { get; set; }
        public byte BagStatus { get; set; }
        #endregion Enum
    }
}
