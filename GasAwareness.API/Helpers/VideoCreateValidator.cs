using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GasAwareness.API.Models.Video.Requests;

namespace GasAwareness.API.Helpers
{
    public class VideoCreateValidator : AbstractValidator<CreateVideoRequestDto>
    {
        public VideoCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Video başlığı boş olamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Path)
                .NotEmpty().WithMessage("Video url'i boş olamaz.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Video kategorisi boş olamaz.");

            RuleFor(x => x.AgeGroupId)
                .NotEmpty().WithMessage("Video yaş grubu boş olamaz.");

            RuleFor(x => x.SubscriptionTypeId)
                .NotEmpty().WithMessage("Video abonelik tipi boş olamaz.");
        }
    }
}