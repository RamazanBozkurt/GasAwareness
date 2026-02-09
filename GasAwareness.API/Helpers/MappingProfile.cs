using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.AgeGroup.Requests;
using GasAwareness.API.Models.AgeGroup.Responses;
using GasAwareness.API.Models.Category.Requests;
using GasAwareness.API.Models.Category.Responses;
using GasAwareness.API.Models.SubscriptionType.Requests;
using GasAwareness.API.Models.SubscriptionType.Responses;
using GasAwareness.API.Models.Survey.Responses;
using GasAwareness.API.Models.Video.Requests;
using GasAwareness.API.Models.Video.Responses;

namespace GasAwareness.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateVideoRequestDto, Video>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Path))
                .ReverseMap(); 

            CreateMap<Category, CategoryResponseDto>().ReverseMap();

            CreateMap<AgeGroup, AgeGroupResponseDto>().ReverseMap();

            CreateMap<SubscriptionType, SubscriptionTypeResponseDto>().ReverseMap();

            CreateMap<Video, VideoResponseDto>().ReverseMap();

            CreateMap<Video, VideoDetailResponseDto>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.VideoCategories.Select(x => x.Category)))
                .ForMember(dest => dest.AgeGroups, opt => opt.MapFrom(src => src.VideoAgeGroups.Select(x => x.AgeGroup)))
                .ForMember(dest => dest.SubscriptionTypes, opt => opt.MapFrom(src => src.VideoSubscriptionTypes.Select(x => x.SubscriptionType)))
                .ForMember(dest => dest.SurveyId, opt => opt.MapFrom(src => src.Surveys.FirstOrDefault().Id))
                .ReverseMap();

            CreateMap<Category, CreateCategoryRequestDto>().ReverseMap();

            CreateMap<AgeGroup, CreateAgeGroupRequestDto>().ReverseMap();

            CreateMap<SubscriptionType, CreateSubscriptionTypeRequestDto>().ReverseMap();

            CreateMap<Survey, SurveyResponseDto>().ReverseMap();

            CreateMap<SurveyOption, OptionResponseDto>().ReverseMap();

            CreateMap<SurveyQuestion, QuestionResponseDto>().ReverseMap();

            CreateMap<Survey, SurveyMainResponseDto>()
                .ForMember(dest => dest.QuestionCount, opt => opt.MapFrom(src => src.Questions.Count()))
                .ReverseMap();

            CreateMap<SurveyResult, UserSurveyListDto>()
                .ForMember(dest => dest.ResultId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.SurveyTitle, opt => opt.MapFrom(src => src.Survey.Title))
                .ForMember(dest => dest.CorrectCount, opt => opt.MapFrom(src => src.CorrectCount))
                .ForMember(dest => dest.WrongCount, opt => opt.MapFrom(src => src.WrongCount))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
                .ForMember(dest => dest.CompletedAt, opt => opt.MapFrom(src => src.CompletedAt))
                .ReverseMap();
        }
    }
}