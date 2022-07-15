using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Dto
{
    public class RouteDto
    {
        public byte DeliveryPoint { get; set; }
        public List<DeliveryDto> Deliveries { get; set; }
    }
    public class DeliveryDto
    {
        public string Barcode { get; set; }
        public byte Status { get; set; }
    }
}
