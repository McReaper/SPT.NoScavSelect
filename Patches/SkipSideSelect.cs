using System.Reflection;
using EFT;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace NoScavSelect.Patches
{
    /// <summary>
    /// In SPT 4.x, pressing Play raises MenuScreen.OnMenuItemSelected(EMenuType.Play), which
    /// MainMenuShowOperation.ShowScreen routes to method_44: that opens the PMC/Scav side selection screen,
    /// whose Next button only sets RaidSettings.Side and calls method_45 (map selection).
    /// We intercept the Play entry in ShowScreen, force the side to PMC and go straight to method_45.
    /// method_44 itself is left alone, so mods that open the side selection screen directly
    /// (e.g. PitFireteam's "My Squad") still get it.
    /// Back on the map screen then returns to the main menu instead of the side screen.
    /// </summary>
    internal class SkipSideSelect : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(
                typeof(MainMenuShowOperation),
                nameof(MainMenuShowOperation.ShowScreen),
                new[] { typeof(EMenuType), typeof(bool) });
        }

        [PatchPrefix]
        private static bool Prefix(MainMenuShowOperation __instance, EMenuType screen)
        {
            if (screen != EMenuType.Play)
            {
                return true;
            }

            __instance.raidSettings_0.Side = ESideType.Pmc;
            __instance.method_45().HandleExceptions();
            return false;
        }
    }
}
