using BepInEx;
using BepInEx.Logging;
using NoScavSelect.Patches;

namespace NoScavSelect
{
    [BepInPlugin("com.mcreaper.noscavselect", "No Scav Select", "2.0.0")]
    [BepInDependency("com.SPT.core", "4.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;

        private void Awake()
        {
            LogSource = Logger;

            new SkipSideSelect().Enable();

            LogSource.LogInfo("[No Scav Select] plugin loaded!");
        }
    }
}
