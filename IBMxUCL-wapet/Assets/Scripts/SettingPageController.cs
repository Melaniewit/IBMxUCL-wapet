using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Core; // Ensure this is included

public class SettingPageController : MonoBehaviour
{
    [SerializeField] private Button signOutButton;

    private void Start()
    {
        if (signOutButton != null)
        {
            signOutButton.onClick.AddListener(SignOutButtonPressed);
        }
        else
        {
            Debug.LogError("SignOutButton is not assigned.");
        }
    }

    private void SignOutButtonPressed()
    {
        Debug.Log("SignOut button pressed.");
        try
        {
            AuthenticationService.Instance.SignOut(true);
            Debug.Log("SignOut is successful.");
            // Add any additional logic needed after sign out
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"SignOut failed: {ex.Message}");
        }
    }
}
