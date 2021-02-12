using System;
using System.Collections.Generic;

namespace Dorsavi.Xamarin.Forms.RemoteServer.Models
{
    public class DorsaviItemsDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public Lazy<List<DorsaviPetItemsDto>> Pets { get; set; } //Only load Pets when needed 
    }
}
