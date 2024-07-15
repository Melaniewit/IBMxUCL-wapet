using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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

    private PlayerProfile playerProfile;  // Declare the PlayerProfile variable

    private void Start()
    {
        loginPanel = Instantiate(loginPanelPrefab, transform).transform;
        userPanel = Instantiate(userPanelPrefab, transform).transform;
        signInPopUp = Instantiate(signInPopUpPrefab, transform);
        Debug.Log("Instantiated SignIn Pop-Up at position: " + signInPopUp.transform.position);
        signOutPopUp = Instantiate(signOutPopUpPrefab, transform);

        loginPanel.gameObject.SetActive(true);
        userPanel.gameObject.SetActive(false);
        signInPopUp.SetActive(false);
        signOutPopUp.SetActive(false);

        SetupLoginController();
    }

    private void SetupLoginController()
    {
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

    private IEnumerator ShowPopUpAfterDelay(GameObject popUp, float delay)
    {
        yield return new WaitForSeconds(delay);
        popUp.SetActive(true);
        Debug.Log(popUp.name + " should now be visible.");
    }

    private void LoginController_OnSignedIn(PlayerProfile profile)
    {
        playerProfile = profile;
        loginPanel.gameObject.SetActive(false);
        userPanel.gameObject.SetActive(true);
        StartCoroutine(ShowPopUpAfterDelay(signInPopUp, 0.5f));  // Delay to ensure visibility
    }


    private void LoginController_OnSignedOut()
    {
        userPanel.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(true);
        signOutPopUp.SetActive(true);
        Debug.Log("Sign-Out Successful, showing pop-up");
    }

    private void SignOutButtonPressed()
    {
        if (loginController != null)
        {
            loginController.SignOut();
        }
    }


}
