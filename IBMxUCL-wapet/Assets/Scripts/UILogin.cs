using System;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signOutButton; // Reference to SignOutButton on userPanel

    [SerializeField] private GameObject loginPanelPrefab; // Reference to LoginPanel prefab
    [SerializeField] private GameObject userPanelPrefab;  // Reference to userPanel prefab

    private Transform loginPanel;
    private Transform userPanel;

    [SerializeField] private LoginController loginController;

    private PlayerProfile playerProfile;

    private void Start()
    {
        Debug.Log("UILogin Start called.");

        // Instantiate panels from prefabs
        loginPanel = Instantiate(loginPanelPrefab, transform).transform;
        userPanel = Instantiate(userPanelPrefab, transform).transform;

        // Ensure only login panel is active initially
        loginPanel.gameObject.SetActive(true);
        userPanel.gameObject.SetActive(false);

        // Check if loginController is assigned
        if (loginController == null)
        {
            loginController = FindObjectOfType<LoginController>();
            if (loginController == null)
            {
                Debug.LogError("LoginController is not assigned and cannot be found in the scene.");
            }
            else
            {
                Debug.Log("LoginController found and assigned.");
            }
        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        Debug.Log("loginButton: " + (loginButton != null ? "Not null" : "Null"));
        Debug.Log("signOutButton: " + (signOutButton != null ? "Not null" : "Null"));
        Debug.Log("loginController: " + (loginController != null ? "Not null" : "Null"));

        loginButton.onClick.AddListener(LoginButtonPressed);

        if (loginController != null)
        {
            loginController.OnSignedIn += LoginController_OnSignedIn;
            loginController.OnAvatarUpdate += LoginController_OnAvatarUpdate;
            loginController.OnSignedOut += LoginController_OnSignedOut;
        }

        // Add sign out button listener
        if (signOutButton != null)
        {
            Debug.Log("Adding listener to signOutButton.");
            signOutButton.onClick.AddListener(SignOutButtonPressed);
        }
        else
        {
            Debug.LogError("SignOutButton is not assigned.");
        }
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable called");
        Debug.Log("loginButton: " + (loginButton != null ? "Not null" : "Null"));
        Debug.Log("signOutButton: " + (signOutButton != null ? "Not null" : "Null"));
        Debug.Log("loginController: " + (loginController != null ? "Not null" : "Null"));

        loginButton.onClick.RemoveListener(LoginButtonPressed);

        if (loginController != null)
        {
            loginController.OnSignedIn -= LoginController_OnSignedIn;
            loginController.OnAvatarUpdate -= LoginController_OnAvatarUpdate;
            loginController.OnSignedOut -= LoginController_OnSignedOut;
        }

        // Remove sign out button listener
        if (signOutButton != null)
        {
            signOutButton.onClick.RemoveListener(SignOutButtonPressed);
        }
    }

    private async void LoginButtonPressed()
    {
        Debug.Log("LoginButton pressed.");
        if (loginController != null)
        {
            await loginController.InitSignIn();
        }
    }

    private void LoginController_OnSignedIn(PlayerProfile profile)
    {
        Debug.Log("OnSignedIn event received.");
        playerProfile = profile;
        loginPanel.gameObject.SetActive(false);
        userPanel.gameObject.SetActive(true);
    }

    private void LoginController_OnAvatarUpdate(PlayerProfile profile)
    {
        playerProfile = profile;
    }

    private void LoginController_OnSignedOut()
    {
        Debug.Log("OnSignedOut event received.");
        userPanel.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(true);
    }

    public void SignOutButtonPressed()
    {
        Debug.Log("SignOut button pressed.");
        if (loginController != null)
        {
            Debug.Log("Calling SignOut method.");
            loginController.SignOut();
        }
    }
}
