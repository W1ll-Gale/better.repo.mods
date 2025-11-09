using HarmonyLib;
using Photon.Pun;
using System.Reflection;
using TeamHeals.Config;

namespace TeamHeals.Patches
{
    static class ExtractionPointHealPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ExtractionPoint), "StateSet")]
        static void StateSet_Postfix(ExtractionPoint __instance, ExtractionPoint.State newState)
        {
            if (newState != ExtractionPoint.State.Complete || SemiFunc.RunIsShop())
                return;

            if (!PhotonNetwork.IsMasterClient)
                return;

            int healAmount = Configuration.ExtractionHealAmount.Value;
            foreach (PlayerAvatar player in GameDirector.instance.PlayerList)
            {
                if (player != null && player.playerHealth != null)
                {
                    if (Configuration.EnableFullReviveHealth.Value)
                    {
                        int health = TeamHealsPlugin.health_ref(player.playerHealth);
                        int maxHealth = TeamHealsPlugin.max_health_ref(player.playerHealth);
                        healAmount = maxHealth - health;
                    }
                    FieldInfo healthField = typeof(PlayerHealth).GetField("health", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                    int currentHealth = healthField != null ? (int)healthField.GetValue(player.playerHealth) : 1;
                    FieldInfo isDisabledField = typeof(PlayerAvatar).GetField("isDisabled", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                    bool isDisabled = isDisabledField != null ? (bool)isDisabledField.GetValue(player) : false;

                    if (currentHealth > 0 && !isDisabled)
                    {
                        player.playerHealth.HealOther(healAmount, true);
                    }
                }
            }
            TeamHealsPlugin.Log.LogInfo($"All alive players healed to {healAmount} after extraction.");
        }
    }
}
