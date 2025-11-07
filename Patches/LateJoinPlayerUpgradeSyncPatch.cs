using HarmonyLib;
using REPOTeamBoosters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamUpgrades.Patches
{
    [HarmonyPatch(typeof(LevelGenerator), nameof(LevelGenerator.PlayerSpawn))]
    class LateJoinPlayerUpgradeSyncPatch
    {
        [HarmonyPostfix]
        private static void UpgradeLateJoinPlayer()
        {
            List<PlayerAvatar> players = SemiFunc.PlayerGetAll();
            if (players == null || !players.Any())
            {
                return;
            }

            List<string> steamIDs = players.Select(p => SemiFunc.PlayerGetSteamID(p)).ToList();

            SyncUpgrade("playerUpgradeDeathHeadBattery", steamIDs, steamID => PunManager.instance.UpgradeDeathHeadBattery(steamID));
            SyncUpgrade("playerUpgradeCrouchRest", steamIDs, steamID => PunManager.instance.UpgradePlayerCrouchRest(steamID));
            SyncUpgrade("playerUpgradeLaunch", steamIDs, steamID => PunManager.instance.UpgradePlayerTumbleLaunch(steamID));
            SyncUpgrade("playerUpgradeTumbleClimb", steamIDs, steamID => PunManager.instance.UpgradePlayerTumbleClimb(steamID));
            SyncUpgrade("playerUpgradeThrow", steamIDs, steamID => PunManager.instance.UpgradePlayerThrowStrength(steamID));
            SyncUpgrade("playerUpgradeStrength", steamIDs, steamID => PunManager.instance.UpgradePlayerGrabStrength(steamID));
            SyncUpgrade("playerUpgradeSpeed", steamIDs, steamID => PunManager.instance.UpgradePlayerSprintSpeed(steamID));
            SyncUpgrade("playerUpgradeHealth", steamIDs, steamID => PunManager.instance.UpgradePlayerHealth(steamID));
            SyncUpgrade("playerUpgradeMapPlayerCount", steamIDs, steamID => PunManager.instance.UpgradeMapPlayerCount(steamID));
            SyncUpgrade("playerUpgradeStamina", steamIDs, steamID => PunManager.instance.UpgradePlayerEnergy(steamID));
            SyncUpgrade("playerUpgradeExtraJump", steamIDs, steamID => PunManager.instance.UpgradePlayerExtraJump(steamID));
            SyncUpgrade("playerUpgradeRange", steamIDs, steamID => PunManager.instance.UpgradePlayerGrabRange(steamID));
            SyncUpgrade("playerUpgradeTumbleWings", steamIDs, steamID => PunManager.instance.UpgradePlayerTumbleWings(steamID));

            TeamBoostersBase.mls.LogInfo("Synchronized all upgrades for all players.");
        }

        private static void SyncUpgrade(string upgradeDictionaryName, List<string> steamIDs, Action<string> upgradeAction)
        {
            if (StatsManager.instance.dictionaryOfDictionaries.TryGetValue(upgradeDictionaryName, out var upgradeDict))
            {
                int maxLevel = 0;
                foreach (string steamID in steamIDs)
                {
                    if (upgradeDict.TryGetValue(steamID, out int level) && level > maxLevel)
                    {
                        maxLevel = level;

                    }
                }

                if (maxLevel > 0)
                {
                    foreach (string steamID in steamIDs)
                    {
                        if (upgradeDict.TryGetValue(steamID, out int currentLevel) && currentLevel < maxLevel)
                        {
                            for (int i = currentLevel; i < maxLevel; i++)
                            {
                                upgradeAction(steamID);
                            }
                        }
                    }
                    TeamBoostersBase.mls.LogInfo($"Synchronized upgrade '{upgradeDictionaryName}' to level {maxLevel} for all players.");
                }
            }
        }
    }
}
