using System;
using System.Collections.Generic;

namespace Dorsavi.Xamarin.Forms.RemoteServer.Models
{
    public class DorsaviItemsDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public List<DorsaviPetItemsDto> Pets { get; set; }
        public DorsaviItemsDto()
        {
            if (Pets == null)
                Pets = new List<DorsaviPetItemsDto>();
        }
    }
}
