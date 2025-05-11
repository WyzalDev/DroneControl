using _DroneControl.Scripts;
using _DroneControl.Scripts.Shop;
using _DroneControl.Scripts.Shop.SellBucket;
using _DroneControl.TerminalPanel.Minigame;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _DroneControl.Player
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform InteractorSource;
        [SerializeField] public float interactDistance;
        [SerializeField] public TMP_Text tooltip;
        [SerializeField] public Image itemGrabTooltip;
        [SerializeField] public PlayerPickUpDrop PlayerPickUpDrop;

        public static bool isBlocked { get; private set; }

        private InputAction _interactAction;

        private void Awake()
        {
            EventManager.ActivatePlayerControl += Unblock;
            EventManager.DeactivatePlayerControl += Block;
        }

        private void Start()
        {
            _interactAction = InputSystem.actions.FindAction("Interact");
        }

        private void Update()
        {
            if (!isBlocked)
            {
                var r = new Ray(InteractorSource.position, InteractorSource.forward);
                if (Physics.Raycast(r, out var hit, interactDistance))
                {
                    if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
                    {
                        if (interactable is InteractableShop interactableShop && !GridManager.isDocked)
                        {
                            tooltip.text = "";
                            itemGrabTooltip.gameObject.SetActive(false);
                            return;
                        }

                        tooltip.text = interactable.TooltipMessage;
                        if (_interactAction.IsPressed())
                        {
                            interactable.Interact();
                        }
                    }
                    else if (hit.collider.gameObject.TryGetComponent(out PhysicItemMono item))
                    {
                        if(PlayerPickUpDrop._item == null)
                            itemGrabTooltip.gameObject.SetActive(true);
                        else
                            itemGrabTooltip.gameObject.SetActive(false);
                    }
                    else
                    {
                        itemGrabTooltip.gameObject.SetActive(false);
                        tooltip.text = "";
                    }
                }
                else
                {
                    tooltip.text = "";
                    itemGrabTooltip.gameObject.SetActive(false);
                }
            }
            else
            {
                tooltip.text = "";
                itemGrabTooltip.gameObject.SetActive(false);
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