using HarmonyLib;

namespace REPOTeamBoosters.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerSprintSpeed), nameof(ItemUpgradePlayerSprintSpeed.Upgrade))]
    internal class ItemUpgradePlayerSprintSpeedPatch
    {
        static bool Prefix(ItemUpgradePlayerSprintSpeed __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerSprintSpeed(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.mls.LogInfo($"Upgraded Player Sprint Speed for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.mls.LogInfo("Applied Player Sprint Speed upgrade to all players.");

            return false;
        }
    }
}