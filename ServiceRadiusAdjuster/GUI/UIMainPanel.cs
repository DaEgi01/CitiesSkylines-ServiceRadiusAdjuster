using UnityEngine;
using ColossalFramework.Globalization;
using ColossalFramework.UI;

using System;
using System.Reflection;

using ServiceRadiusAdjuster.Model;

namespace ServiceRadiusAdjuster.GUI
{
    public class UIMainPanel : UIPanel
    {
        private UITitleBar m_title;
        private UIDropDown m_category;
        private UITextField m_search;
        private UIFastList m_fastList;
        private UIButton m_import;
        private UIButton m_export;
        private UITextureSprite m_preview;
        private UISprite m_followVehicle;
        private UIOptionPanel m_optionPanel;

        public UIButton m_button;

        private OptionItem[] m_optionsList;
        private PreviewRenderer m_previewRenderer;
        private CameraController m_cameraController;
        private uint m_seekStart = 0;

        private const int HEIGHT = 550;
        private const int WIDTHLEFT = 470;
        private const int WIDTHRIGHT = 315;

        public UIOptionPanel optionPanel
        {
            get { return m_optionPanel; }
        }

        public OptionItem[] optionList
        {
            get { return m_optionsList; }
            set
            {
                m_optionsList = value;
                Array.Sort(m_optionsList);

                PopulateList();
            }
        }

