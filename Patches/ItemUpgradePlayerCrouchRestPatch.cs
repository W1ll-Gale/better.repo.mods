using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerCrouchRest), nameof(ItemUpgradePlayerCrouchRest.Upgrade))]
    class ItemUpgradePlayerCrouchRestPatch
    {
        static bool Prefix(ItemUpgradePlayerCrouchRest __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerCrouchRest(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.mls.LogInfo($"Upgraded Player Crouch Rest for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.mls.LogInfo("Applied Player Crouch Rest upgrade to all players.");

            return false;
        }
    }
}