using System;
using ColossalFramework.UI;
using UnityEngine;

namespace ServiceRadiusAdjuster.View
{
    public class OptionItemViewAdapter : IOptionItemView
    {
        private readonly UITextField accumulationTextField;
        private readonly UITextField accumulationDefaultTextField;

        private readonly UITextField radiusTextField;
        private readonly UITextField radiusDefaultTextField;

        private readonly Color32 textFieldColorDefault;
        private readonly Color32 textFieldColorError;

        private readonly UIButton applyButton;
        private readonly UIButton undoButton;
        private readonly UIButton defaultButton;

        private readonly string accumulationTooltipDefault;
        private readonly string radiusTooltipDefault;

        private string accumulationErrorMessage = string.Empty;
        private string radiusErrorMessage = string.Empty;

        public OptionItemViewAdapter(
            UITextField accumulationTextField,
            UITextField accumulationDefaultTextField,
            UITextField radiusTextField,
            UITextField radiusDefaultTextField,
            UIButton applyButton,
            UIButton undoButton,
            UIButton defaultButton)
        {
            if (accumulationTextField == null) throw new ArgumentNullException(nameof(accumulationTextField));
            if (accumulationDefaultTextField == null) throw new ArgumentNullException(nameof(accumulationDefaultTextField));
            if (radiusTextField == null) throw new ArgumentNullException(nameof(radiusTextField));
            if (radiusDefaultTextField == null) throw new ArgumentNullException(nameof(radiusDefaultTextField));
            if (applyButton == null) throw new ArgumentNullException(nameof(applyButton));
            if (undoButton == null) throw new ArgumentNullException(nameof(undoButton));
            if (defaultButton == null) throw new ArgumentNullException(nameof(defaultButton));

            this.accumulationTextField = accumulationTextField;
            this.accumulationDefaultTextField = accumulationDefaultTextField;

            this.radiusTextField = radiusTextField;
            this.radiusDefaultTextField = radiusDefaultTextField;

            this.textFieldColorDefault = accumulationTextField.color;
            this.textFieldColorError = new Color32(255, 0, 0, 255);

            this.applyButton = applyButton;
            this.undoButton = undoButton;
            this.defaultButton = defaultButton;

            this.accumulationTooltipDefault = accumulationTextField.tooltip;
            this.radiusTooltipDefault = radiusTextField.tooltip;

            this.accumulationTextField.eventTextChanged += (component, value) => this.AccumulationChanged?.Invoke(this, EventArgs.Empty);
            this.radiusTextField.eventTextChanged += (component, value) => this.RadiusChanged?.Invoke(this, EventArgs.Empty);

            this.applyButton.eventClicked += (component, eventParam) => this.ApplyButtonClicked?.Invoke(this, EventArgs.Empty);
            this.undoButton.eventClicked += (component, eventParam) => this.UndoButtonClicked?.Invoke(this, EventArgs.Empty);
            this.defaultButton.eventClicked += (component, eventParam) => this.DefaultButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public string Accumulation
        {
            get => this.accumulationTextField.text;
            set => this.accumulationTextField.text = value;
        }

        public string AccumulationDefault
        {
            get => this.accumulationDefaultTextField.text;
            set => this.accumulationDefaultTextField.text = value;
        }

        public string Radius
        {
            get => this.radiusTextField.text;
            set => this.radiusTextField.text = value;
        }

        public string RadiusDefault
        {
            get => this.radiusDefaultTextField.text;
            set => this.radiusDefaultTextField.text = value;
        }

        public bool ApplyButtonEnabled
        {
            get => this.applyButton.enabled;
            set
            {
                if (value)
                {
                    this.applyButton.Enable();
                }
                else
                {
                    this.applyButton.Disable();
                }
            }
        }

        public bool UndoButtonEnabled
        {
            get => this.undoButton.enabled;
            set
            {
                if (value)
                {
                    this.undoButton.Enable();
                }
                else
                {
                    this.undoButton.Disable();
                }
            }
        }

        public bool DefaultButtonEnabled
        {
            get => this.defaultButton.enabled;
            set
            {
                if (value)
                {
                    this.defaultButton.Enable();
                }
                else
                {
                    this.defaultButton.Disable();
                }
            }
        }

        public string AccumulationErrorMessage
        {
            get => this.accumulationErrorMessage;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.accumulationTextField.color = textFieldColorDefault;
                    this.accumulationTextField.tooltip = accumulationTooltipDefault;
                }
                else
                {
                    this.accumulationTextField.color = textFieldColorError;
                    this.accumulationTextField.tooltip = value;
                }

                this.accumulationErrorMessage = value;
            }
        }

        public string RadiusErrorMessage
        {
            get => this.radiusErrorMessage;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.radiusTextField.color = textFieldColorDefault;
                    this.radiusTextField.tooltip = radiusTooltipDefault;
                }
                else
                {
                    this.radiusTextField.color = textFieldColorError;
                    this.radiusTextField.tooltip = value;
                }

                this.radiusErrorMessage = value;
            }
        }

        public event EventHandler AccumulationChanged;
        public event EventHandler RadiusChanged;

        public event EventHandler ApplyButtonClicked;
        public event EventHandler UndoButtonClicked;
        public event EventHandler DefaultButtonClicked;
    }
}
