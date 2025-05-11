using UnityEngine;

namespace _DroneControl.Scripts.Health
{
    public class LifeLostHandler : MonoBehaviour
    {
        [SerializeField] private HealthStorage healthStorage;

        private void Awake()
        {
            EventManager.DockedFalse += LostOneLife;
            EventManager.MoveWhenDontUndock += LostOneLife;
            EventManager.DockWhenDocked += LostOneLife;
            EventManager.MoveFailedGetDamage += LostOneLife;
        }

        private void LostOneLife()
        {
            healthStorage.OneLifeLost();
            EventManager.InvokeOneHealthLost();
        }

        private void OnDestroy()
        {
            EventManager.DockedFalse -= LostOneLife;
            EventManager.MoveWhenDontUndock -= LostOneLife;
            EventManager.DockWhenDocked -= LostOneLife;
            EventManager.MoveFailedGetDamage -= LostOneLife;
        }
    }
}