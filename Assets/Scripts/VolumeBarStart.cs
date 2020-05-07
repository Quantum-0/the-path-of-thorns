using UnityEngine;
using UnityEngine.UI;

public class VolumeBarStart : MonoBehaviour
{
    // Записываю значение из PlayerPrefs (volume)
    void Start()
    {
        if(PlayerPrefs.GetFloat("volume") != null)
        {
            GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("volume");
        }
    }
}
