using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Service;
using ServiceRadiusAdjuster.View;
using System;

namespace ServiceRadiusAdjuster.Presenter
{
    public class OptionItemPresenter
    {
        private readonly IOptionItemView view;
        private readonly OptionItem model;
        private readonly IGameEngineService gameEngineService;

        public OptionItemPresenter(IOptionItemView view, OptionItem model, IGameEngineService gameEngineService)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.gameEngineService = gameEngineService ?? throw new ArgumentNullException(nameof(gameEngineService));

            this.view.Accumulation = model.Accumulation.ToString();
            this.view.AccumulationDefault = model.AccumulationDefault.ToString();
            this.view.Radius = model.Radius.ToString();
            this.view.RadiusDefault = model.RadiusDefault.ToString();

            this.UpdateViewButtonsFromModel();

            this.view.AccumulationChanged += (sender, e) => HandleChangeState();
            this.view.RadiusChanged += (sender, e) => HandleChangeState();
            this.view.ApplyButtonClicked += (sender, e) =>
            {
                Apply();
                this.RequestPersistence?.Invoke(this, EventArgs.Empty);
            };
            this.view.UndoButtonClicked += (sender, e) => Undo();
            this.view.DefaultButtonClicked += (sender, e) =>
            {
                DefaultAndApply();
                this.RequestPersistence?.Invoke(this, EventArgs.Empty);
            };

            this.view.AccumulationErrorMessage = string.Empty;
            this.view.RadiusErrorMessage = string.Empty;
        }

        public bool IsDirty => 
            model.Accumulation.ToString() != view.Accumulation
            || model.Radius.ToString() != view.Radius;

        public bool IsDefault => 
            model.Accumulation == model.AccumulationDefault 
            && model.Radius == model.RadiusDefault;

        public IOptionItemView View => view;
        public OptionItem Model => model;
        public event EventHandler RequestPersistence;

        private void HandleChangeState()
        {
            this.UpdateViewButtonsFromModel();
        }

        private void UpdateViewFromModel()
        {
            if (view.Accumulation != model.Accumulation.ToString())
            {
                view.Accumulation = model.Accumulation.ToString();
            }

            if (view.Radius != model.Radius.ToString())
            {
                view.Radius = model.Radius.ToString();
            }

            this.UpdateViewButtonsFromModel();
        }

        private void UpdateViewButtonsFromModel()
        {
            view.ApplyButtonEnabled = IsDirty;
            view.UndoButtonEnabled = IsDirty;
            view.DefaultButtonEnabled = !IsDefault;
        }

        public void Apply()
        {
            view.AccumulationErrorMessage = string.Empty;
            view.RadiusErrorMessage = string.Empty;

            if (model.Accumulation.HasValue)
            {
                var setAccumulationResult = model.SetAccumulation(view.Accumulation);
                if (setAccumulationResult.IsFailure)
                {
                    view.AccumulationErrorMessage = setAccumulationResult.Error;
                }
            }

            if (model.Radius.HasValue)
            {
                var setRadiusResult = model.SetRadius(view.Radius);
                if (setRadiusResult.IsFailure)
                {
                    view.RadiusErrorMessage = setRadiusResult.Error;
                }
            }

            var result = gameEngineService.ApplyToGame(model);
            if (result.IsFailure)
            {
                view.AccumulationErrorMessage += " " + result.Error;
                view.RadiusErrorMessage += " " + result.Error;
            }

            this.UpdateViewFromModel();
        }

        public void Undo()
        {
            this.UpdateViewFromModel();
        }

        public void DefaultAndApply()
        {
            if (model.AccumulationDefault.HasValue)
            {
                view.Accumulation = model.AccumulationDefault.Value.ToString();
            }

            if (model.RadiusDefault.HasValue)
            {
                view.Radius = model.RadiusDefault.Value.ToString();
            }

            this.Apply();
        }
    }
}
