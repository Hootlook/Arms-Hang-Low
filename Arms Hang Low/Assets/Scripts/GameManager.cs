using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EventSystem eventSystem;
    public GameObject menuContainer;
    public GameObject menu;
    public GameObject menuContinue;
    public GameObject menuResume;
    public Text menuLabel;
    public bool menuPrevState;
    public bool levelEnd;
    public bool inLevel;
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
        if (!levelEnd && inLevel)
        {
            if (Input.GetButtonDown("Start"))
            {
                menu.SetActive(!menu.activeSelf);
            }

            if (menuPrevState == !menu.activeSelf)
            {
                eventSystem.SetSelectedGameObject(GetFirstActive(menuContainer));
            }
        }

        menuPrevState = menu.activeSelf;
    }

    public void LoadNextLevel(bool restart)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + (restart ? 0 : 1));
        levelEnd = false;
        win = false;
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        levelEnd = false;
        inLevel = false;
        win = false;
    }

    public void Resume()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void EndLevel(bool hasWon)
    {
        levelEnd = true;

        if (hasWon)
        {
            win = true;
        }
        menuLabel.text = win ? "Bravo !": "Aww to bad :(";
        menuResume.SetActive(false);
        menuContinue.SetActive(win);
        menu.SetActive(true);
        eventSystem.SetSelectedGameObject(GetFirstActive(menuContainer));
    }

    GameObject GetFirstActive(GameObject objectRoot)
    {
        for (int i = 0; i < objectRoot.transform.childCount; i++)
        {
            if (objectRoot.transform.GetChild(i).gameObject.activeSelf == true)
            {
                return objectRoot.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }
    private void OnLevelWasLoaded(int level)
    {
        menuLabel.text = "MENU";
        menuResume.SetActive(true);
        menuContinue.SetActive(win);
        menu.SetActive(false);

        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            inLevel = true;
        }
    }
}
