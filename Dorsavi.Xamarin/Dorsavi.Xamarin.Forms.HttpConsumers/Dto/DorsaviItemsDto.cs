using System;
using System.Collections.Generic;
using System.Text;

namespace Dorsavi.Xamarin.Forms.HttpConsumers.Dto
{
    public class DorsaviItemsDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public Lazy<List<DorsaviPetItemsDto>> Pets { get; set; } //Only load Pets when needed 
    }
}
