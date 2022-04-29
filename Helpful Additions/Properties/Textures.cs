using UnityEngine;

namespace HelpfulAdditions.Properties {
    internal static class Textures {
        public static Sprite Blank => Resources.LoadSprite("Blank");

        #region UiIcons

        public static Sprite DeleteAllProjectilesButton => Resources.LoadSprite("UiIcons.DeleteAllProjectilesButton");

        public static Sprite DropDownButton => Resources.LoadSprite("UiIcons.DropDownButton");

        public static Sprite DropDownArrow => Resources.LoadSprite("UiIcons.DropDownArrow");

        public static Sprite SettingsIcon => Resources.LoadSprite("UiIcons.SettingsIcon");

        public static Sprite RoundInfoButton => Resources.LoadSprite("UiIcons.RoundInfoButton");

        public static Sprite PrevArrow => Resources.LoadSprite("UiIcons.PrevArrow");

        public static Sprite NextArrow => Resources.LoadSprite("UiIcons.NextArrow");

        public static Sprite TextInputBack => Resources.LoadSprite("UiIcons.TextInputBack");

        #endregion

        #region SettingsIcons

        public static Sprite DeleteAllProjectilesSettingsIcon => Resources.LoadSprite("SettingsIcons.DeleteAllProjectiles");

        public static Sprite SinglePlayerCoopSettingsIcon => Resources.LoadSprite("SettingsIcons.SinglePlayerCoop");

        public static Sprite PowersInSandboxSettingsIcon => Resources.LoadSprite("SettingsIcons.PowersInSandbox");

        public static Sprite RoundInfoScreenSettingsIcon => Resources.LoadSprite("SettingsIcons.RoundInfoScreen");

        public static Sprite RoundSetSwitcherSettingsIcon => Resources.LoadSprite("SettingsIcons.RoundSetSwitcher");

        public static Sprite BlonsInEditorSettingsIcon => Resources.LoadSprite("SettingsIcons.BlonsInEditor");

        #endregion

        #region UnknownBloon

        public static Sprite UnknownBloon => Resources.LoadSprite("Bloons.UnknownBloon");

        public static Sprite UnknownEdge => Resources.LoadSprite("Bloons.Bloons.Test.TestEdge");

        public static Sprite UnknownSpan => Resources.LoadSprite("Bloons.Bloons.Test.TestSpan");

        #endregion
    }
}
