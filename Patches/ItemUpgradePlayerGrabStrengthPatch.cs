using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerGrabStrength), nameof(ItemUpgradePlayerGrabStrength.Upgrade))]
    class ItemUpgradePlayerGrabStrengthPatch
    {
        static bool Prefix(ItemUpgradePlayerGrabStrength __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerGrabStrength(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.mls.LogInfo($"Upgraded Player Grab Strength for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.mls.LogInfo("Applied Player Grab Strength upgrade to all players.");

            return false;
        }
    }
}