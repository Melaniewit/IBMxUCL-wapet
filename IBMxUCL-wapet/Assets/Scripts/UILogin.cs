using System;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    [SerializeField] private Button loginButton;

    [SerializeField] private GameObject loginPanelPrefab; // Reference to LoginPanel prefab
    [SerializeField] private GameObject userPanelPrefab;  // Reference to panel prefab

    private Transform loginPanel;
    private Transform userPanel;

    [SerializeField] private LoginController loginController;

    private PlayerProfile playerProfile;

    private void Start()
    {
        // Instantiate panels from prefabs
        loginPanel = Instantiate(loginPanelPrefab, transform).transform;
        userPanel = Instantiate(userPanelPrefab, transform).transform;

        // Ensure only login panel is active initially
        loginPanel.gameObject.SetActive(true);
        userPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        loginButton.onClick.AddListener(LoginButtonPressed);
        loginController.OnSignedIn += LoginController_OnSignedIn;
        loginController.OnAvatarUpdate += LoginController_OnAvatarUpdate;
    }

    private void OnDisable()
    {
        loginButton.onClick.RemoveListener(LoginButtonPressed);
        loginController.OnSignedIn -= LoginController_OnSignedIn;
        loginController.OnAvatarUpdate -= LoginController_OnAvatarUpdate;
    }

    private async void LoginButtonPressed()
    {
        await loginController.InitSignIn();
    }

    private void LoginController_OnSignedIn(PlayerProfile profile)
    {
        playerProfile = profile;
        loginPanel.gameObject.SetActive(false);
        userPanel.gameObject.SetActive(true);

        // If you need to update the UI with user information, add your own methods here
    }

    private void LoginController_OnAvatarUpdate(PlayerProfile profile)
    {
        playerProfile = profile;
        // Update avatar or other UI elements if necessary
    }
}
