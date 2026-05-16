using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class SeedRegistry : MonoBehaviour
{
   [SerializeField] private SeedEntity[] seedRegistry;

   public Plant GetPlant(ItemData item)
   {
       foreach (var itemEntity in seedRegistry)
       {
           if (itemEntity.itemData != item)
            continue;
           return itemEntity.prefabs[Random.Range(0, itemEntity.prefabs.Length)];
       }
       return null;
   }
}
[Serializable]
public class SeedEntity
{
    public ItemData itemData;
    public Plant[] prefabs;
}
