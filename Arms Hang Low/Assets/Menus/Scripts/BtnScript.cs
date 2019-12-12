using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Button btn;

    public void OnSelect(BaseEventData eventData)
    {
        btn.image.rectTransform.sizeDelta = new Vector2(240, 130);
        Text text = btn.GetComponentInChildren<Text>(true);
        if (text != null)
        {
            text.fontSize = 60;
            text.fontStyle = FontStyle.Bold;
        }
        ParticleSystem particule = btn.GetComponentInChildren<ParticleSystem>(true);
        particule.Play();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        btn.image.rectTransform.sizeDelta = new Vector2(190, 80);
        Text text = btn.GetComponentInChildren<Text>(true);
        if (text != null)
        {
            text.fontSize = 40;
            text.fontStyle = FontStyle.Normal;
        }
        ParticleSystem particule = btn.GetComponentInChildren<ParticleSystem>(true);
        particule.Stop();
    }

    public void LoadLevel(BaseEventData eventData)
    {
        Debug.Log(btn.tag);
        switch(btn.tag)
        {
            case "play":
                SceneManager.LoadScene("Level1");
                break;
            case "level":
                SceneManager.LoadScene("LevelsList");
                break;
            case "credit":
                SceneManager.LoadScene("Credit");
                break;
            case "quit":
                Application.Quit();
                break;
        }
    }
}
