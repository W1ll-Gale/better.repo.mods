using BepInEx;
using HarmonyLib;

namespace REPOTeamBoosters.Patches
{
    [HarmonyPatch(typeof(ItemUpgradeMapPlayerCount), nameof(ItemUpgradeMapPlayerCount.Upgrade))]
    internal class ItemUpgradeMapPlayerCountPatch
    {
        static bool Prefix(ItemUpgradeMapPlayerCount __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradeMapPlayerCount(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.mls.LogInfo($"Upgraded Map Player Count for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.mls.LogInfo("Applied Map Player Count upgrade to all players.");

            return false;
        }
    }
}