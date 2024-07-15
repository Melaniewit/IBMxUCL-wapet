using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    public GameObject panelToHide; // Attach the popup panel here in the inspector

    public void ClosePanel()
    {
        Debug.Log("Attempting to close panel...");
        if (panelToHide != null)
        {
            Debug.Log("Panel is currently active: " + panelToHide.activeSelf);
            panelToHide.SetActive(false);
            Debug.Log("Panel should now be inactive: " + panelToHide.activeSelf);
        }
        else
            Debug.LogError("Panel to hide not set.");
    }

}


