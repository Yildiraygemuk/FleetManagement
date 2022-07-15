using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation.Vehicle;
using Core.Aspects;
using Core.Utilities.Results;
using DataAccess.Abstract.Repositories;
using Entities.Concrete;
using Entities.Dto;
using Entities.Vm;
using System;
using System.Linq;

namespace Business.Concrete
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }
        [ValidationAspect(typeof(VehicleValidator))]
        public IResult Add(VehicleDto vehicleDto)
        {
            var mappedDto = _mapper.Map<Vehicle>(vehicleDto);
            var addedData=_vehicleRepository.Add(mappedDto).Data;
            return new SuccessDataResult<Vehicle>(addedData);
        }
        public IDataResult<IQueryable<VehicleVm>> GetListQueryable()
        {
            var entityList = _vehicleRepository.GetAllQueryable();
            var dtoList = _mapper.ProjectTo<VehicleVm>(entityList);
            return new SuccessDataResult<IQueryable<VehicleVm>>(dtoList);
        }
    }
}
