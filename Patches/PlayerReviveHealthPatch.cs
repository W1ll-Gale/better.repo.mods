using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterHeals.Config;

namespace BetterHeals.Patches
{
    static class PlayerReviveHealthPatch
    {
        [HarmonyPatch(typeof(PlayerAvatar), "ReviveRPC")]
        [HarmonyPostfix]
        static void ReviveRPC_Postfix(PlayerAvatar __instance)
        {
            int custom_revive_health = Configuration.CustomReviveHealthAmount.Value - 1;

            if (__instance.photonView.IsMine)
            {
                if (Configuration.EnableFullReviveHealth.Value)
                {
                    int health = TeamHealsPlugin.health_ref(__instance.playerHealth);
                    int maxHealth = TeamHealsPlugin.max_health_ref(__instance.playerHealth);
                    custom_revive_health = maxHealth - health;
                }
                __instance.playerHealth.HealOther(custom_revive_health, true);
                TeamHealsPlugin.Log.LogInfo($"Player revived with custom health: {custom_revive_health}");
            }
        }
    }
}
