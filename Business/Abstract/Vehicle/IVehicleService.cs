using Core.Utilities.Results;
using Entities.Dto;
using Entities.Vm;
using System;
using System.Linq;

namespace Business.Abstract
{
    public interface IVehicleService
    {
        IDataResult<IQueryable<VehicleVm>> GetListQueryable();
        IResult Add(VehicleDto vehicleDto);
    }
}
