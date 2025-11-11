using BepInEx.Configuration;


namespace TeamUpgrades.Config
{
    internal class Configuration
    {
        public static ConfigEntry<bool> EnableLateJoinPlayerUpdateSyncPatch;
        public static ConfigEntry<bool> EnableItemUpgradeMapPlayerCountPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerEnergyPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerExtraJumpPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerGrabRangePatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerGrabStrengthPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerGrabThrowPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerHealthPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerSprintSpeedPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerTumbleLaunchPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerTumbleWingsPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerTumbleClimbPatch;
        public static ConfigEntry<bool> EnableItemUpgradeDeathHeadBatteryPatch;
        public static ConfigEntry<bool> EnableItemUpgradePlayerCrouchRestPatch;

        public static void Init(ConfigFile config)
        {
            EnableLateJoinPlayerUpdateSyncPatch = config.Bind<bool>(
                "Late Join Settings",
                "EnableLateJoinPlayerUpgradeSync",
                false,
                "Enables Upgrade Sync for Late Joining Players"
            );

            EnableItemUpgradeMapPlayerCountPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradeMapPlayer",
                true,
                "Enables Team Upgrades for Map Player Count Upgrade"
            );

            EnableItemUpgradePlayerEnergyPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerEnergy",
                true,
                "Enables Team Upgrades for Player Energy Upgrade"
            );

            EnableItemUpgradePlayerExtraJumpPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerExtraJump",
                true,
                "Enables Team Upgrades for Player Extra Jump Upgrade"
            );

            EnableItemUpgradePlayerGrabRangePatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerGrabRange",
                true,
                "Enables Team Upgrades for Player Grab Range Upgrade"
            );

            EnableItemUpgradePlayerGrabStrengthPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerGrabStrength",
                true,
                "Enables Team Upgrades for Player Grab Strength Upgrade"
            );

            EnableItemUpgradePlayerGrabThrowPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerGrabThrow",
                true,
                "Enables Team Upgrades for Player Grab Throw Upgrade"
            );

            EnableItemUpgradePlayerHealthPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerHealth",
                true,
                "Enables Team Upgrades for Player Health Upgrade"
            );

            EnableItemUpgradePlayerSprintSpeedPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerSprintSpeed",
                true,
                "Enables Team Upgrades for Player Sprint Speed Upgrade"
            );

            EnableItemUpgradePlayerTumbleLaunchPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerTumbleLaunch",
                true,
                "Enables Team Upgrades for Player Tumble Launch Upgrade"
            );

            EnableItemUpgradePlayerTumbleWingsPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerTumbleWings",
                true,
                "Enables Team Upgrades for Player Tumble Wings Upgrade"
            );

            EnableItemUpgradePlayerTumbleClimbPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerTumbleClimb",
                true,
                "Enables Team Upgrades for Player Tumble Climb Upgrade"
            );

            EnableItemUpgradeDeathHeadBatteryPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradeDeathHeadBattery",
                true,
                "Enables Team Upgrades for Death Head Battery Upgrade"
            );

            EnableItemUpgradePlayerCrouchRestPatch = config.Bind<bool>(
                "Upgrade Sync Settings",
                "EnableUpgradePlayerCrouchRest",
                true,
                "Enables Team Upgrades for Player Crouch Rest Upgrade"
            );

        }
    }
}