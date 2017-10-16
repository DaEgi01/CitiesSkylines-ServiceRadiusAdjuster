using System;

namespace ServiceRadiusAdjuster.View
{
    public interface IGlobalOptionsView
    {
        bool ApplyAllButtonEnabled { get; set; }
        bool UndoAllButtonEnabled { get; set; }
        bool DefaultAllButtonEnabled { get; set; }
        bool DefaultAllx2ButtonEnabled { get; set; }
        bool DefaultAllx4ButtonEnabled { get; set; }

        event EventHandler ApplyAllButtonClicked;
        event EventHandler UndoAllButtonClicked;
        event EventHandler DefaultAllButtonClicked;
        event EventHandler DefaultAllx2ButtonClicked;
        event EventHandler DefaultAllx4ButtonClicked;
    }
}
