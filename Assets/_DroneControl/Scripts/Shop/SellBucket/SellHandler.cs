using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _DroneControl.Scripts.Shop.SellBucket
{
    public class SellHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text result;
        [SerializeField] private ShopInfoHandler shopInfoHandler;

        private int _summ = 0;

        private void Awake()
        {
            EventManager.SellAllItems += SellAllItems;
        }

        private void OnDestroy()
        {
            EventManager.SellAllItems -= SellAllItems;
        }

        private void SellAllItems(List<PhysicItem> items)
        {
            _summ = 0;

            if (items.Count == 0)
            {
                EventManager.InvokeNoItemsInBucket();
                return;
            }

            shopInfoHandler.Clear();

            foreach (var item in items)
            {
                AddToBalance(item.cost);
                shopInfoHandler.AddLine($"{item.name} - {item.cost}");
            }

            shopInfoHandler.Submit();
            result.text = _summ.ToString();
            EventManager.InvokeSelledAllItems();
        }

        private void AddToBalance(int sum)
        {
            if (sum <= 0) return;
            ShopStorage.Balance += sum;
            _summ += sum;
        }
    }

    public enum PhysicItemTypes
    {
        Diamond,
        Propeller,
        MetalPipe,
        Ruby,
        Sapphire,
        ScrapPile,
        Rock1,
        Rock2,
        Rock3,
    }
}