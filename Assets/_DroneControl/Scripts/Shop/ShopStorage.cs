using System;
using System.Collections.Generic;
using _DroneControl.Scripts.Battery;
using _DroneControl.Scripts.Shop.SellBucket;
using Unity.VisualScripting;
using UnityEngine;

namespace _DroneControl.Scripts.Shop
{
    public class ShopStorage : MonoBehaviour
    {
        private const int MAX_ITEMS = 6;
        public static int Balance = 50000;
        [SerializeField] public ItemInfo[] itemSlots = new ItemInfo[MAX_ITEMS];

        public static ShopStorage Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static bool TryBuy(int slotNumber)
        {
            var itemToBuy = Instance.itemSlots[slotNumber];
            if (itemToBuy.itemCost <= Balance)
            {
                if (!itemToBuy.isItemRebuyable && itemToBuy.isBought)
                {
                    EventManager.InvokeNotEnoughMoney();
                    return false;
                }

                Balance -= itemToBuy.itemCost;
                switch (itemToBuy.itemType)
                {
                    case Item.Battery:
                        if(BatteryStorage.currentAmount < BatteryStorage.currentCapacity)
                            EventManager.InvokeBatteryBrought();
                        else 
                            EventManager.InvokeMaxCapacityWhenBuy();
                        break;
                    case Item.Repair:
                        EventManager.InvokeRepairBrought();
                        Debug.Log("Repair brought");
                        break;
                    case Item.Scanner:
                        itemToBuy.isBought = true;
                        EventManager.InvokeScannerBrought();
                        Debug.Log("Scanner brought");
                        break;
                    case Item.BatteryUpgrade:
                        itemToBuy.isBought = true;
                        EventManager.InvokeBatteryUpgradeBrought();
                        Debug.Log("Battery upgrade brought");
                        break;
                    case Item.ChanceUpgrade:
                        itemToBuy.isBought = true;
                        EventManager.InvokeLuckUpgradeBrought();
                        Debug.Log("Luck upgrade brought");
                        break;
                    case Item.LifeUpgrade:
                        itemToBuy.isBought = true;
                        EventManager.InvokeLifeUpgradeBrought();
                        Debug.Log("Life upgrade brought");
                        break;
                }

                return true;
            }

            return false;
        }

        public static string GetString(Item item)
        {
            switch (item)
            {
                case Item.Battery:
                    return "Terminal Battery";
                case Item.Repair:
                    return "Repair";
                case Item.Scanner:
                    return "Scanner";
                case Item.BatteryUpgrade:
                    return "Battery Capacity";
                case Item.LifeUpgrade:
                    return "Ship Durability";
                case Item.ChanceUpgrade:
                    return "Luck Upgrade";
            }

            return "unknown";
        }
    }

    [Serializable]
    public class ItemInfo
    {
        [SerializeField] public Item itemType;
        [Range(0, 10000)] [SerializeField] public int itemCost;

        [SerializeField] public bool isItemRebuyable;
        [SerializeField] public bool isBought;
    }

    public enum Item
    {
        Battery,
        Repair,
        Scanner,
        BatteryUpgrade,
        LifeUpgrade,
        ChanceUpgrade
    }
}