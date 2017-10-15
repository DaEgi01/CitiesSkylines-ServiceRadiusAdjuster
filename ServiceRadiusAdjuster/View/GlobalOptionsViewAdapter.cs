using ColossalFramework.UI;
using System;

namespace ServiceRadiusAdjuster.View
{
    public class GlobalOptionsViewAdapter : IGlobalOptionsView
    {
        private readonly UIButton applyAllButton;
        private readonly UIButton undoAllButton;
        private readonly UIButton defaultAllButton;
        private readonly UIButton defaultAllx2Button;
        private readonly UIButton defaultAllx4Button;

        public GlobalOptionsViewAdapter(UIButton applyAllButton, UIButton undoAllButton, UIButton defaultAllButton, UIButton defaultAllx2Button, UIButton defaultAllx4Button)
        {
            this.applyAllButton = applyAllButton;
            this.undoAllButton = undoAllButton;
            this.defaultAllButton = defaultAllButton;
            this.defaultAllx2Button = defaultAllx2Button;
            this.defaultAllx4Button = defaultAllx4Button;

            applyAllButton.eventClicked += (component, eventParam) => this.ApplyAllButtonClicked?.Invoke(this, EventArgs.Empty);
            undoAllButton.eventClicked += (component, eventParam) => this.UndoAllButtonClicked?.Invoke(this, EventArgs.Empty);
            defaultAllButton.eventClicked += (component, eventParam) => this.DefaultAllButtonClicked?.Invoke(this, EventArgs.Empty);
            defaultAllx2Button.eventClicked += (component, eventParam) => this.DefaultAllx2ButtonClicked?.Invoke(this, EventArgs.Empty);
            defaultAllx4Button.eventClicked += (component, eventParam) => this.DefaultAllx4ButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ApplyAllButtonClicked;
        public event EventHandler UndoAllButtonClicked;
        public event EventHandler DefaultAllButtonClicked;
        public event EventHandler DefaultAllx2ButtonClicked;
        public event EventHandler DefaultAllx4ButtonClicked;

        public bool ApplyAllButtonEnabled
        {
            get { return applyAllButton.enabled; }
            set
            {
                if (value)
                {
                    applyAllButton.Enable();
                }
                else
                {
                    applyAllButton.Disable();
                }
            }
        }

        public bool UndoAllButtonEnabled
        {
            get { return undoAllButton.enabled; }
            set
            {
                if (value)
                {
                    undoAllButton.Enable();
                }
                else
                {
                    undoAllButton.Disable();
                }
            }
        }

        public bool DefaultAllButtonEnabled
        {
            get { return defaultAllButton.enabled; }
            set
            {
                if (value)
                {
                    defaultAllButton.Enable();
                }
                else
                {
                    defaultAllButton.Disable();
                }
            }
        }

        public bool DefaultAllx2ButtonEnabled
        {
            get { return defaultAllx2Button.enabled; }
            set
            {
                if (value)
                {
                    defaultAllx2Button.Enable();
                }
                else
                {
                    defaultAllx2Button.Disable();
                }
            }
        }

        public bool DefaultAllx4ButtonEnabled
        {
            get { return defaultAllx4Button.enabled; }
            set
            {
                if (value)
                {
                    defaultAllx4Button.Enable();
                }
                else
                {
                    defaultAllx4Button.Disable();
                }
            }
        }
    }
}
