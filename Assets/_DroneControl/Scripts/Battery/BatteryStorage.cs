using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _DroneControl.Scripts.Battery
{
    public class BatteryStorage : MonoBehaviour
    {

        [SerializeField] private int maxCapacityWithoutUpgrade = 4;
        [SerializeField] private int maxCapacityWithUpgrade = 7;
        [SerializeField] private Image batteryAdditionalSlotIcon;
        [SerializeField] private List<Image> batteryIcons;

        public static int currentCapacity;
        public static int currentAmount;
        
        private void Awake()
        {
            EventManager.BatteryUpgradeBrought += UpgradeBrought;
            EventManager.BatteryBrought += ConsumableBrought;
        }

        private void Start()
        {
            currentCapacity = maxCapacityWithoutUpgrade;
            currentAmount = maxCapacityWithoutUpgrade;
            BatteryCountChanged();
        }

        private void OnDestroy()
        {
            EventManager.BatteryUpgradeBrought -= UpgradeBrought;
            EventManager.BatteryBrought -= ConsumableBrought;
        }

        private void ConsumableBrought()
        {
            currentAmount++;
            BatteryCountChanged();
        }

        public void UseOneBattery()
        {
            currentAmount--;
            BatteryCountChanged();
        }

        public void BatteryCountChanged()
        {
            for (int i = 0; i < currentAmount; i++)
            {
                batteryIcons[i].gameObject.SetActive(true);
            }
            if(currentAmount < maxCapacityWithUpgrade)
                for (int i = currentAmount; i < maxCapacityWithUpgrade; i++)
                {
                    batteryIcons[i].gameObject.SetActive(false);
                }
        }
        
        private void UpgradeBrought()
        {
            currentCapacity = maxCapacityWithUpgrade;
            batteryAdditionalSlotIcon.gameObject.SetActive(true);
        }
    }
}