using HarmonyLib;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BetterHeals.Patches
{
    static class FullHealthAtStartPatch
    {
        [HarmonyPatch(typeof(LevelGenerator), "GenerateDone")]
        [HarmonyPostfix]
        static void GenerateDone_Postfix()
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

            if (PhotonNetwork.IsMasterClient)
            {
                new GameObject("HealCoroutineRunner").AddComponent<HealRunner>();
            }
        }
    }

    public class HealRunner : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(HealAllPlayersToFull());
        }

        private IEnumerator HealAllPlayersToFull()
        {
            yield return new WaitForSeconds(1.0f);

            TeamHealsPlugin.Log.LogInfo("Proceeding to heal all players to full health.");

            if (GameDirector.instance == null || GameDirector.instance.PlayerList == null)
            {
                TeamHealsPlugin.Log.LogWarning("GameDirector.instance or PlayerList is null.");
                Destroy(gameObject); 
                yield break;
            }

            List<PlayerAvatar> players = GameDirector.instance.PlayerList;
            FieldInfo healthField = typeof(PlayerHealth).GetField("health", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo maxHealthField = typeof(PlayerHealth).GetField("maxHealth", BindingFlags.Instance | BindingFlags.NonPublic);

            if (healthField == null || maxHealthField == null)
            {
                TeamHealsPlugin.Log.LogError("Could not find health or maxHealth fields via reflection.");
                Destroy(gameObject);
                yield break;
            }

            foreach (PlayerAvatar player in players)
            {
                if (player != null && player.playerHealth != null)
                {
                    int health = (int)healthField.GetValue(player.playerHealth);
                    int maxHealth = (int)maxHealthField.GetValue(player.playerHealth);
                    int healAmount = maxHealth - health;

                    TeamHealsPlugin.Log.LogInfo($"Player {player.photonView.Owner.NickName} current health: {health}, max health: {maxHealth}, calculated heal amount: {healAmount}");

                    if (healAmount > 0)
                    {
                        player.playerHealth.HealOther(healAmount, true);
                        TeamHealsPlugin.Log.LogInfo($"Healed player {player.photonView.Owner.NickName} to full health: {maxHealth}");
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
