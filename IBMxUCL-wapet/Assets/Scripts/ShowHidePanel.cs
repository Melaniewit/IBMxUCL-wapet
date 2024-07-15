using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ShowHidePanel : MonoBehaviour
{
    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}


