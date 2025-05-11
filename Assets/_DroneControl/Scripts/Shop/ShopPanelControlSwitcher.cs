using _DroneControl.Audio;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _DroneControl.Scripts.Shop
{
    public class ShopPanelControlSwitcher : MonoBehaviour
    {
        public static bool isBlocked = true;

        private InputAction _closePanelAction;
        private InputAction _clickAction;
        
        
        private void Awake()
        {
            EventManager.ActivateShopControl += Unblock;
            EventManager.DeactivateShopControl += Block;
        }

        private void Start()
        {
            _closePanelAction = InputSystem.actions.FindAction("ClosePanel");
            _clickAction = InputSystem.actions.FindAction("LeftClick");
            _clickAction.performed += ClickHandle;
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

        private void ClickHandle(InputAction.CallbackContext context)
        {
            if(!isBlocked)
                AudioStorage.PlayGlobalSfx("shopClick");
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
            _clickAction.performed -= ClickHandle;
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