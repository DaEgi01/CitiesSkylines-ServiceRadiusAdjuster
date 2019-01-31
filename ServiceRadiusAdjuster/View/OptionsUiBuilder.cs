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
        private readonly int borderWidth = 8;
        private readonly float groupPaddingBottom = 0f;
        private readonly float itemHeight = 70f;
        private readonly int smallSpaceBetween = 8;
        private readonly int largeSpaceBetween = 60;
        private readonly float textElementWidth = 160f;
        private readonly float defaultTextElementWidth = 50f;
        private readonly float optionItemButtonWidth = 10f;
        private readonly float globalButtonScaleFactor = 0.8f;

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
        private readonly string noOldProfileNoCurrentProfileOutOfGameMessage = "Please start a game first.\nA new profile will be created from the items in your game.";
        private readonly string oldProfileNoCurrentProfileOutOfGameMessage = "Please start a game first.\nThe profile format has changed. A new profile will be created from the items in your game and your existing values will be applied to the new profile.";

        public GlobalOptionsPresenter GlobalOptionsPresenter { get; private set; }

        public void BuildInGameUi(UIHelperBase helper, IConfigurationService configurationService, IGameEngineService gameEngineService, ModFullTitle modFullTitle, Profile profile)
        {
            if (helper == null) throw new ArgumentNullException(nameof(helper));
            if (modFullTitle == null) throw new ArgumentNullException(nameof(modFullTitle));
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            if (gameEngineService == null) throw new ArgumentNullException(nameof(gameEngineService));

            var rootScrollablePanelHelper = helper as UIHelper;
            var rootScrollablePanel = rootScrollablePanelHelper.self as UIScrollablePanel;
            var rootPanel = rootScrollablePanel.parent as UIPanel;
            var rootScrollbar = rootPanel.components.Where(x => x.GetType() == typeof(UIScrollbar)).Single();

            rootPanel.RemoveUIComponent(rootScrollablePanel);
            rootPanel.RemoveUIComponent(rootScrollbar);

            var headerRootPanel = rootPanel.AddUIComponent<UIPanel>();
            headerRootPanel.anchor = UIAnchorStyle.Left | UIAnchorStyle.Top | UIAnchorStyle.Right;
            headerRootPanel.height = 50f;

            var contentRootPanel = rootPanel.AddUIComponent<UIPanel>();
            contentRootPanel.anchor = UIAnchorStyle.All;
            contentRootPanel.relativePosition = new Vector3(0, headerRootPanel.height);

            contentRootPanel.AttachUIComponent(rootScrollablePanel.gameObject);
            contentRootPanel.AttachUIComponent(rootScrollbar.gameObject);

            var headerRootPanelHelper = new UIHelper(headerRootPanel);

            var headerContentPanelUIHelper = headerRootPanelHelper.AddGroup(modFullTitle) as UIHelper;
            var headerContentPanel = headerContentPanelUIHelper.self as UIPanel;
            headerContentPanel.padding = new RectOffset(0, 0, 0, 0);
            headerContentPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            headerContentPanel.autoLayoutPadding = new RectOffset(0, smallSpaceBetween, 0, smallSpaceBetween);

            var headerPanel = headerContentPanel.parent as UIPanel;
            headerPanel.autoLayout = false;
            headerPanel.name = "headerPanel";
            headerPanel.relativePosition = Vector3.zero;
            headerPanel.height = 117f;
            headerPanel.padding = new RectOffset(borderWidth, borderWidth, borderWidth, borderWidth);

            var applyAllButton = headerContentPanelUIHelper.AddButton(applyAll, new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            applyAllButton.scaleFactor = globalButtonScaleFactor;
            applyAllButton.color = new Color32(0, 255, 0, 255);
            applyAllButton.hoveredColor = new Color32(0, 255, 0, 255);
            applyAllButton.focusedColor = new Color32(0, 255, 0, 255);

            var undoAllButton = headerContentPanelUIHelper.AddButton(undoAll, new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            undoAllButton.scaleFactor = globalButtonScaleFactor;
            undoAllButton.color = new Color32(255, 0, 0, 255);
            undoAllButton.hoveredColor = new Color32(255, 0, 0, 255);
            undoAllButton.focusedColor = new Color32(255, 0, 0, 255);

            var defaultAllButton = headerContentPanelUIHelper.AddButton(defaultAll, new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            defaultAllButton.scaleFactor = globalButtonScaleFactor;

            var defaultAllx2Button = headerContentPanelUIHelper.AddButton(defaultAll + " x2", new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            defaultAllx2Button.scaleFactor = globalButtonScaleFactor;

            var defaultAllx4Button = headerContentPanelUIHelper.AddButton(defaultAll + " x4", new OnButtonClicked(() => { /* do nothing */ })) as UIButton;
            defaultAllx4Button.scaleFactor = globalButtonScaleFactor;

            var globalOptionsViewAdapter = new GlobalOptionsViewAdapter(applyAllButton, undoAllButton, defaultAllButton, defaultAllx2Button, defaultAllx4Button);

            var optionItemPresenters = new List<OptionItemPresenter>();
            foreach (var viewGroup in profile.ViewGroups.OrderBy(vg => vg.Order))
            {
                var optionItemPresenterGroup = CreateOptionItemPresenterGroup(helper, viewGroup, gameEngineService);
                optionItemPresenters.AddRange(optionItemPresenterGroup);
            }

            this.GlobalOptionsPresenter = new GlobalOptionsPresenter(globalOptionsViewAdapter, profile, optionItemPresenters, configurationService, gameEngineService);
        }

        public void BuildGenerateUi(UIHelperBase helper, IConfigurationService configurationService, IGameEngineService gameEngineService, ModFullTitle modFullTitle, Profile profile)
        {
            if (helper == null) throw new ArgumentNullException(nameof(helper));
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            if (gameEngineService == null) throw new ArgumentNullException(nameof(gameEngineService));
            if (modFullTitle == null) throw new ArgumentNullException(nameof(modFullTitle));
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            var mainGroup = helper.AddGroup(modFullTitle) as UIHelper;
            mainGroup.AddButton("Generate UI", () => 
                {
                    this.BuildInGameUi(helper, configurationService, gameEngineService, modFullTitle, profile);
                }
            );
        }

        public void BuildNoProfileUi(UIHelperBase helper, ModFullTitle modFullTitle, bool oldValuesFound)
        {
            if (helper == null) throw new ArgumentNullException(nameof(helper));
            if (modFullTitle == null) throw new ArgumentNullException(nameof(modFullTitle));
            
            var mainGroup = helper.AddGroup(modFullTitle) as UIHelper;

            var mainPanel = mainGroup.self as UIPanel;
            mainPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            mainPanel.autoLayoutPadding = new RectOffset(0, 5, 0, 5);

            var configurationChangeDuringGameplayLabel = mainPanel.AddUIComponent<UILabel>();
            configurationChangeDuringGameplayLabel.text = oldValuesFound ? oldProfileNoCurrentProfileOutOfGameMessage : noOldProfileNoCurrentProfileOutOfGameMessage;
        }

        public void ClearExistingUi(UIHelperBase helper)
        {
            var uiHelper = helper as UIHelper;
            var mainPanel = uiHelper.self as UIScrollablePanel;

            foreach (var component in mainPanel.components)
            {
                UnityEngine.Object.Destroy(component.gameObject);
            }
        }

        public void ShowExceptionPanel(string title, string message, bool isError)
        {
            var infoPanel = UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel");
            infoPanel.SetMessage(title, message, isError);
        }

        private List<OptionItemPresenter> CreateOptionItemPresenterGroup(UIHelperBase helper, ViewGroup viewGroup, IGameEngineService gameEngineService)
        {
            var visibleGroupItems = viewGroup.OptionItems
                .Where(oi => !oi.Ignore)
                .OrderBy(oi => oi.DisplayName)
                .ToList();

            var group = helper.AddGroup(viewGroup.Name);
            var panel = (group as UIHelper).self as UIPanel;
            panel.autoLayout = false;
            panel.height = itemHeight * visibleGroupItems.Count + groupPaddingBottom;

            var result = new List<OptionItemPresenter>();
            var offset = 0;
            foreach (var optionItem in visibleGroupItems)
            {
                var optionItemPresenter = CreateOptionItemPresenter(group, offset * itemHeight, optionItem, gameEngineService);
                result.Add(optionItemPresenter);
                offset++;
            }

            return result;
        }

        private OptionItemPresenter CreateOptionItemPresenter(UIHelperBase groupHelper, float verticalStart, OptionItem optionItem, IGameEngineService gameEngineService)
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
    }
}
