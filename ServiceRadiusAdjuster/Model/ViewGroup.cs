using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ServiceRadiusAdjuster.Model
{
    public class ViewGroup
    {
        private readonly string name;
        private readonly int order;
        private readonly List<OptionItem> optionItems;

        public ViewGroup(string name, int order) 
            : this(name, order, new List<OptionItem>())
        {
        }

        public ViewGroup(string name, int order, List<OptionItem> optionItems)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.order = order;
            this.optionItems = optionItems ?? throw new ArgumentException(nameof(optionItems));
            this.OptionItems = new ReadOnlyCollection<OptionItem>(optionItems);
        }

        public string Name => name;
        public int Order => order;
        public ReadOnlyCollection<OptionItem> OptionItems { get; }

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