using UnityEngine;

namespace _DroneControl.Scripts.Shop
{
    public class InteractableShop : MonoBehaviour, IInteractable
    {
        [SerializeField] private string tooltip;

        public string TooltipMessage { get; set; }


        private void Start()
        {
            TooltipMessage = tooltip;
        }

        public static bool isBlocked = false;

        private void Awake()
        {
            TooltipMessage = tooltip;
            EventManager.ActivatePlayerControl += Unblock;
            EventManager.DeactivatePlayerControl += Block;
        }

        public void Interact()
        {
            if (!isBlocked)
            {
                EventManager.InvokeDeactivatePlayerControl();
                EventManager.InvokeActivateShopControl();
            }
        }

        private void OnDestroy()
        {
            EventManager.ActivatePlayerControl -= Unblock;
            EventManager.DeactivatePlayerControl -= Block;
        }

        private static void Block()
        {
            isBlocked = true;
        }

        private static void Unblock()
        {
            isBlocked = false;
        }
    }
}