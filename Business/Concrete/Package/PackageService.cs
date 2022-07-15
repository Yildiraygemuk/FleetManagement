using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects;
using Core.Utilities.Results;
using Core.Utilities.Results.ErrorDataResult;
using DataAccess.Abstract.Repositories;
using Entities.Concrete;
using Entities.Dto;
using Entities.Enums;
using Entities.Vm;
using System.Linq;

namespace Business.Concrete
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IBagRepository _bagRepository;
        private readonly IWrongDeliveryLogRepository _wrongDeliveryLogRepository;
        private readonly IMapper _mapper;

        public PackageService(IPackageRepository packageRepository, IBagRepository bagRepository, IWrongDeliveryLogRepository wrongDeliveryLogRepository, IMapper mapper)
        {
            _packageRepository = packageRepository;
            _bagRepository = bagRepository;
            _wrongDeliveryLogRepository = wrongDeliveryLogRepository;
            _mapper = mapper;
        }
        [ValidationAspect(typeof(PackageValidator))]
        public IDataResult<Package> Add(PackageDto packageDto)
        {
            var mappedDto = _mapper.Map<Package>(packageDto);
            var addedData = _packageRepository.Add(mappedDto).Data;
            return new SuccessDataResult<Package>(addedData);
        }

        public IDataResult<IQueryable<PackageVm>> GetListQueryable()
        {
            var entityList = _packageRepository.GetAllQueryable();
            var dtoList = _mapper.ProjectTo<PackageVm>(entityList);
            return new SuccessDataResult<IQueryable<PackageVm>>(dtoList);
        }
        public IDataResult<IQueryable<BagInformationVm>> GetPackagesWithBagListQueryable()
        {
            var entityList = _packageRepository.GetAllQueryable().Where(x => x.BagId != null);
            var dtoList = _mapper.ProjectTo<BagInformationVm>(entityList);
            return new SuccessDataResult<IQueryable<BagInformationVm>>(dtoList);
        }
        public IDataResult<Bag> BagDeliveryRootCheck(string barcode, byte deliveryPoint)
        {
            var bag = _bagRepository.Get(x => x.Barcode == barcode);
            bag.BagStatus = (byte)EnumBagStatus.Loaded;

            if (deliveryPoint != (byte)EnumDeliveryPoint.Branch && bag.DeliveryPointForUnloading == deliveryPoint)
            {
                UpdateUnloadedBagStatus(bag);
            }
            else
            {
                WrongDeliveryLogDto wrongDeliveryLogDto = new WrongDeliveryLogDto()
                {
                    Bag = bag,
                    DeliveryPoint = deliveryPoint,
                    Message = WrongDeliveryMessageString.WrongPackagePoint
                };
                AddWrongBagDeliveryLog(wrongDeliveryLogDto);
            }

            var packages = _packageRepository.GetAllQueryable(x => x.Bag.Barcode == barcode);
            if (packages.Any() && deliveryPoint != (byte)EnumDeliveryPoint.Branch)
            {
                foreach (var package in packages.ToList())
                {
                    PackageDeliveryRootCheck(package.Barcode, deliveryPoint);
                }
            }
            return new SuccessDataResult<Bag>(bag);
        }
        public void AddWrongPackageDeliveryLog(WrongDeliveryLogDto wrongDeliveryLogDto)
        {
            WrongDeliveryLog wrongDeliveryLog = new WrongDeliveryLog()
            {
                BagBarcode = wrongDeliveryLogDto.Package.Bag?.Barcode,
                PackageBarcode = wrongDeliveryLogDto.Package.Barcode,
                ExceptedDeliveryPoint = wrongDeliveryLogDto.DeliveryPoint,
                ActualDeliveryPoint = wrongDeliveryLogDto.Package.DeliveryPointForUnloading,
                Message = wrongDeliveryLogDto.Message
            };
            _wrongDeliveryLogRepository.Add(wrongDeliveryLog);
        }
        public void AddWrongBagDeliveryLog(WrongDeliveryLogDto wrongDeliveryLogDto)
        {
            WrongDeliveryLog wrongDeliveryLog = new WrongDeliveryLog()
            {
                BagBarcode = wrongDeliveryLogDto.Bag.Barcode,
                ExceptedDeliveryPoint = wrongDeliveryLogDto.DeliveryPoint,
                ActualDeliveryPoint = wrongDeliveryLogDto.Bag.DeliveryPointForUnloading,
                Message = wrongDeliveryLogDto.Message
            };
            _wrongDeliveryLogRepository.Add(wrongDeliveryLog);
        }
        public IDataResult<Package> PackageDeliveryRootCheck(string barcode, byte deliveryPoint)
        {
            var package = _packageRepository.Get(x => x.Barcode == barcode);
            package.PackageStatus = (byte)EnumPackageStatus.Loaded;
            switch (deliveryPoint)
            {
                case (byte)EnumDeliveryPoint.Branch:
                    if (package.BagId == null && package.DeliveryPointForUnloading == deliveryPoint)
                    {
                        UpdateUnloadedPackageStatus(package);
                    }
                    else
                    {
                        WrongDeliveryLogDto wrongDeliveryLogDto = new WrongDeliveryLogDto()
                        {
                            Package = package,
                            DeliveryPoint = deliveryPoint,
                            Message = WrongDeliveryMessageString.WrongPackagePoint
                        };
                        AddWrongPackageDeliveryLog(wrongDeliveryLogDto);
                    }
                    break;
                case (byte)EnumDeliveryPoint.DistributionCenter:
                    if (package.DeliveryPointForUnloading == deliveryPoint)
                    {
                        UpdateUnloadedPackageStatus(package);
                    }
                    else
                    {
                        WrongDeliveryLogDto wrongDeliveryLogDto = new WrongDeliveryLogDto()
                        {
                            Package = package,
                            DeliveryPoint = deliveryPoint,
                            Message = WrongDeliveryMessageString.WrongPackagePoint
                        };
                        AddWrongPackageDeliveryLog(wrongDeliveryLogDto);
                    }
                    break;
                case (byte)EnumDeliveryPoint.TransferCenter:
                    if (package.BagId != null && package.DeliveryPointForUnloading == deliveryPoint)
                    {
                        UpdateUnloadedPackageStatus(package);
                        UpdateUnloadedBagStatus(_bagRepository.GetById(package.BagId.Value));
                    }
                    else if(package.BagId == null && package.DeliveryPointForUnloading == deliveryPoint)
                    {
                        WrongDeliveryLogDto wrongDeliveryLogDto = new WrongDeliveryLogDto()
                        {
                            Package = package,
                            DeliveryPoint = deliveryPoint,
                            Message = WrongDeliveryMessageString.BaglessPackageError
                        };
                        AddWrongPackageDeliveryLog(wrongDeliveryLogDto);
                    }
                    else
                    {
                        WrongDeliveryLogDto wrongDeliveryLogDto = new WrongDeliveryLogDto()
                        {
                            Package = package,
                            DeliveryPoint = deliveryPoint,
                            Message = WrongDeliveryMessageString.WrongPackagePoint
                        };
                        AddWrongPackageDeliveryLog(wrongDeliveryLogDto);
                    }
                    break;
            }
            return new SuccessDataResult<Package>(package);
        }
        public void UpdateUnloadedPackageStatus(Package package)
        {
            package.PackageStatus = (byte)EnumPackageStatus.Unloaded;
            _packageRepository.Update(package);
        }
        public void UpdateUnloadedBagStatus(Bag bag)
        {
            bag.BagStatus = (byte)EnumBagStatus.Unloaded;
            _bagRepository.Update(bag);
        }
        public IDataResult<DeliveryCheckDto> DeliveryCheck(DeliveryCheckDto deliveryCheckDto)
        {

            foreach (var route in deliveryCheckDto.Route)
            {

                foreach (var deliveryInfo in route.Deliveries)
                {
                    if (deliveryInfo.Barcode.StartsWith("C"))
                    {
                        var bagDeliveryControl = BagDeliveryRootCheck(deliveryInfo.Barcode, route.DeliveryPoint);
                        deliveryInfo.Status = bagDeliveryControl.Data.BagStatus;
                    }
                    else
                    {
                        var packageDeliveryControl = PackageDeliveryRootCheck(deliveryInfo.Barcode, route.DeliveryPoint);
                        deliveryInfo.Status = packageDeliveryControl.Data.PackageStatus;
                    }
                }
            }

            return new SuccessDataResult<DeliveryCheckDto>(deliveryCheckDto);
        }

        public IDataResult<BagInformationDto> AddPackageToBag(BagInformationDto bagInformationDto)
        {
            var bag = _bagRepository.Get(x => x.Barcode == bagInformationDto.BagBarcode);
            var package = _packageRepository.Get(x => x.Barcode == bagInformationDto.PackageBarcode);
            if (bag != null && bag.DeliveryPointForUnloading == package.DeliveryPointForUnloading)
            {
                package.BagId = bag.Id;
                package.PackageStatus = (byte)EnumPackageStatus.LoadedIntoBag;
                _packageRepository.Update(package);
                return new SuccessDataResult<BagInformationDto>(bagInformationDto);
            }
            return new ErrorDataResult<BagInformationDto>(bagInformationDto, "Bag is not found");
        }
    }
}
