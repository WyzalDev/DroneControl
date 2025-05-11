using System;
using _DroneControl.Scripts;
using UnityEngine;

namespace _DroneControl.Audio
{
    public class SfxHandler : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.ActivatePlayerControl += MonitorOffSound;
            EventManager.DeactivatePlayerControl += MonitorOnSound;
            EventManager.BatteryBrought += BatterySlot;
            EventManager.OneBatteryUsed += BatteryUnslot;
            EventManager.OneHealthLost += ShipHit;
            EventManager.CollectedItemTrue += LootFind;
            EventManager.NotEnoughMoney += NoMoney;
            EventManager.UndockedFalse += CantDoThat;
            EventManager.CollectedItemFalse += CantDoThat;
            EventManager.MaxBatteryCapacityWhenBuy += CantDoThat;
            EventManager.MaxHealthCapacityWhenBuy += CantDoThat;
            EventManager.NoItemsInBucket += CantDoThat;
            
            //buy item
            EventManager.BatteryBrought += BuyItem;
            EventManager.RepairBrought += BuyItem;
            EventManager.ScannerBrought += BuyItem;
            EventManager.BatteryUpgradeBrought += BuyItem;
            EventManager.LifeUpgradeBrought += BuyItem;
            EventManager.LuckUpgradeBrought += BuyItem;
        }
        
        private void OnDestroy()
        {
            EventManager.ActivatePlayerControl -= MonitorOffSound;
            EventManager.DeactivatePlayerControl -= MonitorOnSound;
            EventManager.BatteryBrought -= BatterySlot;
            EventManager.OneBatteryUsed -= BatteryUnslot;
            EventManager.OneHealthLost -= ShipHit;
            EventManager.CollectedItemTrue -= LootFind;
            EventManager.NotEnoughMoney -= NoMoney;
            EventManager.UndockedFalse -= CantDoThat;
            EventManager.CollectedItemFalse -= CantDoThat;
            EventManager.MaxBatteryCapacityWhenBuy -= CantDoThat;
            EventManager.MaxHealthCapacityWhenBuy -= CantDoThat;
            EventManager.NoItemsInBucket -= CantDoThat;
            
            //buy item
            EventManager.BatteryBrought -= BuyItem;
            EventManager.RepairBrought -= BuyItem;
            EventManager.ScannerBrought -= BuyItem;
            EventManager.BatteryUpgradeBrought -= BuyItem;
            EventManager.LifeUpgradeBrought -= BuyItem;
            EventManager.LuckUpgradeBrought -= BuyItem;
        }

        private void MonitorOnSound()
        {
            AudioStorage.PlayGlobalSfx("monitorOn");
        }
        
        private void MonitorOffSound()
        {
            AudioStorage.PlayGlobalSfx("monitorOff");
        }

        private void BatterySlot()
        {
            AudioStorage.PlayGlobalSfx("batterySlot");
        }

        private void BatteryUnslot()
        {
            AudioStorage.PlayGlobalSfx("batteryUnslot");
        }

        private void ShipHit()
        {
            AudioStorage.PlayGlobalSfx("shipHit");
        }

        private void LootFind()
        {
            AudioStorage.PlayGlobalSfx("lootFind");
        }

        private void NoMoney()
        {
            AudioStorage.PlayGlobalSfx("noMoney");
        }

        private void CantDoThat()
        {
            AudioStorage.PlayGlobalSfx("cantDoThat");
        }

        private void BuyItem()
        {
            AudioStorage.PlayGlobalSfx("buyItem");
        }
    }
}