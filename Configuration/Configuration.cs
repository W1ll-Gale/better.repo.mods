using BepInEx.Configuration;

namespace BetterHeals.Config
{
    internal class Configuration
    {
        public static ConfigEntry<bool> EnableFullHealthAtStartPatch;
        public static ConfigEntry<bool> EnableTeamHealthPackPatch;
        public static ConfigEntry<bool> EnableEqualSplitTeamHealth;
        public static ConfigEntry<bool> EnableHealthRegenPatch;
        public static ConfigEntry<int> HealthRegenAmount;
        public static ConfigEntry<int> CustomRegenIntervalAmount;
        public static ConfigEntry<float> HealAmountMultiplier;
        public static ConfigEntry<bool> EnableCustomReviveHealthPatch;
        public static ConfigEntry<int> CustomReviveHealthAmount;
        public static ConfigEntry<bool> EnableFullReviveHealth;
        public static ConfigEntry<bool> EnableExtractionHealPatch;
        public static ConfigEntry<bool> EnableExtractionFullHeal;
        public static ConfigEntry<int> ExtractionHealAmount;      

        public static void Init(ConfigFile config)
        {
            HealAmountMultiplier = config.Bind<float>(
                "General Health Settings",
                "HealthPackAmountMultiplier",
                1f,
                new ConfigDescription(
                    "Multiplier applied to the health pack healing",
                    new AcceptableValueRange<float>(0.1f, 10f)
                )
            );
            EnableFullHealthAtStartPatch = config.Bind<bool>(
                "General Health Settings",
                "EnableFullHealthAtStart",
                false,
                "If enabled, players will start the match with full health"
            );
            EnableHealthRegenPatch = config.Bind<bool>(
                "General Health Settings",
                "EnableHealthRegen",
                false,
                "If enabled, players will slowly regenerate health over time"
            );
            HealthRegenAmount = config.Bind<int>(
                "General Health Settings",
                "HealthRegenAmount",
                10,
                new ConfigDescription(
                    "The amount of health players will regenerate each interval if the Health Regeneration patch is enabled",
                    new AcceptableValueRange<int>(1, 20)
                )
            );
            CustomRegenIntervalAmount = config.Bind<int>(
                "General Health Settings",
                "CustomRegenIntervalAmount",
                60,
                new ConfigDescription(
                    "The interval in seconds at which players will regenerate health if the Health Regeneration patch is enabled",
                    new AcceptableValueRange<int>(1, 120)
                )
            );
            EnableExtractionHealPatch = config.Bind<bool>(
                "General Health Settings",
                "EnableExtractionHeal",
                true,
                "Enable the patch that heals alive players after extraction is completed"
            );
            EnableExtractionFullHeal = config.Bind<bool>(
                "General Health Settings",
                "ExtractionFullHeal",
                false,
                "If enabled, players will be healed to full health upon extraction regardless of the ExtractionHealAmount setting"
            );
            ExtractionHealAmount = config.Bind<int>(
                "General Health Settings",
                "ExtractionHealAmount",
                20,
                new ConfigDescription(
                    "The health that alive players receive after extraction is completed",
                    new AcceptableValueRange<int>(0, 200)
                )
            );
            EnableTeamHealthPackPatch = config.Bind<bool>(
                "Team Health Settings",
                "EnableTeamHealthPack",
                true,
                "Enable the patch that allows health packs to heal all teammates"
            );
            EnableEqualSplitTeamHealth = config.Bind<bool>(
                "Team Health Settings",
                "EnableEqualSplitTeamHealth",
                false,
                "If enabled, health packs will split their healing amount equally among all teammates"
            );
            EnableCustomReviveHealthPatch = config.Bind<bool>(
                "Team Health Settings",
                "EnableCustomReviveHealth",
                true,
                "Enable the patch that sets a custom value to the health received when a player is revived"
            );

            EnableFullReviveHealth = config.Bind<bool>(
                "Team Health Settings",
                "FullReviveHealth",
                false,
                "If enabled, players will be revived with full health regardless of the CustomReviveHealthAmount setting"
            );

            CustomReviveHealthAmount = config.Bind<int>(
                "Team Health Settings",
                "CustomReviveHealthAmount",
                20,
                new ConfigDescription(
                    "The health that a player receives upon revival",
                    new AcceptableValueRange<int>(1, 100)
                )
            );
        }
    }
}