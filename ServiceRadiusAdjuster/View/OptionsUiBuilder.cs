using ColossalFramework.UI;
using ICities;
using ServiceRadiusAdjuster.Configuration;
using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Presenter;
using ServiceRadiusAdjuster.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ServiceRadiusAdjuster.View
{
    public class OptionsUiBuilder
    {
        private readonly float groupPaddingBottom = 0f;
        private readonly float itemHeight = 70f;
        private readonly float smallSpaceBetween = 8f;
        private readonly float largeSpaceBetween = 80f;
        private readonly float textElementWidth = 160f;
        private readonly float defaultTextElementWidth = 50f;
        private readonly float optionItemButtonWidth = 10f;

        private const string empty = " ";

        //TODO localization
        private readonly string accumulation = "Accumulation";
        private readonly string defaultAccumulation = "Default Accumulation";
        private readonly string noAccumulation = "This item has no accumulation value.";
        private readonly string radius = "Radius";
        private readonly string defaultRadius = "Default Radius";
        private readonly string apply = "Apply";
        private readonly string applyAll = "Apply all";
        private readonly string undo = "Undo";
        private readonly string undoAll = "Undo all";
        private readonly string _default = "Default";
        private readonly string defaultAll = "Default all";
        private readonly string startGameAtLeastOnce = "Configuration can only be changed in game.";

        private UIButton applyAllButton;
        private UIButton undoAllButton;
        private UIButton defaultAllButton;
        private UIButton defaultAllx2Button;
        private UIButton defaultAllx4Button;

        public GlobalOptionsPresenter GlobalOptionsPresenter { get; private set; }

        public void BuildProfileUi(UIHelperBase helper, string modName, string modVersion, Profile profile, IConfigurationService configurationService, IGameEngineService gameEngineService)
        {
            if (helper == null) throw new ArgumentNullException(nameof(helper));
            if (modName == null) throw new ArgumentNullException(nameof(modName));
            if (modVersion == null) throw new ArgumentNullException(nameof(modVersion));
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            if (gameEngineService == null) throw new ArgumentNullException(nameof(gameEngineService));

            var modLongTitle = GenerateLongTitle(modName, modVersion);
            var mainGroup = helper.AddGroup(modLongTitle) as UIHelper;
            var mainPanel = mainGroup.self as UIPanel;
            mainPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            mainPanel.autoLayoutPadding = new RectOffset(0, 5, 0, 5);

            this.AddGlobalButtons(mainGroup);

            var globalOptionsViewAdapter = new GlobalOptionsViewAdapter(this.applyAllButton, this.undoAllButton, this.defaultAllButton, this.defaultAllx2Button, this.defaultAllx4Button);

            var optionItemPresenters = new List<IOptionItemPresenter>();
            foreach (var viewGroup in profile.ViewGroups.OrderBy(vg => vg.Order))
            {
                var optionItemPresenterGroup = CreateOptionItemPresenterGroup(helper, viewGroup, gameEngineService);
                optionItemPresenters.AddRange(optionItemPresenterGroup);
            }

            this.GlobalOptionsPresenter = new GlobalOptionsPresenter(globalOptionsViewAdapter, profile, optionItemPresenters, configurationService, gameEngineService);
        }

        public void BuildNoProfileUi(UIHelperBase helper, string modName, string modVersion)
        {
            if (helper == null) throw new ArgumentNullException(nameof(helper));
            if (modName == null) throw new ArgumentNullException(nameof(modName));
            if (modVersion == null) throw new ArgumentNullException(nameof(modVersion));
            
            var modLongTitle = GenerateLongTitle(modName, modVersion);
            var mainGroup = helper.AddGroup(modLongTitle) as UIHelper;

            var mainPanel = mainGroup.self as UIPanel;
            mainPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            mainPanel.autoLayoutPadding = new RectOffset(0, 5, 0, 5);

            var startGameAtLeastOnceLabel = mainPanel.AddUIComponent<UILabel>();
            startGameAtLeastOnceLabel.text = startGameAtLeastOnce;
        }

        private void AddGlobalButtons(UIHelperBase helper)
        {
            this.applyAllButton = helper.AddButton(applyAll, new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            applyAllButton.color = new Color32(0, 255, 0, 255);
            applyAllButton.hoveredColor = new Color32(0, 255, 0, 255);
            applyAllButton.focusedColor = new Color32(0, 255, 0, 255);

            this.undoAllButton = helper.AddButton(undoAll, new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            undoAllButton.color = new Color32(255, 0, 0, 255);
            undoAllButton.hoveredColor = new Color32(255, 0, 0, 255);
            undoAllButton.focusedColor = new Color32(255, 0, 0, 255);

            this.defaultAllButton = helper.AddButton(defaultAll, new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            this.defaultAllx2Button = helper.AddButton(defaultAll + " x2", new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            this.defaultAllx4Button = helper.AddButton(defaultAll + " x4", new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
        }

        private List<IOptionItemPresenter> CreateOptionItemPresenterGroup(UIHelperBase helper, ViewGroup viewGroup, IGameEngineService gameEngineService = null)
        {
            var visibleGroupItems = viewGroup.OptionItems
                .Where(oi => !oi.Ignore)
                .OrderBy(oi => oi.DisplayName)
                .ToList();

            var group = helper.AddGroup(viewGroup.Name);
            var panel = (group as UIHelper).self as UIPanel;
            panel.autoLayout = false;
            panel.height = itemHeight * visibleGroupItems.Count + groupPaddingBottom;

            var result = new List<IOptionItemPresenter>();
            var offset = 0;
            foreach (var optionItem in visibleGroupItems)
            {
                var optionItemPresenter = CreateOptionItemPresenter(group, offset * itemHeight, optionItem, gameEngineService);
                result.Add(optionItemPresenter);
                offset++;
            }

            return result;
        }

        private IOptionItemPresenter CreateOptionItemPresenter(UIHelperBase groupHelper, float verticalStart, OptionItem optionItem, IGameEngineService gameEngineService)
        {
            var optionItemViewAdapter = CreateOptionItemViewAdapter(groupHelper, verticalStart, optionItem);
            return new OptionItemPresenter(optionItemViewAdapter, optionItem, gameEngineService);
        }

        private OptionItemViewAdapter CreateOptionItemViewAdapter(UIHelperBase groupHelper, float verticalStart, OptionItem optionItem)
        {
            var accumulationTextField = groupHelper.AddTextfield(optionItem.DisplayName, optionItem.Accumulation.HasValue ? optionItem.Accumulation.ToString() : "-", new OnTextChanged((s) => { })) as UITextField; //do nothing
            var accumulationPanel = accumulationTextField.parent as UIPanel;
            accumulationPanel.Find<UILabel>("Label").tooltip = optionItem.SystemName;

            accumulationTextField.tooltip = optionItem.Accumulation.HasValue ? accumulation : noAccumulation; //TODO move logic to presenter
            accumulationTextField.width = textElementWidth;
            accumulationTextField.horizontalAlignment = UIHorizontalAlignment.Right;
            accumulationTextField.isEnabled = optionItem.Accumulation.HasValue;
            accumulationTextField.numericalOnly = true;
            accumulationTextField.allowNegative = false;
            accumulationTextField.allowFloats = false;
            var accumulationTextFieldPanel = accumulationTextField.parent as UIPanel;
            accumulationTextFieldPanel.relativePosition = new Vector3(14f, verticalStart);
            accumulationTextFieldPanel.width = textElementWidth;

            var defaultAccumulationTextField = groupHelper.AddTextfield(empty, optionItem.AccumulationDefault.ToString(), new OnTextChanged((s) => { })) as UITextField; //do nothing
            defaultAccumulationTextField.tooltip = optionItem.Accumulation.HasValue ? defaultAccumulation : string.Empty;
            defaultAccumulationTextField.horizontalAlignment = UIHorizontalAlignment.Right;
            defaultAccumulationTextField.width = defaultTextElementWidth;
            defaultAccumulationTextField.isEnabled = false;
            defaultAccumulationTextField.isInteractive = true;
            defaultAccumulationTextField.padding = new RectOffset(0, 0, 4, 4);
            var defaultAccumulationPanel = defaultAccumulationTextField.parent as UIPanel;
            defaultAccumulationPanel.relativePosition = RightTo(accumulationTextFieldPanel, smallSpaceBetween);
            defaultAccumulationPanel.width = defaultTextElementWidth;

            var radiusTextField = groupHelper.AddTextfield(empty, optionItem.Radius.HasValue ? optionItem.Radius.ToString() : "-", new OnTextChanged((s) => { })) as UITextField; //do nothing
            radiusTextField.tooltip = optionItem.Radius.HasValue ? radius : string.Empty;
            radiusTextField.width = textElementWidth;
            radiusTextField.horizontalAlignment = UIHorizontalAlignment.Right;
            radiusTextField.numericalOnly = true;
            radiusTextField.allowNegative = false;
            radiusTextField.allowFloats = false;
            var radiusTextFieldPanel = radiusTextField.parent as UIPanel;
            radiusTextFieldPanel.relativePosition = RightTo(defaultAccumulationPanel, largeSpaceBetween);
            radiusTextFieldPanel.width = textElementWidth;

            var defaultRadiusTextField = groupHelper.AddTextfield(empty, optionItem.RadiusDefault.ToString(), new OnTextChanged((s) => { })) as UITextField; //do nothing
            defaultRadiusTextField.tooltip = optionItem.Radius.HasValue ? defaultRadius : string.Empty;
            defaultRadiusTextField.width = defaultTextElementWidth;
            defaultRadiusTextField.horizontalAlignment = UIHorizontalAlignment.Right;
            defaultRadiusTextField.isEnabled = false;
            defaultRadiusTextField.isInteractive = true;
            defaultRadiusTextField.padding = new RectOffset(0, 0, 4, 4);
            var defaultRadiusTextFieldPanel = defaultRadiusTextField.parent as UIPanel;
            defaultRadiusTextFieldPanel.relativePosition = RightTo(radiusTextFieldPanel, smallSpaceBetween);
            defaultRadiusTextFieldPanel.width = defaultTextElementWidth;

            var itemButtonTextScale = 1.0f;
            var itemButtonPadding = new RectOffset(8, 8, 7, 6);

            var applyButton = groupHelper.AddButton("✓", new OnButtonClicked(() => { })) as UIButton; //do nothing
            applyButton.tooltip = apply;
            applyButton.width = optionItemButtonWidth;
            applyButton.textScale = itemButtonTextScale;
            applyButton.relativePosition = RightTo(defaultRadiusTextFieldPanel, largeSpaceBetween, 22f);
            applyButton.textPadding = itemButtonPadding;
            applyButton.textHorizontalAlignment = UIHorizontalAlignment.Center;

            var undoButton = groupHelper.AddButton("✗", new OnButtonClicked(() => { })) as UIButton; //do nothing
            undoButton.tooltip = undo;
            undoButton.width = optionItemButtonWidth;
            undoButton.textScale = itemButtonTextScale;
            undoButton.relativePosition = RightTo(applyButton, smallSpaceBetween);
            undoButton.textPadding = itemButtonPadding;
            undoButton.textHorizontalAlignment = UIHorizontalAlignment.Center;

            var defaultButton = groupHelper.AddButton("←", new OnButtonClicked(() => { })) as UIButton; //do nothing
            defaultButton.tooltip = _default;
            defaultButton.width = optionItemButtonWidth;
            defaultButton.textScale = itemButtonTextScale;
            defaultButton.relativePosition = RightTo(undoButton, smallSpaceBetween);
            defaultButton.textPadding = itemButtonPadding;
            defaultButton.textHorizontalAlignment = UIHorizontalAlignment.Center;

            return new OptionItemViewAdapter(accumulationTextField, defaultAccumulationTextField, radiusTextField, defaultRadiusTextField, applyButton, undoButton, defaultButton);
        }

        private Vector3 RightTo(UIComponent component, float spaceBetween, float moveDown = 0f)
        {
            return new Vector3(component.relativePosition.x + component.width + spaceBetween, component.relativePosition.y + moveDown);
        }

        private string GenerateLongTitle(string modName, string modVersion)
        {
            return modName + " v" + modVersion;
        }
    }
}
