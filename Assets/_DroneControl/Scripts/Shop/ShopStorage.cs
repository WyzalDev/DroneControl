using System;
using System.Collections.Generic;
using _DroneControl.Scripts.Shop.SellBucket;
using UnityEngine;

namespace _DroneControl.Scripts.Shop
{
    public class ShopStorage : MonoBehaviour
    {
        private const int MAX_ITEMS = 6;
        public static int Balance = 50000;
        [SerializeField] public ItemInfo[] itemSlots = new ItemInfo[MAX_ITEMS];
        [SerializeField] public ShopInfoHandler _ShopInfoHandler;

        [Header("Balance shop pricing")] [SerializeField]
        private int batteryCost;

        [SerializeField] public int repairCost;
        [SerializeField] public int scannerCost;
        [SerializeField] public int batteryUpgradeCost;
        [SerializeField] public int luckUpgradeCost;
        [SerializeField] public int lifeUpgradeCost;

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

        private void SellAllItems(List<PhysicItem> items)
        {
            foreach (var item in items)
            {
                switch (item.item)
                {
                    case Item.Battery:
                        AddToBalance(batteryCost);
                        break;
                    case Item.Repair:
                        AddToBalance(repairCost);
                        break;
                    case Item.Scanner:
                        AddToBalance(scannerCost);
                        break;
                    case Item.BatteryUpgrade:
                        AddToBalance(batteryUpgradeCost);
                        break;
                    case Item.ChanceUpgrade:
                        AddToBalance(luckUpgradeCost);
                        break;
                    case Item.LifeUpgrade:
                        AddToBalance(lifeUpgradeCost);
                        break;
                }
            }
        }

        private void AddToBalance(int sum)
        {
            if (sum <= 0) return;
            Balance += sum;
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
                        EventManager.InvokeBatteryBrought();
                        Debug.Log("Battery brought");
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