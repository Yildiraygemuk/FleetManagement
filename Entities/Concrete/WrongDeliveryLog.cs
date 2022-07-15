using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class WrongDeliveryLog : BaseEntity
    {
        public string PackageBarcode { get; set; }
        public string BagBarcode { get; set; }
        public string Message { get; set; }
        #region Enum
        public byte ExceptedDeliveryPoint { get; set; }
        public byte ActualDeliveryPoint { get; set; } 
        #endregion
    }
}
