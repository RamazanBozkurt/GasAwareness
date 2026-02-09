using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GasAwareness.API.Models.Category.Requests;

namespace GasAwareness.API.Helpers
{
    public class CategoryCreateValidator : AbstractValidator<CreateCategoryRequestDto>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 200 karakter olabilir.");   
        }
    }
}