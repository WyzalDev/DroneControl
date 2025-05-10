using _DroneControl.Scripts;
using Unity.Cinemachine;
using UnityEngine;

namespace _DroneControl.Camera
{
    public class ShopPanelCameraActivityController : MonoBehaviour
    {
        public static bool isTurned { get; private set; }
        
        private CinemachineCamera _cinemachineCamera;
        private void Awake()
        {
            _cinemachineCamera = GetComponent<CinemachineCamera>();
            _cinemachineCamera.gameObject.SetActive(false);
            EventManager.ActivateShopControl += Unblock;
            EventManager.DeactivateShopControl += Block;
        }
        
        private void OnDestroy()
        {
            EventManager.ActivateShopControl -= Unblock;
            EventManager.DeactivateShopControl -= Block;
        }

        private void Block()
        {
            _cinemachineCamera.gameObject.SetActive(false);
            isTurned = false;
        }

        private void Unblock()
        {
            _cinemachineCamera.gameObject.SetActive(true);
            isTurned = true;
        }
    }
}