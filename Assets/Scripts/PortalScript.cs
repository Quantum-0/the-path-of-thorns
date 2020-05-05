﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("endGame", .5f);
    }

    void endGame()
    {
        SceneManager.LoadScene(0);
    }
}
