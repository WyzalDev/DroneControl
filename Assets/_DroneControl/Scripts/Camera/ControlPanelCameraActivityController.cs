using _DroneControl.Scripts;
using Unity.Cinemachine;
using UnityEngine;

namespace _DroneControl.Camera
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class ControlPanelCameraActivityController : MonoBehaviour
    {
        public static bool isTurned { get; private set; }
        
        private CinemachineCamera _cinemachineCamera;
        private void Awake()
        {
            _cinemachineCamera = GetComponent<CinemachineCamera>();
            _cinemachineCamera.gameObject.SetActive(false);
            EventManager.ActivatePanelControl += Unblock;
            EventManager.DeactivatePanelControl += Block;
        }
        
        private void OnDestroy()
        {
            EventManager.ActivatePanelControl -= Unblock;
            EventManager.DeactivatePanelControl -= Block;
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