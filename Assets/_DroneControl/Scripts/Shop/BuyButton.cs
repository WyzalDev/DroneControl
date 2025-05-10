using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _DroneControl.Scripts.Shop
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : MonoBehaviour
    {
        [Range(1,6)]
        [SerializeField] private int slotNumber;
        [SerializeField] private Item item;

        [SerializeField] private TMP_Text text;
        
        private Button _button;
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            text.text = ShopStorage.Instance.itemSlots[slotNumber-1].itemCost.ToString();
        }
        
        public void Buy()
        {
            Debug.Log($"slotNumber = {slotNumber}");
            if(ShopStorage.TryBuy(slotNumber-1))
            {
                if (item is Item.Scanner or Item.BatteryUpgrade or Item.LifeUpgrade or Item.ChanceUpgrade)
                {
                    _button.interactable = false;
                }
            }
        }
    }
}