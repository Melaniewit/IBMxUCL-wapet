using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Method to load the settings scene
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Setting page");
    }


}
