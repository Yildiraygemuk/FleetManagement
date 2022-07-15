using Entities.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BagValidator : AbstractValidator<BagDto>
    {
        public BagValidator()
        {
            RuleFor(x => x.Barcode).NotNull();
            RuleFor(x => x.DeliveryPointForUnloading).NotNull();
        }
    }
}
