using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _DroneControl.Scripts.Sparkles
{
    public class CameraAnimation : MonoBehaviour
    {
        private int oneRotateTime = 6;

        private WaitForSeconds cached = new WaitForSeconds(5);

        private void Start()
        {
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            while (true)
            {
                yield return transform.DOLocalRotate(new Vector3(0, 50 , 0), oneRotateTime).SetEase(Ease.Linear)
                    .WaitForCompletion();
                yield return cached;
                yield return transform.DOLocalRotate(new Vector3(0, -50, 0), oneRotateTime).SetEase(Ease.Linear)
                    .WaitForCompletion();
                yield return cached;
            }
        }
    }
}