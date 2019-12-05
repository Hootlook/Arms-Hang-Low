using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnBack : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetButton("Submit")) {
            Debug.Log("heeeere");
            SceneManager.LoadScene("MainMenu");
        }
      
    }
}
