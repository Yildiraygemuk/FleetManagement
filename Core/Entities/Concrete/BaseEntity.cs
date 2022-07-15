using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            this.IsActive = true;
        }
        public Guid Id { get; set; }
        public DateTime AddDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
