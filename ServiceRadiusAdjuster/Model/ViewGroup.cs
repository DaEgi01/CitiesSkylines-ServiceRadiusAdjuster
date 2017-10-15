using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ServiceRadiusAdjuster.Model
{
    public class ViewGroup
    {
        private readonly string name;
        private readonly int order;
        private readonly List<OptionItem> optionItems;

        public ViewGroup(string name, int order) : this(name, order, new List<OptionItem>())
        {
        }

        public ViewGroup(string name, int order, List<OptionItem> optionItems)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (optionItems == null) throw new ArgumentException(nameof(optionItems));

            this.name = name;
            this.order = order;
            this.optionItems = optionItems;
        }

        public string Name => name;
        public int Order => order;
        public ReadOnlyCollection<OptionItem> OptionItems => optionItems.AsReadOnly();

        public void Add(OptionItem optionItem)
        {
            this.optionItems.Add(optionItem);
        }

        public void AddRange(IEnumerable<OptionItem> optionItems)
        {
            this.optionItems.AddRange(optionItems);
        }
    }
}