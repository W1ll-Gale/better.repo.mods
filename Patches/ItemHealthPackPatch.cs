using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using TeamHeals.Config;

namespace TeamHeals.Patches
{
    static class ItemHealthPackPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ItemHealthPack), "Start")]
        static void OverrideHealAmount(ItemHealthPack __instance)
        {
            float multiplier = (float)Math.Round(Configuration.HealAmountMultiplier.Value, 4);

            if (multiplier == 1f)
            {
                TeamHealsPlugin.Log.LogInfo("Heal amount multiplier is 1. No changes made to health pack heal amount.");
                return;
            }

            __instance.healAmount = (int)Math.Ceiling(__instance.healAmount * multiplier);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ItemAttributes), "Start")]
        static void OverrideHealthPackName(ItemAttributes __instance)
        {
            if (__instance.item != null && __instance.item.itemType == SemiFunc.itemType.healthPack)
            {
                ItemHealthPack healthPack = __instance.GetComponent<ItemHealthPack>();
                if (healthPack != null)
                {
                    int newHealAmount = healthPack.healAmount;

                    FieldInfo itemNameField = typeof(ItemAttributes).GetField("itemName", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (itemNameField != null)
                    {
                        string oldName = (string)itemNameField.GetValue(__instance);
                        if (!string.IsNullOrEmpty(oldName))
                        {
                            string newName = System.Text.RegularExpressions.Regex.Replace(
                                oldName,
                                @"\(\d+\)",
                                $"({newHealAmount})"
                            );
                            itemNameField.SetValue(__instance, newName);
                            TeamHealsPlugin.Log.LogInfo($"Health pack name changed from '{oldName}' to '{newName}'");
                        }
                        else
                        {
                            TeamHealsPlugin.Log.LogWarning("itemName value is null or empty in ItemAttributes.Start postfix.");
                        }
                    }
                    else
                    {
                        TeamHealsPlugin.Log.LogWarning("itemName field not found in ItemAttributes.");
                    }
                }
                else
                {
                    TeamHealsPlugin.Log.LogWarning("ItemHealthPack component not found on ItemAttributes.");
                }
            }
        }

        //Haromony Postfix to heal all teammates when a health pack is manually patched in #Plugin.cs
        static void TeamHealthPackSync(ItemHealthPack __instance)
        {
            ItemToggle itemToggle = TeamHealsPlugin.item_toggle_ref(__instance);
            int userPhotonId = TeamHealsPlugin.player_photon_id_ref(itemToggle);

            PlayerAvatar healthPackUser = SemiFunc.PlayerAvatarGetFromPhotonID(userPhotonId);
            List<PlayerAvatar> players = SemiFunc.PlayerGetAll();

            foreach (PlayerAvatar player in players)
            {
                if (player.photonView.ViewID == healthPackUser.photonView.ViewID)
                    continue;

                if(Configuration.EnableEqualSplitTeamHealth.Value)
                {
                    player.playerHealth.HealOther(__instance.healAmount / players.Count, effect: true);
                }
                else
                {
                    player.playerHealth.HealOther(__instance.healAmount, effect: true);
                }

                TeamHealsPlugin.Log.LogInfo(
                    $"Healed player {player.photonView.ViewID} for {__instance.healAmount} HP " +
                    $"(used by {healthPackUser.photonView.ViewID})"
                );
            }

            TeamHealsPlugin.Log.LogInfo(
                $"Health pack used by player {healthPackUser.photonView.ViewID} healed all teammates"
            );
        }
    }
}
