using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnWorldScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Button btn;

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("heeeere");
        Text text = btn.GetComponentInChildren<Text>(true);
        if (text != null)
        {
            text.fontSize = 38;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("laaaaa");
        Text text = btn.GetComponentInChildren<Text>(true);
        if (text != null)
        {
            text.fontSize = 28;
        }
    }

    public void LoadLevel(BaseEventData eventData)
    {
        Debug.Log("iciiiii");
        Debug.Log(btn.tag);
        switch (btn.tag)
        {
            case "world1":
                SceneManager.LoadScene("LevelWorld1");
                break;
            case "world2":
                SceneManager.LoadScene("");
                break;
            case "back":
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
