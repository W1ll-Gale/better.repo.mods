using HarmonyLib;

namespace REPOTeamBoosters.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerHealth), nameof(ItemUpgradePlayerHealth.Upgrade))]
    internal class ItemUpgradePlayerHealthPatch
    {
        static bool Prefix(ItemUpgradePlayerHealth __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerHealth(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.Log.LogInfo($"Upgraded Player Health for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.Log.LogInfo("Applied Player Health upgrade to all players.");

            return false;
        }
    }
}