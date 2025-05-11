using System.Collections;
using _DroneControl.Audio;
using UnityEngine;

namespace _DroneControl.GameFlow
{
    public class BootstrapLose : MonoBehaviour
    {
        [SerializeField] private float delay = 6f;
        
        private void Start()
        {
            StartCoroutine(LoseSFX());
        }

        private IEnumerator LoseSFX()
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                AudioStorage.PlayGlobalSfx("lose");
            }
        }
    }
}