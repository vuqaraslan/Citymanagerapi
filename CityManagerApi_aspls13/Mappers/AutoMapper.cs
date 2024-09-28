using AutoMapper;
using CityManagerApi_aspls13.Dtos;
using CityManagerApi_aspls13.Entities;

namespace CityManagerApi_aspls13.Mappers
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, option =>
                {
                    option.MapFrom(src => src.CityImages.FirstOrDefault(cImg => cImg.IsMain).Url);
                });

            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
