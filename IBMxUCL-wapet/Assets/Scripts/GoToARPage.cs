

using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToARPage : MonoBehaviour
{
    // This function is called when the button is clicked
    public void OnBackToMenuButtonClicked()
    {
        // Load the scene named "Menu"
        SceneManager.LoadScene("AR_plane_detection");
    }
}
