using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace WaterPipeUpgraderv2
{
    public class Gui : UIPanel
    {
        private HeatingInfoViewPanel _heatingInfoViewPanel;
        private UILabel _pipeCount;
        private UILabel _upgradeCost;
        private UILabel _upgradeResult;
           
        private void CalculateUpgrade()
        {
            int totalCost = 0;
            ToolBase.ToolErrors none = ToolBase.ToolErrors.None;
            int num2 = this.UpgradeWaterToHeat(true, out totalCost, out none);
            this._upgradeCost.text = (totalCost * 0.01f).ToString(ColossalFramework.Globalization.Locale.Get("MONEY_FORMAT"), LocaleManager.cultureInfo);
            if (Singleton<EconomyManager>.instance.PeekResource(EconomyManager.Resource.Construction, totalCost) != totalCost)
            {
                this._upgradeCost.textColor = Color.red;
            }
            else
            {
                this._upgradeCost.textColor = Color.green;
            }
            this._pipeCount.text = num2.ToString();
        }

        private void CreatePanel()
        {
            base.name = "UpgradeWaterPipes";
            base.width = this._heatingInfoViewPanel.component.width;
            base.height = 150f;
            base.backgroundSprite = "MenuPanel2";
            this.canFocus = true;
            this.isInteractive = true;
            base.isVisible = false;
            base.relativePosition = new Vector3(this._heatingInfoViewPanel.component.absolutePosition.x, 241f);
            UILabel label = base.AddUIComponent<UILabel>();
            label.name = "Title";
            label.text = "Upgrade Water Pipes";
            label.textAlignment = UIHorizontalAlignment.Center;
            label.font = WaterPipeUpgraderv2.UIHelper.Font;
            label.position = new Vector3((base.width / 2f) - (label.width / 2f), -20f + (label.height / 2f));
            UIPanel local1 = base.AddUIComponent<UIPanel>();
            local1.name = "ContainerPanel";
            local1.anchor = UIAnchorStyle.Right | UIAnchorStyle.Left | UIAnchorStyle.Top;
            local1.transform.localPosition = Vector3.zero;
            local1.width = base.width;
            local1.height = base.height - 60f;
            local1.autoLayout = true;
            local1.autoLayoutDirection = LayoutDirection.Vertical;
            local1.autoLayoutPadding = new RectOffset(0, 0, 0, 1);
            local1.autoLayoutStart = LayoutStart.TopLeft;
            local1.relativePosition = new Vector3(0f, 50f);
            UIPanel parent = local1.AddUIComponent<UIPanel>();
            parent.width = base.width;
            parent.height = 30f;
            parent.autoLayoutDirection = LayoutDirection.Horizontal;
            parent.autoLayoutStart = LayoutStart.TopLeft;
            parent.autoLayoutPadding = new RectOffset(8, 0, 4, 4);
            parent.autoLayout = true;
            UILabel label2 = parent.AddUIComponent<UILabel>();
            label2.text = "Upgrade cost:";
            label2.font = WaterPipeUpgraderv2.UIHelper.Font;
            label2.textColor = Color.white;
            label2.textScale = 0.8f;
            label2.autoSize = false;
            label2.height = 30f;
            label2.width = 100f;
            label2.verticalAlignment = UIVerticalAlignment.Middle;
            label2 = parent.AddUIComponent<UILabel>();
            label2.text = 0.ToString(ColossalFramework.Globalization.Locale.Get("MONEY_FORMAT"), LocaleManager.cultureInfo);
            label2.font = WaterPipeUpgraderv2.UIHelper.Font;
            label2.textColor = Color.white;
            label2.textScale = 0.8f;
            label2.autoSize = false;
            label2.height = 30f;
            label2.width = 140f;
            label2.textAlignment = UIHorizontalAlignment.Right;
            label2.verticalAlignment = UIVerticalAlignment.Middle;
            this._upgradeCost = label2;
            UIButton button = WaterPipeUpgraderv2.UIHelper.CreateButton(parent);
            button.text = "Calculate";
            button.tooltip = "Calculates the upgrade cost and the pipe count.";
            button.textScale = 0.8f;
            button.width = 75f;
            button.height = 22f;
            button.eventClick += (c, p) => Singleton<SimulationManager>.instance.AddAction(new System.Action(this.CalculateUpgrade));
            UIPanel local3 = local1.AddUIComponent<UIPanel>();
            local3.width = base.width;
            local3.height = 30f;
            local3.autoLayoutDirection = LayoutDirection.Horizontal;
            local3.autoLayoutStart = LayoutStart.TopLeft;
            local3.autoLayoutPadding = new RectOffset(8, 0, 4, 4);
            local3.autoLayout = true;
            label2 = local3.AddUIComponent<UILabel>();
            label2.text = "Pipe count:";
            label2.font = WaterPipeUpgraderv2.UIHelper.Font;
            label2.textColor = Color.white;
            label2.textScale = 0.8f;
            label2.autoSize = false;
            label2.height = 30f;
            label2.width = 100f;
            label2.verticalAlignment = UIVerticalAlignment.Middle;
            label2 = local3.AddUIComponent<UILabel>();
            label2.text = "0";
            label2.font = WaterPipeUpgraderv2.UIHelper.Font;
            label2.textColor = Color.white;
            label2.textScale = 0.8f;
            label2.autoSize = false;
            label2.height = 30f;
            label2.width = 140f;
            label2.textAlignment = UIHorizontalAlignment.Right;
            label2.verticalAlignment = UIVerticalAlignment.Middle;
            this._pipeCount = label2;
            UIPanel local4 = local1.AddUIComponent<UIPanel>();
            local4.width = base.width;
            local4.height = 30f;
            local4.autoLayoutDirection = LayoutDirection.Horizontal;
            local4.autoLayoutStart = LayoutStart.TopLeft;
            local4.autoLayoutPadding = new RectOffset(8, 0, 4, 4);
            local4.autoLayout = true;
            button = WaterPipeUpgraderv2.UIHelper.CreateButton(local4);
            button.text = "Upgrade";
            button.tooltip = "Tries to upgrade all water pipes to heating pipes.";
            button.textScale = 0.8f;
            button.width = 75f;
            button.height = 22f;
            button.eventClick += (c, p) => Singleton<SimulationManager>.instance.AddAction(new System.Action(this.UpgradePipes));
            label2 = local4.AddUIComponent<UILabel>();
            label2.text = "";
            label2.font = WaterPipeUpgraderv2.UIHelper.Font;
            label2.textColor = Color.white;
            label2.textScale = 0.8f;
            label2.autoSize = false;
            label2.height = 30f;
            label2.width = (base.width - button.width) - 16f;
            label2.verticalAlignment = UIVerticalAlignment.Middle;
            this._upgradeResult = label2;
        }

        private void GetSegmentControlPoints(int segmentIndex, out NetTool.ControlPoint startPoint, out NetTool.ControlPoint middlePoint, out NetTool.ControlPoint endPoint)
        {
            NetManager instance = Singleton<NetManager>.instance;
            NetInfo info = instance.m_segments.m_buffer[segmentIndex].Info;
            startPoint.m_node = instance.m_segments.m_buffer[segmentIndex].m_startNode;
            startPoint.m_segment = 0;
            startPoint.m_position = instance.m_nodes.m_buffer[startPoint.m_node].m_position;
            startPoint.m_direction = instance.m_segments.m_buffer[segmentIndex].m_startDirection;
            startPoint.m_elevation = instance.m_nodes.m_buffer[startPoint.m_node].m_elevation;
            startPoint.m_outside = (instance.m_nodes.m_buffer[startPoint.m_node].m_flags & NetNode.Flags.Outside) > NetNode.Flags.None;
            endPoint.m_node = instance.m_segments.m_buffer[segmentIndex].m_endNode;
            endPoint.m_segment = 0;
            endPoint.m_position = instance.m_nodes.m_buffer[endPoint.m_node].m_position;
            endPoint.m_direction = -instance.m_segments.m_buffer[segmentIndex].m_endDirection;
            endPoint.m_elevation = instance.m_nodes.m_buffer[endPoint.m_node].m_elevation;
            endPoint.m_outside = (instance.m_nodes.m_buffer[endPoint.m_node].m_flags & NetNode.Flags.Outside) > NetNode.Flags.None;
            middlePoint.m_node = 0;
            middlePoint.m_segment = (ushort)segmentIndex;
            middlePoint.m_position = startPoint.m_position + ((Vector3)(startPoint.m_direction * (info.GetMinNodeDistance() + 1f)));
            middlePoint.m_direction = startPoint.m_direction;
            middlePoint.m_elevation = Mathf.Lerp(startPoint.m_elevation, endPoint.m_elevation, 0.5f);
            middlePoint.m_outside = false;
        }

        public override void OnDestroy()
        {
            if (this._heatingInfoViewPanel != null)
            {
                this._heatingInfoViewPanel.component.eventVisibilityChanged -= new ColossalFramework.UI.PropertyChangedEventHandler<bool>(this.ParentVisibilityChanged);
                this._heatingInfoViewPanel.component.eventPositionChanged -= new ColossalFramework.UI.PropertyChangedEventHandler<Vector2>(this.ParentPositionChanged);
            }
            base.OnDestroy();
        }

        private void ParentPositionChanged(UIComponent component, Vector2 value)
        {
            this.UpdatePosition();
        }

        private void ParentVisibilityChanged(UIComponent component, bool value)
        {
            base.isVisible = value;
            if (base.isVisible)
            {
                this.UpdatePosition();
                this._upgradeCost.text = "0";
                this._upgradeCost.textColor = Color.white;
                this._pipeCount.text = "0";
                this._upgradeResult.text = "";
            }
        }

        public override void Start()
        {
            try

            { //Begin Try

                base.Start();
                this._heatingInfoViewPanel = GameObject.Find("(Library) HeatingInfoViewPanel").GetComponent<HeatingInfoViewPanel>();
                if (this._heatingInfoViewPanel != null)
                {
                    this._heatingInfoViewPanel.component.eventVisibilityChanged += new ColossalFramework.UI.PropertyChangedEventHandler<bool>(this.ParentVisibilityChanged);
                    this._heatingInfoViewPanel.component.eventPositionChanged += new ColossalFramework.UI.PropertyChangedEventHandler<Vector2>(this.ParentPositionChanged);
                    this.CreatePanel();
                }

            } //End Try

            catch (Exception ex)
            { //Begin Catch

                Util.LogException(ex);

            } //End Catch
        }

        private void UpdatePosition()
        {
            base.relativePosition = this._heatingInfoViewPanel.component.absolutePosition + new Vector3(0f, this._heatingInfoViewPanel.component.size.y + 1f, 0f);
        }

        private void UpgradePipes()
        {
            try
            {
                int totalCost = 0;
                ToolBase.ToolErrors none = ToolBase.ToolErrors.None;
                int num2 = this.UpgradeWaterToHeat(false, out totalCost, out none);
                string str = "";
                if (none != ToolBase.ToolErrors.None)
                {
                    str = " Not enough money.";
                    this._upgradeResult.textColor = Color.red;
                }
                else
                {
                    this._upgradeResult.textColor = Color.green;
                }
                object[] objArray1 = new object[] { "Upgraded ", num2, " pipes.", str };
                this._upgradeResult.text = string.Concat(objArray1);
                Util.DebugPrint(this.name + Mod._version + " All pipes upgraded");
            }

            catch (Exception ex)
            {
                Util.LogException(ex);
            }
        }

        private int UpgradeWaterToHeat(bool test, out int totalCost, out ToolBase.ToolErrors errors)
        {
         
                int num = 0;
                totalCost = 0;
                errors = ToolBase.ToolErrors.None;
                NetInfo info = PrefabCollection<NetInfo>.FindLoaded("Heating Pipe");
                if (info == null)
                {
                    //Debug.Log("Couldn't find Heating Pipe, aborting.");
                    Util.Log("Couldn't find Heating Pipe, aborting.");
                    return num;
                }
                NetSegment[] buffer = Singleton<NetManager>.instance.m_segments.m_buffer;
                for (int i = 0; i < buffer.Length; i++)
                {
                    NetSegment segment = buffer[i];
                    if (((segment.Info != null) && (segment.Info.category == "WaterServices")) && (segment.m_flags != NetSegment.Flags.None))
                    {
                        NetTool.ControlPoint point;
                        NetTool.ControlPoint point2;
                        NetTool.ControlPoint point3;
                        this.GetSegmentControlPoints(i, out point, out point2, out point3);
                        bool visualize = false;
                        bool autoFix = true;
                        bool needMoney = true;
                        bool invert = false;
                        ushort node = 0;
                        ushort num4 = 0;
                        int cost = 0;
                        int productionRate = 0;
                        errors = NetTool.CreateNode(info, point, point2, point3, NetTool.m_nodePositionsSimulation, 0x3e8, test, visualize, autoFix, needMoney, invert, false, 0, out node, out num4, out cost, out productionRate);
                        if ((errors == ToolBase.ToolErrors.None) | test)
                        {
                            num++;
                            totalCost += cost;
                        }
                        else if ((errors & ToolBase.ToolErrors.NotEnoughMoney) != ToolBase.ToolErrors.None)
                        {
                            return num;
                        }
                    }
                }
                return num;
    }
    }
}

