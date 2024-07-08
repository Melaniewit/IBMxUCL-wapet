using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Method to load the settings scene
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Setting page"); // Replace "SettingsScene" with the actual name of your settings scene
    }

    // Method to load the login scene
    public void LoadLoginScene()
    {
        SceneManager.LoadScene("Login page"); // Replace "LoginScene" with the actual name of your login scene
    }
}
