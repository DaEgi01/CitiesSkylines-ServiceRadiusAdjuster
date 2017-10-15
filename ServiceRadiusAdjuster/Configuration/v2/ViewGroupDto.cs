using System.Collections.Generic;

namespace ServiceRadiusAdjuster.Configuration.v2
{
    public class ViewGroupDto
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public List<OptionItemDto> OptionItems { get; set; }
    }
}
