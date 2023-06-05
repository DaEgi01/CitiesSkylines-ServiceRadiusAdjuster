using System.Xml.Serialization;

namespace ServiceRadiusAdjuster.Configuration.v3;

[XmlRoot("OptionItem")]
public class OptionItemDto
{
    public string? SystemName { get; set; }
    public string? DisplayName { get; set; }
    public string? Type { get; set; }
    public int? Accumulation { get; set; }
    public int? AccumulationDefault { get; set; }
    public float? Radius { get; set; }
    public float? RadiusDefault { get; set; }
    public bool? Ignore { get; set; }
}