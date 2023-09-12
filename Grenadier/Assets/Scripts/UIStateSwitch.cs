using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateSwitch : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    private bool _menuOpened;

    private void OpenMenu()
    {
        settingsMenu.SetActive(true);
        _menuOpened = true;
        EnableCursor();

    }

    public void CloseMenu()
    {
        settingsMenu.SetActive(false);
        _menuOpened = false;
        DisableCursor();

    }

    private void Start()
    {
        CloseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menuOpened)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
