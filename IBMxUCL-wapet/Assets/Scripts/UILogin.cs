using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILogin : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signOutButton; // Reference to SignOutButton on userPanel

    [SerializeField] private GameObject loginPanelPrefab; // Reference to LoginPanel prefab
    [SerializeField] private GameObject userPanelPrefab;  // Reference to UserPanel prefab
    [SerializeField] private GameObject popupPrefab; // Reference to Popup prefab

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
        loginButton.onClick.AddListener(LoginButtonPressed);

        if (loginController != null)
        {
            loginController.OnSignedIn += LoginController_OnSignedIn;
            loginController.OnAvatarUpdate += LoginController_OnAvatarUpdate;
            loginController.OnSignedOut += LoginController_OnSignedOut;
        }

        if (signOutButton != null)
        {
            signOutButton.onClick.AddListener(SignOutButtonPressed);
        }
        else
        {
            Debug.LogError("SignOutButton is not assigned.");
        }
    }

    private void OnDisable()
    {
        loginButton.onClick.RemoveListener(LoginButtonPressed);

        if (loginController != null)
        {
            loginController.OnSignedIn -= LoginController_OnSignedIn;
            loginController.OnAvatarUpdate -= LoginController_OnAvatarUpdate;
            loginController.OnSignedOut -= LoginController_OnSignedOut;
        }

        if (signOutButton != null)
        {
            signOutButton.onClick.RemoveListener(SignOutButtonPressed);
        }
    }

    private async void LoginButtonPressed()
    {
        if (loginController != null)
        {
            await loginController.InitSignIn();
        }
    }

    private void LoginController_OnSignedIn(PlayerProfile profile)
    {
        playerProfile = profile;
        loginPanel.gameObject.SetActive(false);
        userPanel.gameObject.SetActive(true);

        ShowPopup("Sign in successful!", () => SceneManager.LoadScene("MenuPage"));
    }

    private void LoginController_OnAvatarUpdate(PlayerProfile profile)
    {
        playerProfile = profile;
    }

    private void LoginController_OnSignedOut()
    {
        userPanel.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(true);

        ShowPopup("Sign out successful!", () => SceneManager.LoadScene("LoginPage"));
    }

    private void SignOutButtonPressed()
    {
        if (loginController != null)
        {
            loginController.SignOut();
        }
    }

    private void ShowPopup(string message, Action onClose)
    {
        GameObject popupInstance = Instantiate(popupPrefab, transform);
        Text popupText = popupInstance.GetComponentInChildren<Text>();
        Button closeButton = popupInstance.GetComponentInChildren<Button>();

        popupText.text = message;
        closeButton.onClick.AddListener(() =>
        {
            Destroy(popupInstance);
            onClose.Invoke();
        });
    }
}
