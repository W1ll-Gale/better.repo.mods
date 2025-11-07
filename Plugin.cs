using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using REPOTeamBoosters.Patches;
using TeamUpgrades.Configuration;
using TeamUpgrades.Patches;


using PatchInfo = System.ValueTuple<BepInEx.Configuration.ConfigEntry<bool>, System.Action, string>;


namespace REPOTeamBoosters
{
    [BepInPlugin(mod_guid, mod_name, mod_version)]
    public class TeamBoostersBase : BaseUnityPlugin
    {
        private const string mod_guid = "MrBytesized.REPO.BetterTeamBoosters";
        private const string mod_name = "Better Team Boosters";
        private const string mod_version = "1.2.2";

        private readonly Harmony harmony = new Harmony(mod_guid);

        private static TeamBoostersBase instance;

        internal static ManualLogSource mls;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(mod_guid);

            harmony.PatchAll(typeof(TeamBoostersBase));

            Configuration.Init(Config);

            var patches = new PatchInfo[]
            {
                (
                Configuration.EnableItemUpgradeMapPlayerCountPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradeMapPlayerCountPatch)),
                    "Map Player Count Upgrade"
                ),
                (
                Configuration.EnableItemUpgradePlayerEnergyPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerEnergyPatch)),
                    "Player Energy Upgrade"
                ),
                (
                Configuration.EnableItemUpgradePlayerExtraJumpPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerExtraJumpPatch)),
                    "Player Extra Jump Upgrade"
                ),
                (
                Configuration.EnableItemUpgradePlayerGrabRangePatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerGrabRangePatch)),
                    "Player Grab Range Upgrade"
                ),
                (
                Configuration.EnableItemUpgradePlayerGrabStrengthPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerGrabStrengthPatch)),
                    "Player Grab Strength Upgrade"
                ),
                (
                Configuration.EnableItemUpgradePlayerGrabThrowPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerGrabThrowPatch)),
                    "Player Grab Throw Upgrade"
                ),
                (
                Configuration.EnableItemUpgradePlayerHealthPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerHealthPatch)),
                    "Player Health Upgrade"
                ),
                (
                    Configuration.EnableItemUpgradePlayerSprintSpeedPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerSprintSpeedPatch)),
                    "Player Sprint Speed Upgrade"
                ),
                (
                    Configuration.EnableItemUpgradePlayerTumbleLaunchPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerTumbleLaunchPatch)),
                    "Player Thumle Lauch Upgrade"
                ),
                (
                    Configuration.EnableItemUpgradePlayerTumbleWingsPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerTumbleWingsPatch)),
                    "Player Tumble Launch Upgrade"
                ),
                (
                    Configuration.EnableItemUpgradePlayerTumbleClimbPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerTumbleClimbPatch)),
                    "Player Tumble Launch Upgrade"
                ),
                (
                    Configuration.EnableItemUpgradeDeathHeadBatteryPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradeDeathHeadBatteryPatch)),
                    "Player Tumble Launch Upgrade"
                ),
                (
                    Configuration.EnableItemUpgradePlayerCrouchRestPatch,
                    () => harmony.PatchAll(typeof(ItemUpgradePlayerCrouchRestPatch)),
                    "Player Crouch Rest Upgrade"
                )
            };

            foreach (var (is_patch_enabled, apply_patch, patch_name) in patches)
            {
                if (is_patch_enabled.Value)
                {
                    apply_patch();
                    mls.LogInfo($"{patch_name} patch is applied");
                }
            }

            mls.LogInfo("Better Team Boosters mod has been activated");
        }
    }
}