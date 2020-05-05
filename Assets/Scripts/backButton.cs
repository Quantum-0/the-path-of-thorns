using UnityEngine;
using UnityEngine.SceneManagement;

public class backButton : MonoBehaviour
{
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
