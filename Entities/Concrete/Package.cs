using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Package: BaseEntity
    {
        public string Barcode { get; set; }
        public short VolumetricWeight { get; set; }
        public Guid? BagId { get; set; }
        public Bag Bag { get; set; }

        #region Enum
        public byte DeliveryPointForUnloading { get; set; }
        public byte PackageStatus { get; set; }
        #endregion Enum
    }
}
