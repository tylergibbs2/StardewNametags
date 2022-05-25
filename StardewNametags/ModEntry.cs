using HarmonyLib;
using StardewModdingAPI;
using StardewValley;

namespace StardewNametags
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        public static bool DisplayNames = true;

        public static bool AllowToggle = true;

        public ModEntry()
        {
            Harmony harmony = new("tylergibbs2.stardewnametags");
            harmony.PatchAll();
        }
        public override void Entry(IModHelper helper)
        {
            ModConfig config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.SaveLoaded += (o, e) =>
            {
                config = helper.ReadConfig<ModConfig>();  // Re-read the config when saves are loaded.
                if (config.MultiplayerOnly && !Game1.IsMultiplayer)
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
                if (config.ToggleKey.JustPressed() && AllowToggle)
                    DisplayNames = !DisplayNames;
            };
        }
    }
}
