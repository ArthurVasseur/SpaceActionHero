//
// © Arthur Vasseur arthurvasseur.fr
//
using UnityEngine;
using UnityEngine.UI;

public class ApplicationInfo : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Text>().text = "Beta : " + Application.version;
    }
}
