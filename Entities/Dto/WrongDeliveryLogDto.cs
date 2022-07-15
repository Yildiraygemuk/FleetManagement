using Entities.Concrete;

namespace Entities.Dto
{
    public class WrongDeliveryLogDto
    {
        public Package Package { get; set; }
        public Bag Bag { get; set; }
        public byte DeliveryPoint { get; set; }
        public string Message { get; set; }
    }
}
