using HarmonyLib;

namespace REPOTeamBoosters.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerEnergy), nameof(ItemUpgradePlayerEnergy.Upgrade))]
    internal class ItemUpgradePlayerEnergyPatch
    {
        static bool Prefix(ItemUpgradePlayerEnergy __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerEnergy(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.mls.LogInfo($"Upgraded Player Energy for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.mls.LogInfo("Applied Player Energy upgrade to all players.");

            return false;
        }
    }
}