﻿using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace DropUpgradesOnDestroy
{
    [HarmonyPatch(typeof(Vehicle), "OnKill")]
    public class Vehicle_OnKill
    {
        [HarmonyPrefix]
        public static void Prefix(Vehicle __instance)
        {
            Dictionary<string, InventoryItem> eq = AccessTools.Field(typeof(Equipment), "equipment").GetValue(__instance.modules) as Dictionary<string, InventoryItem>;
            List<InventoryItem> equipment = eq?.Values?.Where((e) => e != null).ToList() ?? new List<InventoryItem>();
            foreach (InventoryItem item in equipment)
            {
                GameObject gameObject = CraftData.InstantiateFromPrefab(item.item.GetTechType());
                Vector3 position = __instance.gameObject.transform.position;
                gameObject.transform.position = new Vector3(position.x + UnityEngine.Random.Range(-3, 3), position.y + UnityEngine.Random.Range(5, 8), position.z + UnityEngine.Random.Range(-3, 3));
                gameObject.SetActive(true);
            }
        }
    }

}