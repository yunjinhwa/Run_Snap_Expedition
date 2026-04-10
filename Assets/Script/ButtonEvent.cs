using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void OnTitleButtonClicked()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().LoadTitleScene();
        SceneManager.LoadScene("TitleScene");
    }
}
