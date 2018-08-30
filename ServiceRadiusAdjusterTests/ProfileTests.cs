using ServiceRadiusAdjuster.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ServiceRadiusAdjusterTests
{
    public class ProfileTests
    {
        [Fact(DisplayName = nameof(Combine_AddNewOptionItems))]
        public void Combine_AddNewOptionItems()
        {
            var existingOptionItems1 = new List<OptionItem>()
            {
                new OptionItem(ServiceType.Building, "oi1_1", "oi1_1", 10, 10, 100, 100),
                new OptionItem(ServiceType.Building, "oi1_2", "oi1_2", 20, 20, 200, 200)
            };

            var existingOptionItems2 = new List<OptionItem>()
            {
                new OptionItem(ServiceType.Building, "oi2_1", "oi2_1", 10, 10, 100, 100),
                new OptionItem(ServiceType.Building, "oi2_2", "oi2_2", 20, 20, 200, 200)
            };

            var existingViewGroups = new List<ViewGroup>()
            {
                new ViewGroup("vg1", 1, existingOptionItems1),
                new ViewGroup("vg2", 2, existingOptionItems2)
            };

            var profile = new Profile(existingViewGroups);

            var newOptionItems1 = new List<OptionItem>()
            {
                new OptionItem(ServiceType.Building, "oi1_1", "oi1_1", 10, 10, 100, 100),
                new OptionItem(ServiceType.Building, "oi1_2", "oi1_2", 20, 20, 200, 200),
                new OptionItem(ServiceType.Building, "oi1_3", "oi1_3", 30, 30, 300, 300)
            };

            var newOptionItems2 = new List<OptionItem>()
            {
                new OptionItem(ServiceType.Building, "oi2_1", "oi2_1", 10, 10, 100, 100),
                new OptionItem(ServiceType.Building, "oi2_2", "oi2_2", 20, 20, 200, 200),
                new OptionItem(ServiceType.Building, "oi2_3", "oi2_3", 30, 30, 300, 300)
            };

            var newViewGroups = new List<ViewGroup>()
            {
                new ViewGroup("vg1", 1, newOptionItems1),
                new ViewGroup("vg2", 1, newOptionItems2)
            };

            var expectedOptionItems1 = new List<OptionItem>()
            {
                new OptionItem(ServiceType.Building, "oi1_1", "oi1_1", 10, 10, 100, 100),
                new OptionItem(ServiceType.Building, "oi1_2", "oi1_2", 20, 20, 200, 200),
                new OptionItem(ServiceType.Building, "oi1_3", "oi1_3", 30, 30, 300, 300)
            };

            var expectedOptionItems2 = new List<OptionItem>()
            {
                new OptionItem(ServiceType.Building, "oi2_1", "oi2_1", 10, 10, 100, 100),
                new OptionItem(ServiceType.Building, "oi2_2", "oi2_2", 20, 20, 200, 200),
                new OptionItem(ServiceType.Building, "oi2_3", "oi2_3", 30, 30, 300, 300)
            };

            var expectedViewGroups = new List<ViewGroup>()
            {
                new ViewGroup("vg1", 1, newOptionItems1),
                new ViewGroup("vg2", 1, newOptionItems2)
            };

            var expected = new Profile(expectedViewGroups).ViewGroups.SelectMany(vg => vg.OptionItems);
            var actual = profile.Combine(newViewGroups).ViewGroups.SelectMany(vg => vg.OptionItems);

            var expectedList = expected.ToList();
            var actualList = actual.ToList();

            for (int i = 0; i < expectedList.Count; i++)
            {
                var expectedItem = expectedList[i];
                var actualItem = actualList[i];

                //TODO find out what the issue with the execution timer is
                Assert.Equal(expectedItem, actualItem);
            }

            Assert.Equal(expected, actual);
        }
    }
}
