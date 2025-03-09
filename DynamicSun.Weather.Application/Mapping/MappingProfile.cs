using AutoMapper;
using DynamicSun.Weather.Application.Constants;
using DynamicSun.Weather.Application.Models;
using DynamicSun.Weather.Domain.Entities;

namespace DynamicSun.Weather.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WeatherData, WeatherDataModel>()
                .ForMember(dest => dest.WeatherDateTime, opt => opt.MapFrom(src =>
                    TimeZoneInfo.ConvertTimeFromUtc(src.WeatherDateTime,
                        TimeZoneInfo.FindSystemTimeZoneById(TimeZoneConstants.RussianStandardTime))));
        }
    }
}
