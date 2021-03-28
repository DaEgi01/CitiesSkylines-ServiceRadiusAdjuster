using ServiceRadiusAdjuster.FunctionalCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ServiceRadiusAdjuster.Model
{
    public class Profile
    {
        private readonly List<ViewGroup> _viewGroups;

        public Profile()
            : this(new List<ViewGroup>())
        {
        }

        public Profile(List<ViewGroup> viewGroups)
        {
            _viewGroups = viewGroups ?? throw new ArgumentNullException(nameof(viewGroups));
            ViewGroups = new ReadOnlyCollection<ViewGroup>(_viewGroups);
        }

        public ReadOnlyCollection<ViewGroup> ViewGroups { get; }

        /// <summary>
        /// Adds new OptionItems into the appropriate group.
        /// </summary>
        public Profile Combine(IEnumerable<ViewGroup> newViewGroups)
        {
            var combinedViewGroups = new List<ViewGroup>();
            combinedViewGroups.AddRange(_viewGroups);

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
                    if (relevantViewGroup == null)
                    {
                        //add it if it does not exist yet
                        relevantViewGroup = new ViewGroup(newViewGroup.Name, newViewGroup.Order);
                        combinedViewGroups.Add(relevantViewGroup);
                    }

                    relevantViewGroup.Add(newOptionItem); //and put the item into that group
                }
            }

            return new Profile(combinedViewGroups);
        }

        public Result<string, Profile> BatchEdit(float? accumulationMultiplier, float? radiusMultiplier)
        {
            if (!accumulationMultiplier.HasValue && !radiusMultiplier.HasValue)
            {
                Result<string>.Error("Please enter a accumulation or radius multiplier.");
            }

            foreach (var viewGroup in ViewGroups)
            {
                foreach (var optionItem in viewGroup.OptionItems)
                {
                    if (accumulationMultiplier.HasValue && optionItem.AccumulationDefault.HasValue)
                    {
                        optionItem.SetAccumulation((int)(accumulationMultiplier.Value * optionItem.AccumulationDefault.Value));
                    }

                    if (radiusMultiplier.HasValue && optionItem.RadiusDefault.HasValue)
                    {
                        optionItem.SetRadius(radiusMultiplier.Value * optionItem.RadiusDefault.Value);
                    }
                }
            }

            return Result<string, Profile>.Ok(this);
        }
    }
}
