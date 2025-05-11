using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _DroneControl.Scripts.Health
{
    public class HealthStorage : MonoBehaviour
    {
        [SerializeField] private int maxCapacityWithoutUpgrade = 3;
        [SerializeField] private int maxCapacityWithUpgrade = 5;
        [SerializeField] private RectTransform healthAdditionalSlots;
        [SerializeField] private List<Image> healthIcons;
        
        public static int currentCapacity;
        public static int currentAmount;
        
        private void Awake()
        {
            EventManager.LifeUpgradeBrought += UpgradeBrought;
            EventManager.RepairBrought += ConsumableBrought;
        }

        private void Start()
        {
            currentCapacity = maxCapacityWithoutUpgrade;
            currentAmount = maxCapacityWithoutUpgrade;
            CountChanged();
        }

        private void OnDestroy()
        {
            EventManager.LifeUpgradeBrought -= UpgradeBrought;
            EventManager.RepairBrought -= ConsumableBrought;
        }

        private void ConsumableBrought()
        {
            currentAmount++;
            CountChanged();
        }

        public void OneLifeLost()
        {
            currentAmount--;
            CountChanged();
        }

        public void CountChanged()
        {
            for (int i = 0; i < currentAmount; i++)
            {
                healthIcons[i].gameObject.SetActive(true);
            }
            if(currentAmount < maxCapacityWithUpgrade)
                for (int i = currentAmount; i < maxCapacityWithUpgrade; i++)
                {
                    healthIcons[i].gameObject.SetActive(false);
                }
        }
        
        private void UpgradeBrought()
        {
            currentCapacity = maxCapacityWithUpgrade;
            healthAdditionalSlots.gameObject.SetActive(false);
        }
    }
}