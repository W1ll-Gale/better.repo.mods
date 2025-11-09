using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HarmonyLib;
using TeamHeals.Config;

namespace TeamHeals.Patches
{
    [HarmonyPatch(typeof(PunManager), nameof(PunManager.ShopPopulateItemVolumes))]
    internal class PunManagerPatch
    {
        private static readonly AccessTools.FieldRef<PunManager, ShopManager>
            shop_manager_ref = AccessTools.FieldRefAccess<PunManager, ShopManager>("shopManager");


        static public bool Prefix(PunManager __instance)
        {
            ref ShopManager shop_manager = ref shop_manager_ref(__instance);
            List<Item> health_packs = new HashSet<Item>(shop_manager.potentialItemHealthPacks).Distinct().ToList();

            for (int index = 0; index < health_packs.Count; ++index)
            {
                string item_name = health_packs[index].itemName;

                string result = Regex.Replace(item_name, @"\d+", match =>
                {
                    int heal_amount = int.Parse(match.Value);
                    heal_amount = (int)Math.Ceiling(heal_amount * Configuration.HealAmountMultiplier.Value);

                    return heal_amount.ToString();
                });

                health_packs[index].itemName = result;
            }

            return true;
        }
    }
}
