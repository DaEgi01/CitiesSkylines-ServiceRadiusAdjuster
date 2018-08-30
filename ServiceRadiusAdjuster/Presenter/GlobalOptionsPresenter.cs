using ServiceRadiusAdjuster.Configuration;
using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Service;
using ServiceRadiusAdjuster.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceRadiusAdjuster.Presenter
{
    public class GlobalOptionsPresenter
    {
        private readonly IGlobalOptionsView optionsView;
        private readonly List<OptionItemPresenter> optionItemPresenters;
        private readonly IConfigurationService configurationService;
        private readonly IGameEngineService gameEngineService;
        private readonly Profile profile;

        public GlobalOptionsPresenter(IGlobalOptionsView view, Profile profile, List<OptionItemPresenter> optionItemPresenters, IConfigurationService configurationService, IGameEngineService gameEngineService)
        {
            this.optionsView = view ?? throw new ArgumentNullException(nameof(view));
            this.profile = profile ?? throw new ArgumentNullException(nameof(profile));
            this.optionItemPresenters = optionItemPresenters ?? throw new ArgumentNullException(nameof(optionItemPresenters));
            this.configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            this.gameEngineService = gameEngineService ?? throw new ArgumentNullException(nameof(gameEngineService));

            this.optionsView.ApplyAllButtonClicked += (sender, e) => ApplyAll();
            this.optionsView.UndoAllButtonClicked += (sender, e) => UndoAll();
            this.optionsView.DefaultAllButtonClicked += (sender, e) => DefaultAll();
            this.optionsView.DefaultAllx2ButtonClicked += (sender, e) => DefaultAllx2();
            this.optionsView.DefaultAllx4ButtonClicked += (sender, e) => DefaultAllx4();

            foreach (var optionItemPresenter in optionItemPresenters)
            {
                optionItemPresenter.RequestPersistence += (sender, e) =>
                {
                    configurationService.SaveProfile(this.profile);
                };
            }
        }

        private void UpdateViewState()
        {
            optionsView.ApplyAllButtonEnabled = optionItemPresenters.Any(oip => oip.View.ApplyButtonEnabled);
            optionsView.UndoAllButtonEnabled = optionItemPresenters.Any(oip => oip.View.UndoButtonEnabled);
            optionsView.DefaultAllButtonEnabled = optionItemPresenters.Any(oip => oip.View.DefaultButtonEnabled);
        }

        public void ApplyAll()
        {
            optionItemPresenters.ForEach(p => p.Apply());

            //TODO handle errors
            var saveResult = configurationService.SaveProfile(profile);
            var applyResult = gameEngineService.ApplyToGame(profile);
            UpdateViewState();
        }

        public void UndoAll()
        {
            optionItemPresenters.ForEach(p => p.Undo());

            //TODO handle errors
            configurationService.SaveProfile(profile);
            UpdateViewState();
        }

        public void DefaultAll()
        {
            optionItemPresenters.ForEach(p => p.DefaultAndApply());

            //TODO handle errors
            configurationService.SaveProfile(profile);
            UpdateViewState();
        }

        public void DefaultAllx2()
        {
            optionItemPresenters.ForEach(p => p.View.Radius = (p.Model.RadiusDefault * 2).ToString());
            optionItemPresenters.ForEach(p => p.Apply());

            //TODO handle errors
            configurationService.SaveProfile(profile);
            UpdateViewState();
        }

        public void DefaultAllx4()
        {
            optionItemPresenters.ForEach(p => p.View.Radius = (p.Model.RadiusDefault * 4).ToString());
            optionItemPresenters.ForEach(p => p.Apply());

            //TODO handle errors
            configurationService.SaveProfile(profile);
            UpdateViewState();
        }
    }
}
