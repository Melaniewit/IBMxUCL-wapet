using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadOption1Choose : MonoBehaviour
{
    public void LoadOption1Scene()
    {
        SceneManager.LoadScene("Option1-choose your pet");
    }
}
