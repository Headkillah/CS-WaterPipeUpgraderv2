using ColossalFramework.UI;
using System;
using UnityEngine;

namespace WaterPipeUpgraderv2
{


    public static class UIHelper
    {
        private static UIFont _font;

        public static UIButton CreateButton(UIComponent parent)
        {
            UIButton local1 = parent.AddUIComponent<UIButton>();
            local1.font = Font;
            local1.textPadding = new RectOffset(0, 0, 4, 0);
            local1.normalBgSprite = "ButtonMenu";
            local1.disabledBgSprite = "ButtonMenuDisabled";
            local1.hoveredBgSprite = "ButtonMenuHovered";
            local1.focusedBgSprite = "ButtonMenu";
            local1.pressedBgSprite = "ButtonMenuPressed";
            local1.textColor = new Color32(0xff, 0xff, 0xff, 0xff);
            local1.disabledTextColor = new Color32(7, 7, 7, 0xff);
            local1.hoveredTextColor = new Color32(0xff, 0xff, 0xff, 0xff);
            local1.focusedTextColor = new Color32(0xff, 0xff, 0xff, 0xff);
            local1.pressedTextColor = new Color32(30, 30, 0x2c, 0xff);
            return local1;
        }

        public static UIFont Font
        {
            get
            {
                if (_font == null)
                {
                    _font = GameObject.Find("(Library) PublicTransportInfoViewPanel").GetComponent<PublicTransportInfoViewPanel>().Find<UILabel>("Label").font;
                }
                return _font;
            }
        }
    }
}

