using System.Collections;
using _DroneControl.Scripts;
using DG.Tweening;
using Game.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace _DroneControl.GameFlow
{
    public class EndGameHandler : MonoBehaviour
    {
        [SerializeField] Image fade;
        
        private void Awake()
        {
            EventManager.EndGame += EndGameHandle;
        }

        private void OnDestroy()
        {
            EventManager.EndGame -= EndGameHandle;
        }

        private void EndGameHandle(bool flag)
        {
            StartCoroutine(EndGameHandleCoroutine(flag));
        }

        private IEnumerator EndGameHandleCoroutine(bool flag)
        {
            yield return fade.DOFade(1f, 1.5f).SetEase(Ease.InQuart).WaitForCompletion();

            StartCoroutine(flag
                ? SceneLoader.LoadScene(SceneName.WinEnd)
                : SceneLoader.LoadScene(SceneName.LoseEnd));
        }
    }
}