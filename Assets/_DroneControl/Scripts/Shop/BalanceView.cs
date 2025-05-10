using System;
using TMPro;
using UnityEngine;

namespace _DroneControl.Scripts.Shop
{
    public class BalanceView : MonoBehaviour
    {
        public TMP_Text balanceText;

        private void Awake()
        {
            EventManager.SelledAllItems += BalanceUpdate;
            EventManager.BatteryBrought += BalanceUpdate;
            EventManager.RepairBrought += BalanceUpdate;
            EventManager.ScannerBrought += BalanceUpdate;
            EventManager.BatteryUpgradeBrought += BalanceUpdate;
            EventManager.LifeUpgradeBrought += BalanceUpdate;
            EventManager.LuckUpgradeBrought += BalanceUpdate;
            BalanceUpdate();
        }

        private void OnDestroy()
        {
            EventManager.SelledAllItems -= BalanceUpdate;
            EventManager.BatteryBrought -= BalanceUpdate;
            EventManager.RepairBrought -= BalanceUpdate;
            EventManager.ScannerBrought -= BalanceUpdate;
            EventManager.BatteryUpgradeBrought -= BalanceUpdate;
            EventManager.LifeUpgradeBrought -= BalanceUpdate;
            EventManager.LuckUpgradeBrought -= BalanceUpdate;
        }

        private void BalanceUpdate()
        {
            balanceText.text = ShopStorage.Balance.ToString();
        }
    }
}