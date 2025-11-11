using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerExtraJump), nameof(ItemUpgradePlayerExtraJump.Upgrade))]
    internal class ItemUpgradePlayerExtraJumpPatch
    {
        static bool Prefix(ItemUpgradePlayerExtraJump __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerExtraJump(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.Log.LogInfo($"Upgraded Player Extra Jump for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.Log.LogInfo("Applied Player Extra Jump upgrade to all players.");

            return false;
        }
    }
}