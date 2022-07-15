using System.Collections.Generic;

namespace Entities.Dto
{
    public class DeliveryCheckDto
    {
        public string Plate { get; set; }
        public List<RouteDto> Route { get; set; }
    }
}
