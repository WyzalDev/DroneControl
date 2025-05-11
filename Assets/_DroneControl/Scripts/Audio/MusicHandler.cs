using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _DroneControl.Audio
{
    public class MusicHandler : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return AudioStorage.PlayGlobalMusic("background", AudioStorage.fadeSettings).WaitForCompletion();
        }
    }
}