using UnityEngine;
using ColossalFramework.UI;
using ServiceRadiusAdjuster.Model;

namespace ServiceRadiusAdjuster.GUI
{
    public class UIOptionPanel : UIPanel
    {
        private UITextField m_accumulation;
        private UITextField m_radius;

        private UIButton m_restore;

        public OptionItem m_optionItem = null;

        public override void Start()
        {
            base.Start();
            canFocus = true;
            isInteractive = true;
            width = 315;
            height = 330;

            SetupControls();

            //TODO
            //m_options = new VehicleOptions();
        }

        public void Show(OptionItem optionItem)
        {
            m_optionItem = optionItem;

            if (optionItem.Accumulation.HasValue)
            {
                m_accumulation.enabled = true;
                m_accumulation.text = optionItem.Accumulation.Value.ToString();
            }
            else
            {
                m_accumulation.enabled = false;
            }

            if (optionItem.Radius.HasValue)
            {
                m_radius.enabled = true;
                m_radius.text = optionItem.Radius.Value.ToString();
            }
        }

        private void SetupControls()
        {
            var panel = this.AddUIComponent<UIPanel>();
            panel.gameObject.AddComponent<UICustomControl>();

            panel.backgroundSprite = "UnlockingPanel";
            panel.width = width - 10;
            panel.height = height - 75;
            panel.relativePosition = new Vector3(5, 0);

            // Max Speed
            UILabel accumulationLabel = panel.AddUIComponent<UILabel>();
            accumulationLabel.text = "Accumulation:";
            accumulationLabel.textScale = 0.8f;
            accumulationLabel.relativePosition = new Vector3(15, 15);

            m_accumulation = UIUtils.CreateTextField(panel);
            m_accumulation.numericalOnly = true;
            m_accumulation.width = 75;
            m_accumulation.tooltip = "Change the accumulation value.";
            m_accumulation.relativePosition = new Vector3(15, 35);

            UILabel radiusLabel = panel.AddUIComponent<UILabel>();
            radiusLabel.text = "Radius";
            radiusLabel.textScale = 0.8f;
            radiusLabel.relativePosition = new Vector3(95, 40);

            m_radius = UIUtils.CreateTextField(panel);
            m_radius.numericalOnly = true;
            m_radius.allowFloats = true;
            m_radius.width = 60;
            m_radius.tooltip = "Change the radius value.";
            m_radius.relativePosition = new Vector3(160, 35);

            // Restore default
            m_restore = UIUtils.CreateButton(panel);
            m_restore.text = "Restore default";
            m_restore.width = 130;
            m_restore.tooltip = "Restore all values to default";
            m_restore.relativePosition = new Vector3(160, 215);

            panel.BringToFront();

            // Event handlers
            m_accumulation.eventTextSubmitted += OnAccumulationSubmitted;
            m_radius.eventTextSubmitted += OnRadiusSubmitted;

            m_restore.eventClick += (c, p) =>
            {
                //TODO

                Show(m_optionItem);
            };
        }

        protected void OnAccumulationSubmitted(UIComponent component, string text)
        {
            m_optionItem.SetAccumulation(text);
        }

        protected void OnRadiusSubmitted(UIComponent component, string text)
        {
            m_optionItem.SetRadius(text);
        }
    }

}
