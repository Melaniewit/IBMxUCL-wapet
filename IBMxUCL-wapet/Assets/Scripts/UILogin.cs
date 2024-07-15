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
        Debug.Log("Before activating: " + popUp.name + " active state: " + popUp.activeSelf);
        popUp.SetActive(true);
        Debug.Log("After activating: " + popUp.name + " active state: " + popUp.activeSelf);
    }


    private void LoginController_OnSignedIn(PlayerProfile profile)
    {
        loginPanel.gameObject.SetActive(false);
        userPanel.gameObject.SetActive(true);
        Debug.Log("Before activating SignInPopUp: Active State = " + signInPopUp.activeSelf);
        signInPopUp.SetActive(true);
        StartCoroutine(CheckPopUpVisibility());
        Debug.Log("After activating SignInPopUp: Active State = " + signInPopUp.activeSelf);
    }

    private IEnumerator CheckPopUpVisibility()
    {
        yield return null; // Wait for one frame
        Debug.Log("One frame later: SignInPopUp active state: " + signInPopUp.activeSelf);
        yield return new WaitForSeconds(1); // Wait for one second
        Debug.Log("One second later: SignInPopUp active state: " + signInPopUp.activeSelf);
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
