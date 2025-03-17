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
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdCDPdkMLEg8x65815IKpQETpNWcZ7BFnSumGk3pCoX_z7FNw/viewform");
    }
}
