using System;
using System.Collections.Generic;
using _DroneControl.Scripts;
using _DroneControl.Scripts.Shop.SellBucket;
using UnityEngine;

namespace _DroneControl.ScrapSource
{
    public class ScrapSource : MonoBehaviour
    {
        private int chanceForCommon = 60;
        private int chanceForUncommon = 30;

        private int increasedChanceForCommon = 35;
        private int increasedChanceForUncommon = 40;

        [SerializeField] private Transform generatePoint;

        [SerializeField] private List<PhysicItemMono> commons;
        [SerializeField] private List<PhysicItemMono> uncommons;
        [SerializeField] private List<PhysicItemMono> rares;

        private void Awake()
        {
            EventManager.CollectedItemTrue += GeneratePhysicItem;
            EventManager.LuckUpgradeBrought += ChanceUpgradeBrought;
        }

        private void OnDestroy()
        {
            EventManager.CollectedItemTrue -= GeneratePhysicItem;
            EventManager.LuckUpgradeBrought -= ChanceUpgradeBrought;
        }

        private void GeneratePhysicItem()
        {
            var randomInt = UnityEngine.Random.Range(0, 100);
            if (randomInt - chanceForCommon <= 0)
            {
                GenerateCommon();
                return;
            }
            else
            {
                randomInt -= chanceForCommon;
            }

            if (randomInt - chanceForUncommon <= 0)
            {
                GenerateUncommon();
            }
            else
            {
                GenerateRare();
            }
        }

        private void GenerateCommon()
        {
            var randomCommon = UnityEngine.Random.Range(0, commons.Count);
            Instantiate(commons[randomCommon], generatePoint.position, Quaternion.identity);
        }

        private void GenerateUncommon()
        {
            var randomUncommon = UnityEngine.Random.Range(0, uncommons.Count);
            Instantiate(uncommons[randomUncommon], generatePoint.position, Quaternion.identity);
        }

        private void GenerateRare()
        {
            var randomRare = UnityEngine.Random.Range(0, rares.Count);
            Instantiate(rares[randomRare], generatePoint.position, Quaternion.identity);
        }

        private void ChanceUpgradeBrought()
        {
            chanceForCommon = increasedChanceForCommon;
            chanceForUncommon = increasedChanceForUncommon;
        }
    }
}