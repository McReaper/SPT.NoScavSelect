using System.Reflection;
using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace NoScavSelect.Patches
{
    /// <summary>
    /// In SPT 4.x, pressing Play calls MainMenuShowOperation.method_44, which opens the PMC/Scav side
    /// selection screen; its Next button only sets RaidSettings.Side and calls method_45 (map selection).
    /// We skip the screen entirely: force the side to PMC and go straight to method_45.
    /// Back on the map screen then returns to the main menu instead of the side screen.
    /// </summary>
    internal class SkipSideSelect : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(MainMenuShowOperation), nameof(MainMenuShowOperation.method_44));
        }

        [PatchPrefix]
        private static bool Prefix(MainMenuShowOperation __instance)
        {
            __instance.raidSettings_0.Side = ESideType.Pmc;
            __instance.method_45().HandleExceptions();
            return false;
        }
    }
}
