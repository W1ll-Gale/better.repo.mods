using HarmonyLib;
using REPOTeamBoosters;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(ItemUpgradePlayerTumbleWings), nameof(ItemUpgradePlayerTumbleWings.Upgrade))]
    class ItemUpgradePlayerTumbleWingsPatch
    {
        static bool Prefix(ItemUpgradePlayerTumbleWings __instance)
        {
            var players = SemiFunc.PlayerGetAll();

            foreach (var player in players)
            {
                PunManager.instance.UpgradePlayerTumbleWings(SemiFunc.PlayerGetSteamID(player));
                TeamBoostersBase.Log.LogInfo($"Upgraded Player Tumble Wings for player {SemiFunc.PlayerGetSteamID(player)}");
            }

            TeamBoostersBase.Log.LogInfo("Applied Player Tumble Wings upgrade to all players.");

            return false;
        }
    }
}