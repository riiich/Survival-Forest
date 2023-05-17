using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // when the play button is pressed, it will go to the next scene in the "File >> Build Settings..." 
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }

    public void About()
    {
        Debug.Log("About Section!");
        SceneManager.LoadScene("AboutSection");
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
