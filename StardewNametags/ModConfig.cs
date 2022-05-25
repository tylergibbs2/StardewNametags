using StardewModdingAPI.Utilities;

namespace StardewNametags
{
    public class ModConfig
    {
        public bool MultiplayerOnly { get; set; } = true;
        public KeybindList ToggleKey { get; set; } = KeybindList.Parse("F1");
    }
}
