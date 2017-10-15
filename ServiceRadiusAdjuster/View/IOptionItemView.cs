using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceRadiusAdjuster.View
{
    public interface IOptionItemView
    {
        string Accumulation { get; set; }
        string AccumulationDefault { get; set; }

        string Radius { get; set; }
        string RadiusDefault { get; set; }
        
        bool ApplyButtonEnabled { get; set; }
        bool UndoButtonEnabled { get; set; }
        bool DefaultButtonEnabled { get; set; }

        string AccumulationErrorMessage { get; set; }
        string RadiusErrorMessage { get; set; }

        event EventHandler AccumulationChanged;
        event EventHandler RadiusChanged;

        event EventHandler ApplyButtonClicked;
        event EventHandler UndoButtonClicked;
        event EventHandler DefaultButtonClicked;
    }
}
