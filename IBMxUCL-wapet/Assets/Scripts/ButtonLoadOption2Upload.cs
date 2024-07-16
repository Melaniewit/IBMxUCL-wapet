using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadOption2Upload : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void LoadOption2Scene()
    {
        SceneManager.LoadScene("Option2-upload photo");
    }
}
