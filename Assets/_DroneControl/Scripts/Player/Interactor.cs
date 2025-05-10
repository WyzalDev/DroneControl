using _DroneControl.Scripts;
using _DroneControl.Scripts.Shop;
using _DroneControl.TerminalPanel.Minigame;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _DroneControl.Player
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform InteractorSource;
        [SerializeField] public float interactDistance;
        [SerializeField] public TMP_Text tooltip;

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
                        //TODO Uncomment when shop ready
                        // if (interactable is InteractableShop interactableShop && !GridManager.isDocked)
                        // {
                        //     tooltip.text = "";
                        //     return;
                        // }
                        tooltip.text = interactable.TooltipMessage;
                        if (_interactAction.IsPressed())
                        {
                            interactable.Interact();
                        }
                    }
                    else
                    {
                        tooltip.text = "";
                    }
                }
                else
                {
                    tooltip.text = "";
                }
            }
            else
            {
                tooltip.text = "";
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