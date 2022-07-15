using Core.Utilities.Results;
using Entities.Dto;
using Entities.Vm;
using System;
using System.Linq;

namespace Business.Abstract
{
    public interface IBagService
    {
        IDataResult<IQueryable<BagVm>> GetListQueryable();
        IResult Add(BagDto bagDto);
    }
}
