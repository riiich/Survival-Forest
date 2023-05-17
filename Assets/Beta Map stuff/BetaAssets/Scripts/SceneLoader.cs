using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public float loadRange = 150f;
    public Transform player;

    private bool isLoaded;
    private bool shouldLoad;
    // Start is called before the first frame update
    void Start()
    {
        //verify that a scene dosent open twice
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
       //TriggerCheck();

    }

    void DistanceCheck()
    {
        if (player == null)
            return;
        if (Vector3.Distance(player.position, transform.position) < loadRange)
        {
            LoadScene();

        }
        else
        {
            UnLoadScene();
        }
    }

    void TriggerCheck()
    {
        if (shouldLoad)
        {
            LoadScene();

        }
        else
        {
            UnLoadScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = false;
        }
    }


    void LoadScene()
    {
        if (!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    void UnLoadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }
}