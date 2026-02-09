using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GasAwareness.API.Models.AgeGroup.Requests;
using GasAwareness.API.Models.Category.Requests;

namespace GasAwareness.API.Helpers
{
    public class AgeGroupCreateValidator : AbstractValidator<CreateAgeGroupRequestDto>
    {
        public AgeGroupCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Yaş grubu adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 200 karakter olabilir.");   

            RuleFor(x => x.MinAge)
                .NotEmpty().WithMessage("Yaş grubu adı boş olamaz.")
                .GreaterThan(1).WithMessage("Minimum yaş en az 1 olmalı");

            RuleFor(x => x.MaxAge)
                .NotEmpty().WithMessage("Yaş grubu adı boş olamaz.")
                .LessThan(120).WithMessage("Maksimum yaş en fazla 120 olmalı");
        }
    }
}