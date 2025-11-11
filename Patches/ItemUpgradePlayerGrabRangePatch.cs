using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerGrabRange), nameof(ItemUpgradePlayerGrabRange.Upgrade))]
    class ItemUpgradePlayerGrabRangePatch
    {
        static bool Prefix(ItemUpgradePlayerGrabRange __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerGrabRange(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.Log.LogInfo($"Upgraded Player Grab Range for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.Log.LogInfo("Applied Player Grab Range upgrade to all players.");

            return false;
        }
    }
}