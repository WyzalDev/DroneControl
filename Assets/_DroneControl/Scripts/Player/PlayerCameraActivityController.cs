using _DroneControl.Scripts;
using Unity.Cinemachine;
using UnityEngine;

namespace _DroneControl.Player
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class PlayerCameraActivityController : MonoBehaviour
    {
        public static bool isTurned { get; private set; }
        
        private CinemachineCamera _cinemachineCamera;
        private void Awake()
        {
            _cinemachineCamera = GetComponent<CinemachineCamera>();
            EventManager.ActivatePlayerControl += Unblock;
            EventManager.DeactivatePlayerControl += Block;
        }
        
        private void OnDestroy()
        {
            EventManager.ActivatePlayerControl -= Unblock;
            EventManager.DeactivatePlayerControl -= Block;
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