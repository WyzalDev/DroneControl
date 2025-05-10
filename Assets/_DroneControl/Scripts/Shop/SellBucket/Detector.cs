using System.Collections.Generic;
using UnityEngine;

namespace _DroneControl.Scripts.Shop.SellBucket
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask layer;

        private void Awake()
        {
            EventManager.SellItemsInBucket += Detect;
        }

        public void Detect()
        {
            var colliders = Physics.OverlapSphere(transform.position, radius, layer);
            var itemList = new List<PhysicItem>();
            foreach (var colliderItem in colliders)
            {
                if (colliderItem.TryGetComponent<PhysicItemMono>(out var item))
                {
                    itemList.Add(item.itemInfo);
                    Destroy(item.gameObject);
                }
            }

            if (itemList.Count == 0)
            {
                EventManager.InvokeNoItemsInBucket();
            }
            else
            {
                EventManager.InvokeSellAllItems(itemList);
            }
        }

        private void OnDestroy()
        {
            EventManager.SellItemsInBucket -= Detect;
        }
    }
}