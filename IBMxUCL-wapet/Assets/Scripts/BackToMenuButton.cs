using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour
{
    // This function is called when the button is clicked
    public void OnBackToMenuButtonClicked()
    {
        // Load the scene named "Menu"
        SceneManager.LoadScene("Menu page");
    }
}
