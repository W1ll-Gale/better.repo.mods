using HarmonyLib;
using Photon.Pun;
using TeamHeals.Config;
using UnityEngine;

namespace TeamHeals.Patches
{
    public class RegenUpdater : MonoBehaviour
    {
        private float updateThrottle = 0f;

        public void Init()
        {
        }

        private void Update()
        {
            RunManager rm = RunManager.instance;
            if (rm == null ||
                rm.levelCurrent == rm.levelMainMenu ||
                rm.levelCurrent == rm.levelLobbyMenu ||
                rm.levelCurrent == rm.levelLobby ||
                rm.levelCurrent == rm.levelShop ||
                rm.levelCurrent == rm.levelRecording ||
                rm.levelCurrent == rm.levelSplashScreen)
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
        static void Start_Postfix(PlayerController __instance)
        {
            RunManager rm = RunManager.instance;
            if (rm == null ||
                rm.levelCurrent == rm.levelMainMenu ||
                rm.levelCurrent == rm.levelLobbyMenu ||
                rm.levelCurrent == rm.levelLobby ||
                rm.levelCurrent == rm.levelShop ||
                rm.levelCurrent == rm.levelRecording ||
                rm.levelCurrent == rm.levelSplashScreen)
            {
                return;
            }
            if (GameManager.Multiplayer() && !PhotonNetwork.IsMasterClient) return;

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
