using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Method to load the settings scene
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Setting page");
    }

    // Method to load the login scene
    public void LoadLoginScene()
    {
        SceneManager.LoadScene("Login page");
    }
}
