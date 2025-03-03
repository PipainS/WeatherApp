using AutoMapper;
using DynamicSun.Weather.Application.Models;
using DynamicSun.Weather.Domain.Entities;

namespace DynamicSun.Weather.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WeatherData, WeatherDataModel>();

            // Добавьте другие маппинги по необходимости
        }
    }
}
