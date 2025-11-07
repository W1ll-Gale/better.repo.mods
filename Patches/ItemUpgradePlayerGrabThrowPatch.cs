using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerGrabThrow), nameof(ItemUpgradePlayerGrabThrow.Upgrade))]
    class ItemUpgradePlayerGrabThrowPatch
    {
        static bool Prefix(ItemUpgradePlayerGrabThrow __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerThrowStrength(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.mls.LogInfo($"Upgraded Player Grab Throw for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.mls.LogInfo("Applied Player Grab Throw upgrade to all players.");

            return false;
        }
    }
}