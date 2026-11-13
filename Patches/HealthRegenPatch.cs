using HarmonyLib;
using BetterHeals.Config;
using UnityEngine;

namespace BetterHeals.Patches
{
    public class RegenUpdater : MonoBehaviour
    {
        private float updateThrottle = 0f;

        public void Init()
        {
        }

        private void Update()
        {
            if (!SemiFunc.RunIsLevel())
            {
                return;
            }

            if (!Configuration.EnableHealthRegenPatch.Value) return;

            updateThrottle += Time.deltaTime;
            if (updateThrottle < Configuration.CustomRegenIntervalAmount.Value) return;
            updateThrottle = 0f;

            int healAmount = Configuration.HealthRegenAmount.Value;

            foreach (PlayerAvatar player in GameDirector.instance.PlayerList)
            {
                if (player.playerHealth != null)
                {
                    int currentHealth = TeamHealsPlugin.health_ref(player.playerHealth);
                    if (currentHealth > 0)
                    {

                        player.playerHealth.HealOther(healAmount, false);
                    }
                }
            }
            TeamHealsPlugin.Log.LogInfo($"Players healed by regeneration: {healAmount}");
        }
    }


    static class HealthRegenPatch
    {
        [HarmonyPatch(typeof(LevelGenerator), "GenerateDone")]
        [HarmonyPostfix]
        static void Start_Postfix(LevelGenerator __instance)
        {
            if (!SemiFunc.RunIsLevel())
            {
                return;
            }
            if (!SemiFunc.IsMasterClientOrSingleplayer()) return;

            if (__instance != null)
            {
                GameObject gameObject = __instance.gameObject;
                if (gameObject.GetComponent<RegenUpdater>() == null)
                {
                    gameObject.AddComponent<RegenUpdater>().Init();
                    TeamHealsPlugin.Log.LogInfo($"RegenUpdater attached to {__instance.name}.");
                }
            }
        }
    }
}
