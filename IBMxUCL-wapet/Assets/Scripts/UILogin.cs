using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signOutButton;
    [SerializeField] private GameObject loginPanelPrefab;
    [SerializeField] private GameObject userPanelPrefab;
    [SerializeField] private GameObject signInPopUpPrefab; // Reference to the sign-in pop-up prefab
    [SerializeField] private GameObject signOutPopUpPrefab; // Reference to the sign-out pop-up prefab

    private GameObject signInPopUp;
    private GameObject signOutPopUp;
    private Transform loginPanel;
    private Transform userPanel;

    [SerializeField] private LoginController loginController;

    private void Start()
    {
        loginPanel = Instantiate(loginPanelPrefab, transform).transform;
        userPanel = Instantiate(userPanelPrefab, transform).transform;
        signInPopUp = Instantiate(signInPopUpPrefab, transform);
        signOutPopUp = Instantiate(signOutPopUpPrefab, transform);

        // Initially hide pop-ups
        signInPopUp.SetActive(false);
        signOutPopUp.SetActive(false);

        loginPanel.gameObject.SetActive(true);
        userPanel.gameObject.SetActive(false);

        SetupLoginController();
    }

    private void SetupLoginController()
    {
        // Ensure the loginController is set, or find it if not
        if (loginController == null)
        {
            loginController = FindObjectOfType<LoginController>();
            if (loginController == null)
            {
                Debug.LogError("LoginController not found.");
                return;
            }
        }

        loginController.OnSignedIn += LoginController_OnSignedIn;
        loginController.OnSignedOut += LoginController_OnSignedOut;
    }

    private void OnEnable()
    {
        loginButton.onClick.AddListener(LoginButtonPressed);
        signOutButton.onClick.AddListener(SignOutButtonPressed);
    }

    private void OnDisable()
    {
        loginButton.onClick.RemoveListener(LoginButtonPressed);
        signOutButton.onClick.RemoveListener(SignOutButtonPressed);

        if (loginController != null)
        {
            loginController.OnSignedIn -= LoginController_OnSignedIn;
            loginController.OnSignedOut -= LoginController_OnSignedOut;
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
        loginPanel.gameObject.SetActive(false);
        userPanel.gameObject.SetActive(true);
        Debug.Log("Sign-In Successful, showing pop-up");
        signInPopUp.SetActive(true); // Show sign-in pop-up
    }

    private void LoginController_OnSignedOut()
    {
        userPanel.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(true);
        Debug.Log("Sign-Out Successful, showing pop-up");
        signOutPopUp.SetActive(true); // Show sign-out pop-up
    }

    private void SignOutButtonPressed()
    {
        if (loginController != null)
        {
            loginController.SignOut();
        }
    }
}
