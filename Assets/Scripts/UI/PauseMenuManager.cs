using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private bool _isPaused = false;

    void Start()
    {
        CloseMenu();
    }

    public void ToggleMenu()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    public void OpenMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
}
