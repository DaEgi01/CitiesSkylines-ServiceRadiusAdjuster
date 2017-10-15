using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Service;
using ServiceRadiusAdjuster.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceRadiusAdjuster.Presenter
{
    public interface IOptionItemPresenter
    {
        IOptionItemView View { get; }
        OptionItem Model { get; }

        void Apply();
        void Undo();
        void DefaultAndApply();

        event EventHandler RequestPersistence;
    }
}
