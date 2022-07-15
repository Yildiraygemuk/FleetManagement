using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using Entities.Vm;
using System;
using System.Linq;

namespace Business.Abstract
{
    public interface IPackageService
    {
        IDataResult<IQueryable<PackageVm>> GetListQueryable();
        IDataResult<IQueryable<BagInformationVm>> GetPackagesWithBagListQueryable();
        IDataResult<Package> Add(PackageDto packageDto);
        IDataResult<BagInformationDto> AddPackageToBag(BagInformationDto bagInformationDto);
        IDataResult<Bag> BagDeliveryRootCheck(string barcode, byte deliveryPoint);
        IDataResult<Package> PackageDeliveryRootCheck(string barcode, byte deliveryPoint);
        public void UpdateUnloadedPackageStatus(Package package);
        public void UpdateUnloadedBagStatus(Bag bag);
        IDataResult<DeliveryCheckDto> DeliveryCheck(DeliveryCheckDto deliveryCheckDto);
    }
}
