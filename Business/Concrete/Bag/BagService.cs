using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
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
    public class BagService : IBagService
    {
        private readonly IBagRepository _bagRepository;
        private readonly IMapper _mapper;

        public BagService(IBagRepository bagRepository, IMapper mapper)
        {
            _bagRepository = bagRepository;
            _mapper = mapper;
        }
        [ValidationAspect(typeof(BagValidator))]
        public IResult Add(BagDto bagDto)
        {
            var mappedDto = _mapper.Map<Bag>(bagDto);
            var addedData = _bagRepository.Add(mappedDto).Data;
            return new SuccessDataResult<Bag>(addedData);
        }

        public IDataResult<IQueryable<BagVm>> GetListQueryable()
        {
            var entityList = _bagRepository.GetAllQueryable();
            var dtoList = _mapper.ProjectTo<BagVm>(entityList);
            return new SuccessDataResult<IQueryable<BagVm>>(dtoList);
        }
    }
}
