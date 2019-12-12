using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnFirstWorld : MonoBehaviour, ISelectHandler, IDeselectHandler
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
            case "lvl1":
                SceneManager.LoadScene("Level1");
                break;
            case "lvl2":
                SceneManager.LoadScene("Level2");
                break;
            case "lvl3":
                SceneManager.LoadScene("Level3");
                break;
            case "lvl4":
                SceneManager.LoadScene("Level4");
                break;
            case "lvl5":
                SceneManager.LoadScene("Level5");
                break;
            case "back":
                SceneManager.LoadScene("LevelsList");
                break;
        }
    }
}
