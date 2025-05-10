using UnityEngine;
using UnityEngine.InputSystem;

namespace _DroneControl.Scripts.Shop
{
    public class ShopPanelControlSwitcher : MonoBehaviour
    {
        public static bool isBlocked = true;

        private InputAction _closePanelAction;

        private void Awake()
        {
            EventManager.ActivateShopControl += Unblock;
            EventManager.DeactivateShopControl += Block;
        }

        private void Start()
        {
            _closePanelAction = InputSystem.actions.FindAction("ClosePanel");
        }

        private void Update()
        {
            if (!isBlocked)
            {
                if (_closePanelAction.IsPressed())
                {
                    Debug.Log("Panel closes");
                    ControlToPlayer();
                }
            }
        }
        
        public void ControlToPlayer()
        {
            EventManager.InvokeDeactivateShopControl();
            EventManager.InvokeActivatePlayerControl();
        }

        private void OnDestroy()
        {
            EventManager.ActivateShopControl -= Unblock;
            EventManager.DeactivateShopControl -= Block;
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