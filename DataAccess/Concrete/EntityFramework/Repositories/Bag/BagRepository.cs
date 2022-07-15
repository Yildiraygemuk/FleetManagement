using Core.DataAccess.EntityFramework.Concrete;
using DataAccess.Abstract.Repositories;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class BagRepository : GenericRepository<Bag>, IBagRepository
    {
        public BagRepository(FleetManagementContext context) : base(context)
        {
        }
    }
}
