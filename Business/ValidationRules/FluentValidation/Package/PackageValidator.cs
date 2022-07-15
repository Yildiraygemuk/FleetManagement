using Entities.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PackageValidator : AbstractValidator<PackageDto>
    {
        public PackageValidator()
        {
            RuleFor(x => x.Barcode).NotNull().NotEmpty();
            RuleFor(x => x.DeliveryPointForUnloading).NotNull().NotEmpty();
            RuleFor(x => x.VolumetricWeight).NotNull().NotEmpty();
        }
    }
}
