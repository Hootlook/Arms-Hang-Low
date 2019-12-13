using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnWorldScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Button btn;

    public void OnSelect(BaseEventData eventData)
    {
        Text text = btn.GetComponentInChildren<Text>(true);
        Debug.Log(text);
        if (text != null)
        {
            text.fontSize = 56;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Text text = btn.GetComponentInChildren<Text>(true);
        Debug.Log(text);
        if (text != null)
        {
            text.fontSize = 38;
        }
    }

    public void LoadLevel(BaseEventData eventData)
    {
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
