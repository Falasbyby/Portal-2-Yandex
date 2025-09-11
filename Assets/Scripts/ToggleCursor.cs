using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCursor : MonoBehaviour
{
    private bool isCursorVisible = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCursorVisibility();
        }
    }

    private void ToggleCursorVisibility()
    {
        isCursorVisible = !isCursorVisible;
        Cursor.visible = isCursorVisible;
        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
