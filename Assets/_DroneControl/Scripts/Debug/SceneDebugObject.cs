using UnityEngine;

public class SceneDebugObject : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject);
    }
}
