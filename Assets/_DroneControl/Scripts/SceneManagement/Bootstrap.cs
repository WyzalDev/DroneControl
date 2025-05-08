using Game.SceneManagement;
using UnityEngine;

namespace _DroneControl.SceneManagement
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(SceneLoader.LoadScene(SceneName.Game));
        }
    }
}