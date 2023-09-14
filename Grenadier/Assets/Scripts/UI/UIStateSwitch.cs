using System;
using UnityEngine;

public class UIStateSwitch : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject settingsMenu;
    public event Action OnPauseGame;
    public event Action OnContinueGame;
    private bool _menuOpened;
    private bool _startPanelOpened;

    public void StartBehaviour()
    {
        _startPanelOpened = true;
        settingsMenu.SetActive(false);
        startPanel.SetActive(true);
        EnableCursor();
    }

    public void CloseStartPanel()
    {
        _startPanelOpened = false;
        startPanel.SetActive(false);
        DisableCursor();
        OnContinueGame?.Invoke();
    }

    private void OpenMenu()
    {
        settingsMenu.SetActive(true);
        _menuOpened = true;
        EnableCursor();
        OnPauseGame?.Invoke();
    }

    public void CloseMenu()
    {
        settingsMenu.SetActive(false);
        _menuOpened = false;
        DisableCursor();
        OnContinueGame?.Invoke();
    }

    private void Update()
    {
        if (_startPanelOpened)
        {
            return;
        }

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