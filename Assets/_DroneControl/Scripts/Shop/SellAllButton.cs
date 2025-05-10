using UnityEngine;

namespace _DroneControl.Scripts.Shop
{
    public class SellAllButton : MonoBehaviour
    {
        public void SellAll()
        {
            EventManager.InvokeSellItemsInBucket();
        }
    }
}