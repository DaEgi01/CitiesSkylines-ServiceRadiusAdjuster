using System;
using System.Collections.Generic;

namespace ServiceRadiusAdjuster.Model;

public class ViewGroup
{
    private readonly string _name;
    private readonly int _order;
    private readonly List<OptionItem> _optionItems;

    public ViewGroup(string name, int order) 
        : this(name, order, new List<OptionItem>())
    {
    }

    public ViewGroup(string name, int order, List<OptionItem> optionItems)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _order = order;
        _optionItems = optionItems ?? throw new ArgumentException(nameof(optionItems));
    }

    public string Name => _name;
    public int Order => _order;
    public IEnumerable<OptionItem> OptionItems => _optionItems;

    public void Add(OptionItem optionItem)
    {
        _optionItems.Add(optionItem);
    }

    public void AddRange(IEnumerable<OptionItem> optionItems)
    {
        _optionItems.AddRange(optionItems);
    }
}