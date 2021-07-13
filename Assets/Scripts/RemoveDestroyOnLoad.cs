using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("GameManager"));
    }
}
