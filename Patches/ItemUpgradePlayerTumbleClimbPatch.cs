using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerTumbleClimb), nameof(ItemUpgradePlayerTumbleClimb.Upgrade))]
    class ItemUpgradePlayerTumbleClimbPatch
    {
        static bool Prefix(ItemUpgradePlayerTumbleClimb __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerTumbleClimb(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.Log.LogInfo($"Upgraded Player Tumble Climb for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.Log.LogInfo("Applied Player Tumble Climb upgrade to all players.");

            return false;
        }
    }
}