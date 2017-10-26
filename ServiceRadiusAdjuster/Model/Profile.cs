﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ServiceRadiusAdjuster.Model
{
    public class Profile
    {
        private readonly List<ViewGroup> viewGroups;

        public Profile()
        {
            this.viewGroups = new List<ViewGroup>();
        }

        public Profile(List<ViewGroup> viewGroups)
        {
            if (viewGroups == null) throw new ArgumentNullException(nameof(viewGroups));

            this.viewGroups = viewGroups;
        }

        public ReadOnlyCollection<ViewGroup> ViewGroups => viewGroups.AsReadOnly();

        /// <summary>
        /// Adds new OptionItems into the appropriate group.
        /// </summary>
        public Profile Combine(IEnumerable<ViewGroup> newViewGroups)
        {
            var combinedViewGroups = new List<ViewGroup>();
            combinedViewGroups.AddRange(this.viewGroups);

            var combinedOptionItems = combinedViewGroups.SelectMany(svg => svg.OptionItems);
            var newOptionItems = newViewGroups.SelectMany(cvg => cvg.OptionItems);

            foreach (var newOptionItem in newOptionItems)
            {
                var optionItem = combinedOptionItems.SingleOrDefault(oi => oi.SystemName == newOptionItem.SystemName);
                if (optionItem == null) //we have an item in the newViewGroups that is not part of the profile yet
                {
                    //find the the group where the item belongs to according to the newViewGroups
                    var newViewGroup = newViewGroups.Where(nvg => nvg.OptionItems.Any(oi => oi.SystemName == newOptionItem.SystemName)).SingleOrDefault();

                    //now find this group in the combinedViewGroups
                    var relevantViewGroup = combinedViewGroups.Where(cvg => cvg.Name == newViewGroup.Name).SingleOrDefault();
                    if (relevantViewGroup == null) //no such group found
                    {
                        //check if there is a 'misc' group
                        var miscViewGroup = combinedViewGroups.SingleOrDefault(vg => vg.Name == "Misc");
                        if (miscViewGroup == null) //not even that, well ...
                        {
                            miscViewGroup = new ViewGroup("Misc", int.MaxValue); //... then create it
                            combinedViewGroups.Add(miscViewGroup);
                        }

                        relevantViewGroup = miscViewGroup;
                    }

                    relevantViewGroup.Add(newOptionItem); //and put the item into that group
                }
            }

            return new Profile(combinedViewGroups);
        }

        public void ApplyOldValues(Dictionary<string, float> oldValues)
        {
            foreach (var viewGroup in this.ViewGroups)
            {
                foreach (var optionItem in viewGroup.OptionItems)
                {
                    if (oldValues.TryGetValue(optionItem.SystemName, out float radius))
                    {
                        optionItem.SetRadius(radius);
                    }
                }
            }
        }
    }
}
