using AutoMapper;

namespace Dorsavi.Xamarin.Forms.Mappers
{
    using Dorsavi.Xamarin.Forms.Models;
    using Dorsavi.Xamarin.Forms.RemoteServer.Models;
 
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
        }

        private void InitializeFromDto()
        {
            this.CreateMap<DorsaviItemsDto, DorsaviItems>();
        }
    }
}
