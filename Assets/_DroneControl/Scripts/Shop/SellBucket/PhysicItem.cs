using UnityEngine;

namespace _DroneControl.Scripts.Shop.SellBucket
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "WyzalUtilities/Item")]
    public class PhysicItem : ScriptableObject
    {
        public PhysicItemTypes item;
        
        public Rarity rarity;

        public int cost;
        
        public string name;

    }

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare
    }
}