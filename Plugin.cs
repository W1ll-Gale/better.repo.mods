using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using TeamHeals.Config;
using TeamHeals.Patches;
using UnityEngine;

namespace TeamHeals
{
    [BepInPlugin(mod_guid, mod_name, mod_version)]
    public class TeamHealsPlugin : BaseUnityPlugin
    {
        private const string mod_guid = "MrBytesized.REPO.BetterTeamHeals";
        private const string mod_name = "Better Team Heals";
        private const string mod_version = "2.0.0";

        private readonly Harmony harmony = new Harmony(mod_guid);

        private static TeamHealsPlugin instance;

        internal static ManualLogSource Log;

        internal static readonly AccessTools.FieldRef<ItemHealthPack, ItemToggle>
        item_toggle_ref = AccessTools.FieldRefAccess<ItemHealthPack, ItemToggle>("itemToggle");

        internal static readonly AccessTools.FieldRef<ItemToggle, int>
            player_photon_id_ref = AccessTools.FieldRefAccess<ItemToggle, int>("playerTogglePhotonID");

        internal static readonly AccessTools.FieldRef<PlayerHealth, int>
            health_ref = AccessTools.FieldRefAccess<PlayerHealth, int>("health");

        internal static readonly AccessTools.FieldRef<PlayerHealth, int>
            max_health_ref = AccessTools.FieldRefAccess<PlayerHealth, int>("maxHealth");

        private (ConfigEntry<bool> configEntry, Action enablePatch, Action disablePatch, string description)[] patchArray;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            Configuration.Init(Config);

            Log = BepInEx.Logging.Logger.CreateLogSource(mod_guid);

            Log.LogInfo("Team Heals mod has been activated");

            harmony.PatchAll(typeof(TeamHealsPlugin));
            harmony.PatchAll(typeof(PunManagerPatch));
            Log.LogInfo("PunManager patch applied.");
            harmony.PatchAll(typeof(ItemHealthPackPatch));
            Log.LogInfo("ItemHealthPack patch applied.");

            patchArray = new (ConfigEntry<bool>, Action, Action, string)[]
            {
                (Configuration.EnableCustomReviveHealthPatch,
                    () => harmony.PatchAll(typeof(PlayerReviveHealthPatch)),
                    () => harmony.UnpatchSelf(),
                    "Custom Revive Health"),
                (Configuration.EnableExtractionHealPatch,
                    () => harmony.PatchAll(typeof(ExtractionPointHealPatch)),
                    () => harmony.UnpatchSelf(),
                    "Extraction Heal"),
                (Configuration.EnableHealthRegenPatch,
                    () => harmony.PatchAll(typeof(HealthRegenPatch)),
                    () => harmony.UnpatchSelf(),
                    "Health Regeneration"),
                (Configuration.EnableFullHealthAtStartPatch,
                    () => harmony.PatchAll(typeof(FullHealthAtStartPatch)),
                    () => harmony.UnpatchSelf(),
                    "Full Health At Start"),
            };

            foreach (var (configEntry, enablePatch, disablePatch, description) in patchArray)
            {
                UpdatePatchFromConfig(configEntry, enablePatch, disablePatch, description);
                configEntry.SettingChanged += (sender, args) => UpdatePatchFromConfig(configEntry, enablePatch, disablePatch, description);
            }

            PatchTeamHealing(Configuration.EnableTeamHealthPackPatch.Value);
            Configuration.EnableTeamHealthPackPatch.SettingChanged += (sender, args) =>
            {
                PatchTeamHealing(Configuration.EnableTeamHealthPackPatch.Value);
            };
        }

        private void UpdatePatchFromConfig(
            ConfigEntry<bool> configEntry,
            Action enablePatch,
            Action disablePatch,
            string description)
        {
            if (configEntry.Value)
            {
                enablePatch.Invoke();
                Log.LogInfo($"{description} patch enabled.");
            }
            else
            {
                disablePatch.Invoke();
                Log.LogInfo($"{description} patch disabled.");
            }
        }

        private void PatchTeamHealing(bool enable)
        {
            if (enable)
            {
                harmony.Patch(
                    AccessTools.Method(typeof(ItemHealthPack), "UsedRPC"),
                    postfix: new HarmonyMethod(typeof(ItemHealthPackPatch), "TeamHealthPackSync")
                ); Log.LogInfo("Health Pack Team Healing patch enabled.");
            }
            else
            {
                harmony.Unpatch(
                    AccessTools.Method(typeof(ItemHealthPack), "UsedRPC"),
                    AccessTools.Method(typeof(ItemHealthPackPatch), "TeamHealthPackSync")
                ); Log.LogInfo("Health Pack Team Healing patch disabled by config.");
            }
        }
    }
}
