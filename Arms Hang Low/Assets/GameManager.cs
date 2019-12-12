using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool levelEnd = true;
    public bool win;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("instance already created");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (levelEnd)
        {
            if (win)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    LoadNextLevel(false);
                }
            }

            if (Input.GetButtonDown("Cancel"))
            {
                LoadNextLevel(true);
            }
        }
    }

    private void LoadNextLevel(bool restart)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + (restart ? 0 : 1));
        levelEnd = false;
        win = false;
    }

    private void OnGUI()
    {
        GUI.contentColor = Color.red;
        
        if (levelEnd)
        {
            if (win)
            {
                GUI.Label(new Rect(Screen.width / 4, Screen.height / 1.5f, 200, 100), "Press A to continue");
            }

            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.5f, 200, 100) , "Press B to restart");
        }
    }
}
