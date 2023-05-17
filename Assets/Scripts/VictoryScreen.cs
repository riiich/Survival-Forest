using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void Restart() 
    {
        Debug.Log("RESTARTING!");
        SceneManager.LoadScene("Main");
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu!");
        SceneManager.LoadScene("MainMenu");
    }
}
