using AutoMapper;

namespace Dorsavi.Xamarin.Forms.Mappers
{
    using Dorsavi.Xamarin.Forms.Models;
    using Dorsavi.Xamarin.Forms.RemoteServer.Models;
    using Dorsavi.Xamarin.Forms.ViewModels.Collections;

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

        private void InitializeToDto()
        {
            this.CreateMap<DorsaviItems, DorsaviItemsDto>();
            this.CreateMap<DorsaviPetItems, DorsaviPetItemsDto>();
        }

        private void InitializeFromDto()
        {
            this.CreateMap<DorsaviItemsDto, DorsaviItems>();
            this.CreateMap<DorsaviPetItemsDto, DorsaviPetItems>();
            this.CreateMap<DorsaviItems, DorsaviListItemViewModel>();
        }
    }
}
