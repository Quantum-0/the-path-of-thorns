using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    int snd;
    [SerializeField] Button btn;

    private void Start()
    {
        snd = PlayerPrefs.GetInt("sound");

        if(snd == 0)
        {
            PlayerPrefs.SetInt("sound", 1);
        }

        var colors = btn.colors;

        if (snd == 1)
        {
            colors.normalColor = new Color(0, 0, 0, .3411f);
            colors.selectedColor = new Color(0, 0, 0, .3411f);
        }
        else if (snd == -1)
        {
            colors.normalColor = new Color(255, 255, 255, .3411f);
            colors.selectedColor = new Color(255, 255, 255, .3411f);
        }

        btn.colors = colors;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SoundSettings()
    {
        PlayerPrefs.SetInt("sound", -PlayerPrefs.GetInt("sound"));

        Debug.Log(PlayerPrefs.GetInt("sound"));


        var colors = btn.colors;

        if (PlayerPrefs.GetInt("sound") > 0)
        {
            colors.normalColor = new Color(0, 0, 0, .3411f);
            colors.selectedColor = new Color(0, 0, 0, .3411f);
        }
        else if(PlayerPrefs.GetInt("sound") < 0)
        {
            colors.normalColor = new Color(255, 255, 255, .3411f);
            colors.selectedColor = new Color(255, 255, 255, .3411f);
        }

        btn.colors = colors;
    }
}
