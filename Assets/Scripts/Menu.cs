using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            PlayerPrefs.SetInt("sound", 1);
        }
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SoundSettings()
    {
        PlayerPrefs.SetInt("sound", -PlayerPrefs.GetInt("sound"));

#warning TODO: Отобразить полосу изменения громкости
    }
}
