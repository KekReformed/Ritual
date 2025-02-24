using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void loadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void feedbackLink()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLScnBVXONhI0TMRlSM8X93e1X44rf1I1IBHEZ191X4PMsSMddg/viewform");
    }
}
