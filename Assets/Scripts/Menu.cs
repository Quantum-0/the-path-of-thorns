using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject SoundPanel;

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
        MainPanel.SetActive(false);
        SoundPanel.SetActive(true);
    }
    
    public void BackToMainPanel()
    {
        MainPanel.SetActive(true);
        SoundPanel.SetActive(false);
    }

    public void ChangeVolume(float volume)
    {
        if (volume < .1f)
        {
            PlayerPrefs.SetFloat("volume", 0);
        }
        else if(volume > .9f)
        {
            PlayerPrefs.SetFloat("volume", 1);
        }
        else
        {
            PlayerPrefs.SetFloat("volume", volume);
        }
    }
}
