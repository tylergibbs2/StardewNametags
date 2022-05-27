using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using System;

namespace StardewNametags
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        public static bool DisplayNames = true;

        public static bool AllowToggle = true;

        private static ModConfig Config;

        public ModEntry()
        {
            Harmony harmony = new("tylergibbs2.stardewnametags");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.SaveLoaded += (o, e) =>
            {
                if (Config.MultiplayerOnly && !Context.IsMultiplayer)
                {
                    DisplayNames = false;
                    AllowToggle = false;
                }
                else
                {
                    DisplayNames = true;
                    AllowToggle = true;
                }
            };

            helper.Events.Input.ButtonPressed += (o, e) =>
            {
                if (Config.ToggleKey.JustPressed() && AllowToggle)
                    DisplayNames = !DisplayNames;
            };
        }

        private static Color ConvertFromHex(string s)
        {
            if (s.Length != 7)
                return Color.Gray;

            int r = Convert.ToInt32(s.Substring(1, 2), 16);
            int g = Convert.ToInt32(s.Substring(3, 2), 16);
            int b = Convert.ToInt32(s.Substring(5, 2), 16);
            return new Color(r, g, b);
        }

        public static Color GetTextColor()
        {
            return ConvertFromHex(Config.TextColor);
        }

        public static bool GetShouldApplyOpacityToText()
        {
            return Config.AlsoApplyOpacityToText;
        }

        public static Color GetBackgroundColor()
        {
            return ConvertFromHex(Config.BackgroundColor);
        }

        public static float GetBackgroundOpacity()
        {
            return Config.BackgroundOpacity;
        }
    }
}
