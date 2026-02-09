using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GasAwareness.API.Models.AgeGroup.Requests;

namespace GasAwareness.API.Helpers
{
    public class SubscriptionTypeCreateValidator : AbstractValidator<CreateAgeGroupRequestDto>
    {
        public SubscriptionTypeCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Abonelik tipi adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 200 karakter olabilir.");   
        }
    }
}