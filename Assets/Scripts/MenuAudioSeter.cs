using UnityEngine;

public class MenuAudioSeter : MonoBehaviour
{
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = PlayerPrefs.GetFloat("volume");
    }
}
