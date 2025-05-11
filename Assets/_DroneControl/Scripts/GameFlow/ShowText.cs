using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _DroneControl.GameFlow
{
    public class ShowText : MonoBehaviour
    {
        [SerializeField] private float delay = 1f;
        [SerializeField] private float titleShowTime = 2f;
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_Text nextText;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);

            StartCoroutine(AnimationScaleText());
            StartCoroutine(AnimationRotationText());
            
            yield return text.DOFade(1f, titleShowTime).SetEase(Ease.InOutCubic).WaitForCompletion();

            yield return new WaitForSeconds(delay);
            
            yield return nextText.DOFade(1f, delay).SetEase(Ease.InOutCubic).WaitForCompletion();
        }

        private IEnumerator AnimationRotationText()
        {
            while (true)
            {
                yield return text.transform.DORotate(new Vector3(0,0,2), titleShowTime).SetEase(Ease.InOutCubic).WaitForCompletion();
                yield return text.transform.DORotate(new Vector3(0,0,-2), titleShowTime).SetEase(Ease.InOutCubic).WaitForCompletion();
            }
        }

        private IEnumerator AnimationScaleText()
        {
            while (true)
            {
                yield return text.transform.DOScale(1.1f, titleShowTime).SetEase(Ease.InOutCubic).WaitForCompletion();
                yield return text.transform.DOScale(1f, titleShowTime).SetEase(Ease.InOutCubic).WaitForCompletion();
            }
        }
    }
}