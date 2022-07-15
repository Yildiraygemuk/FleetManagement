using Core.DataAccess.EntityFramework.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract.Repositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
    }
}
