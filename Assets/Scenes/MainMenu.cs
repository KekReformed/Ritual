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
        Application.OpenURL("https://forms.gle/HQt6NJHT6BurPdQTA");
    }
}
