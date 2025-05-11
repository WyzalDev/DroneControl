using System;
using _DroneControl.Audio;
using _DroneControl.Scripts.Shop.SellBucket;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _DroneControl.Player
{
    public class PlayerPickUpDrop : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        [SerializeField] private LayerMask pickupLayerMask;
        [SerializeField] private float pickupDistance = 2f;
        [SerializeField] private Transform holdEndPoint;

        private InputAction holdAction;

        [NonSerialized] public PhysicItemMono _item = null;

        private void Start()
        {
            holdAction = InputSystem.actions.FindAction("Grab");
            holdAction.performed += TryGrab;
        }

        private void OnDestroy()
        {
            holdAction.performed -= TryGrab;
        }

        private void TryGrab(InputAction.CallbackContext context)
        {
            Debug.Log("TryGrab");
            if (_item == null)
            {
                //try to grab
                if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, pickupDistance,
                        pickupLayerMask))
                {
                    if (hit.collider.TryGetComponent(out PhysicItemMono item))
                    {
                        AudioStorage.PlayGlobalSfx("grabItem");
                        _item = item;
                        _item.Grab(holdEndPoint);
                    }
                }
            }
            else
            {
                //drop
                AudioStorage.PlayGlobalSfx("dropItem");
                _item.Drop();
                _item = null;
            }
        }
    }
}