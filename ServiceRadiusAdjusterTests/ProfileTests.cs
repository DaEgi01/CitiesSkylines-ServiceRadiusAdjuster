using ServiceRadiusAdjuster.Model;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ServiceRadiusAdjusterTests;

public class ProfileTests
{
    [Fact]
    public void Combine_AddNewOptionItems()
    {
        var existingViewGroups = new List<ViewGroup>()
        {
            new ("vg1", 1, new List<OptionItem>()
            {
                new (ServiceType.Building, "oi1_1", "oi1_1", 10, 10, 100, 100),
                new (ServiceType.Building, "oi1_2", "oi1_2", 20, 20, 200, 200)
            }),
            new ("vg2", 2, new List<OptionItem>()
            {
                new (ServiceType.Building, "oi2_1", "oi2_1", 10, 10, 100, 100),
                new (ServiceType.Building, "oi2_2", "oi2_2", 20, 20, 200, 200)
            })
        };

        var profile = new Profile(existingViewGroups);

        var newViewGroups = new List<ViewGroup>()
        {
            new ("vg1", 1, new List<OptionItem>()
            {
                new (ServiceType.Building, "oi1_1", "oi1_1", 10, 10, 100, 100),
                new (ServiceType.Building, "oi1_2", "oi1_2", 20, 20, 200, 200),
                new (ServiceType.Building, "oi1_3", "oi1_3", 30, 30, 300, 300)
            }),
            new ("vg2", 1, new List<OptionItem>()
            {
                new (ServiceType.Building, "oi2_1", "oi2_1", 10, 10, 100, 100),
                new (ServiceType.Building, "oi2_2", "oi2_2", 20, 20, 200, 200),
                new (ServiceType.Building, "oi2_3", "oi2_3", 30, 30, 300, 300)
            })
        };

        var expectedViewGroups = new List<ViewGroup>()
        {
            new ("vg1", 1, new List<OptionItem>()
            {
                new (ServiceType.Building, "oi1_1", "oi1_1", 10, 10, 100, 100),
                new (ServiceType.Building, "oi1_2", "oi1_2", 20, 20, 200, 200),
                new (ServiceType.Building, "oi1_3", "oi1_3", 30, 30, 300, 300)
            }),
            new ("vg2", 1, new List<OptionItem>()
            {
                new (ServiceType.Building, "oi2_1", "oi2_1", 10, 10, 100, 100),
                new (ServiceType.Building, "oi2_2", "oi2_2", 20, 20, 200, 200),
                new (ServiceType.Building, "oi2_3", "oi2_3", 30, 30, 300, 300)
            })
        };

        var expected = new Profile(expectedViewGroups).ViewGroups.SelectMany(vg => vg.OptionItems).ToList();
        var actual = profile.Combine(newViewGroups).ViewGroups.SelectMany(vg => vg.OptionItems).ToList();

        actual.Should().BeEquivalentTo(expected);
    }
}