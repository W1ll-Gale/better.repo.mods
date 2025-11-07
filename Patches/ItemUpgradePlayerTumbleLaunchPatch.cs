using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerTumbleLaunch), nameof(ItemUpgradePlayerTumbleLaunch.Upgrade))]
    class ItemUpgradePlayerTumbleLaunchPatch
    {
        static bool Prefix(ItemUpgradePlayerTumbleLaunch __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerTumbleLaunch(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.mls.LogInfo($"Upgraded Player Tumble Launch for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.mls.LogInfo("Applied Player Tumble Launch upgrade to all players.");

            return false;
        }
    }
}