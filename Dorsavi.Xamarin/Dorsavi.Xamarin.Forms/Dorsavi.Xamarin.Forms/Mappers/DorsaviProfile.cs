using AutoMapper;
using Dorsavi.Xamarin.Forms.HttpConsumers.Dto;
using Dorsavi.Xamarin.Forms.Models;

namespace Dorsavi.Xamarin.Forms.Mappers
{
    /// <summary>
    /// Configure AutoMapper to perform linq projection & abstract mapping logic away from the main source code
    /// </summary>
    internal class DorsaviProfile : Profile
    {
        public DorsaviProfile()
        {
            InitializeToDto();
            InitializeFromDto();
        }

        public void InitializeToDto()
        {
            this.CreateMap<DorsaviItems, DorsaviItemsDto>();
        }

        public void InitializeFromDto()
        {
            this.CreateMap<DorsaviItemsDto, DorsaviItems>();
        }
    }
}