        public override void Start()
        {
            UIView view = GetUIView();

            name = "AdvancedVehicleOptions";
            backgroundSprite = "UnlockingPanel2";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            width = WIDTHLEFT + WIDTHRIGHT;
            height = HEIGHT;
            relativePosition = new Vector3(Mathf.Floor((view.fixedWidth - width) / 2), Mathf.Floor((view.fixedHeight - height) / 2));

            // Get camera controller
            GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
            if (go != null)
            {
                m_cameraController = go.GetComponent<CameraController>();
            }

            // Setting up UI
            SetupControls();

            // Adding main button
            UITabstrip toolStrip = view.FindUIComponent<UITabstrip>("MainToolstrip");
            m_button = AddUIComponent<UIButton>();

            m_button.normalBgSprite = "IconCitizenVehicle";
            m_button.focusedFgSprite = "ToolbarIconGroup6Focused";
            m_button.hoveredFgSprite = "ToolbarIconGroup6Hovered";

            m_button.size = new Vector2(43f, 49f);
            m_button.name = "Advanced Vehicle Options";
            m_button.relativePosition = new Vector3(0, 5);

            m_button.eventButtonStateChanged += (c, s) =>
            {
                if (s == UIButton.ButtonState.Focused)
                {
                    if (!isVisible)
                    {
                        isVisible = true;
                        m_fastList.DisplayAt(m_fastList.listPosition);
                        m_optionPanel.Show(m_fastList.rowsData[m_fastList.selectedIndex] as OptionItem);
                        m_followVehicle.isVisible = m_preview.parent.isVisible = true;
                    }
                }
                else
                {
                    isVisible = false;
                    m_button.Unfocus();
                }
            };

            toolStrip.AddTab("Advanced Vehicle Options", m_button.gameObject, null, null);

            FieldInfo m_ObjectIndex = typeof(MainToolbar).GetField("m_ObjectIndex", BindingFlags.Instance | BindingFlags.NonPublic);
            m_ObjectIndex.SetValue(ToolsModifierControl.mainToolbar, (int)m_ObjectIndex.GetValue(ToolsModifierControl.mainToolbar) + 1);

            m_title.closeButton.eventClick += (component, param) =>
            {
                toolStrip.closeButton.SimulateClick();
            };

            Locale locale = (Locale)typeof(LocaleManager).GetField("m_Locale", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(LocaleManager.instance);
            Locale.Key key = new Locale.Key
            {
                m_Identifier = "TUTORIAL_ADVISER_TITLE",
                m_Key = m_button.name
            };
            if (!locale.Exists(key))
            {
                locale.AddLocalizedString(key, m_button.name);
            }
            key = new Locale.Key
            {
                m_Identifier = "TUTORIAL_ADVISER",
                m_Key = m_button.name
            };
            if (!locale.Exists(key))
            {
                locale.AddLocalizedString(key, "");
            }

            view.FindUIComponent<UITabContainer>("TSContainer").AddUIComponent<UIPanel>().color = new Color32(0, 0, 0, 0);

            //TODO set options   
        }

        private void SetupControls()
        {
            float offset = 40f;

            // Title Bar
            m_title = AddUIComponent<UITitleBar>();
            m_title.iconSprite = "IconCitizenVehicle";
            m_title.title = "Advanced Vehicle Options";

            // Category DropDown
            UILabel label = AddUIComponent<UILabel>();
            label.textScale = 0.8f;
            label.padding = new RectOffset(0, 0, 8, 0);
            label.relativePosition = new Vector3(10f, offset);
            label.text = "Category :";

            m_category = UIUtils.CreateDropDown(this);
            m_category.width = 150;

            var allCategories = Category.GetAll();

            for (int i = 0; i < allCategories.Count; i++)
                m_category.AddItem(allCategories[i].DisplayName);

            m_category.selectedIndex = 0;
            m_category.tooltip = "Select a category to display\nTip: Use the mouse wheel to switch between categories faster";
            m_category.relativePosition = label.relativePosition + new Vector3(70f, 0f);

            m_category.eventSelectedIndexChanged += (c, t) =>
            {
                m_category.enabled = false;
                PopulateList();
                m_category.enabled = true;
            };

            // Search
            m_search = UIUtils.CreateTextField(this);
            m_search.width = 150f;
            m_search.height = 30f;
            m_search.padding = new RectOffset(6, 6, 6, 6);
            m_search.tooltip = "Type the name of a vehicle type";
            m_search.relativePosition = new Vector3(WIDTHLEFT - m_search.width, offset);

            m_search.eventTextChanged += (c, t) => PopulateList();

            label = AddUIComponent<UILabel>();
            label.textScale = 0.8f;
            label.padding = new RectOffset(0, 0, 8, 0);
            label.relativePosition = m_search.relativePosition - new Vector3(60f, 0f);
            label.text = "Search :";

            // FastList
            m_fastList = UIFastList.Create<UIServiceBuildingItem>(this);
            m_fastList.backgroundSprite = "UnlockingPanel";
            m_fastList.width = WIDTHLEFT - 5;
            m_fastList.height = height - offset - 110;
            m_fastList.canSelect = true;
            m_fastList.relativePosition = new Vector3(5, offset + 35);

            // Configuration file buttons
            UILabel configLabel = this.AddUIComponent<UILabel>();
            configLabel.text = "Configuration file:";
            configLabel.textScale = 0.8f;
            configLabel.relativePosition = new Vector3(10, height - 60);

            m_import = UIUtils.CreateButton(this);
            m_import.text = "Import";
            m_import.tooltip = "Import the configuration";
            m_import.relativePosition = new Vector3(10, height - 40);

            m_export = UIUtils.CreateButton(this);
            m_export.text = "Export";
            m_export.tooltip = "Export the configuration";
            m_export.relativePosition = new Vector3(105, height - 40);

            // Preview
            UIPanel panel = AddUIComponent<UIPanel>();
            panel.backgroundSprite = "GenericPanel";
            panel.width = WIDTHRIGHT - 10;
            panel.height = HEIGHT - 375;
            panel.relativePosition = new Vector3(WIDTHLEFT + 5, offset);

            m_preview = panel.AddUIComponent<UITextureSprite>();
            m_preview.size = panel.size;
            m_preview.relativePosition = Vector3.zero;

            m_previewRenderer = gameObject.AddComponent<PreviewRenderer>();
            m_previewRenderer.size = m_preview.size * 2; // Twice the size for anti-aliasing

            m_preview.texture = m_previewRenderer.texture;

            // Follow
            if (m_cameraController != null)
            {
                m_followVehicle = AddUIComponent<UISprite>();
                m_followVehicle.spriteName = "LocationMarkerFocused";
                m_followVehicle.width = m_followVehicle.spriteInfo.width;
                m_followVehicle.height = m_followVehicle.spriteInfo.height;
                m_followVehicle.tooltip = "Click here to cycle through the existing vehicles of that type";
                m_followVehicle.relativePosition = new Vector3(panel.relativePosition.x + panel.width - m_followVehicle.width - 5, panel.relativePosition.y + 5);

                m_followVehicle.eventClick += (c, p) => FollowNextVehicle();
            }

            // Option panel
            m_optionPanel = AddUIComponent<UIOptionPanel>();
            m_optionPanel.relativePosition = new Vector3(WIDTHLEFT, height - 330);

            // Event handlers
            m_fastList.eventSelectedIndexChanged += OnSelectedItemChanged;
            m_import.eventClick += (c, t) =>
            {
                //TODO

                //DefaultOptions.RestoreAll();
                //AdvancedVehicleOptions.ImportConfig();
                //optionList = AdvancedVehicleOptions.config.options;
            };
            m_export.eventClick += (c, t) =>
            {
                //TODO

                //AdvancedVehicleOptions.ExportConfig();
            };

            panel.eventMouseDown += (c, p) =>
            {
                eventMouseMove += RotateCamera;
                m_previewRenderer.RenderBuilding(m_optionPanel.m_optionItem);
            };

            panel.eventMouseUp += (c, p) =>
            {
                eventMouseMove -= RotateCamera;
                m_previewRenderer.RenderBuilding(m_optionPanel.m_optionItem);
            };

            panel.eventMouseWheel += (c, p) =>
            {
                m_previewRenderer.zoom -= Mathf.Sign(p.wheelDelta) * 0.25f;
                m_previewRenderer.RenderBuilding(m_optionPanel.m_optionItem);
            };
        }

        private void RotateCamera(UIComponent c, UIMouseEventParameter p)
        {
            m_previewRenderer.cameraRotation -= p.moveDelta.x / m_preview.width * 360f;
            m_previewRenderer.RenderBuilding(m_optionPanel.m_optionItem);
        }

        private void PopulateList()
        {
            m_fastList.rowsData.Clear();
            m_fastList.selectedIndex = -1;
            for (int i = 0; i < m_optionsList.Length; i++)
            {
                var optionItem = m_optionsList[i];

                if (optionItem != null &&
                    (m_category.selectedIndex == 0) &&
                    (String.IsNullOrEmpty(m_search.text.Trim()) || m_optionsList[i].DisplayName.ToLower().Contains(m_search.text.Trim().ToLower())))
                {
                    m_fastList.rowsData.Add(m_optionsList[i]);
                }
            }

            m_fastList.rowHeight = 40f;
            m_fastList.DisplayAt(0);
            m_fastList.selectedIndex = 0;

            m_optionPanel.isVisible = m_fastList.rowsData.m_size > 0;
            m_followVehicle.isVisible = m_preview.parent.isVisible = m_optionPanel.isVisible;
        }

        private void FollowNextVehicle()
        {
            var buildings = BuildingManager.instance.m_buildings;
            OptionItem options = m_optionPanel.m_optionItem;

            for (uint i = (m_seekStart + 1) % buildings.m_size; i != m_seekStart; i = (i + 1) % buildings.m_size)
            {
                if (buildings.m_buffer[i].Info.name == m_optionPanel.m_optionItem.SystemName)
                {
                    InstanceID instanceID = default(InstanceID);
                    instanceID.Building = (ushort)i;

                    if (instanceID.IsEmpty || !InstanceManager.IsValid(instanceID))
                        continue;

                    Vector3 targetPosition;
                    Quaternion quaternion;
                    Vector3 vector;

                    if (!InstanceManager.GetPosition(instanceID, out targetPosition, out quaternion, out vector)) continue;

                    Vector3 pos = targetPosition;
                    GameAreaManager.instance.ClampPoint(ref targetPosition);
                    if (targetPosition != pos) continue;

                    m_cameraController.SetTarget(instanceID, ToolsModifierControl.cameraController.transform.position, false);

                    m_seekStart = (i + 1) % buildings.m_size;
                    return;
                }
            }
            m_seekStart = 0;
        }

        protected void OnSelectedItemChanged(UIComponent component, int i)
        {
            m_seekStart = 0;

            var optionItem = m_fastList.rowsData[i] as OptionItem;

            m_optionPanel.Show(optionItem);
            m_followVehicle.isVisible = m_preview.parent.isVisible = true;

            m_previewRenderer.cameraRotation = -60;// 120f;
            m_previewRenderer.zoom = 3f;

            m_previewRenderer.RenderBuilding(m_optionPanel.m_optionItem);
        }

        protected void OnEnableStateChanged(UIComponent component, bool state)
        {
            m_fastList.DisplayAt(m_fastList.listPosition);
        }
    }
}
