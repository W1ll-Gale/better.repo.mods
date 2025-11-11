using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradeDeathHeadBattery), nameof(ItemUpgradeDeathHeadBattery.Upgrade))]
    class ItemUpgradeDeathHeadBatteryPatch
    {
        static bool Prefix(ItemUpgradePlayerTumbleLaunch __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradeDeathHeadBattery(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.Log.LogInfo($"Upgraded Death Head Battery for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.Log.LogInfo("Applied Death Head Battery upgrade to all players.");

            return false;
        }
    }
}