using System;
using UnityEngine;
using UnityEngine.Audio;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _backGroundPanel;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SettingsMenu _settingsMenu;

    public bool IsPaused => Time.timeScale == 0f && _isGameStarted;

    private Menu _currentMenu;

    public Menu MainMenu => _mainMenu;


    private bool _isGameStarted;

    public event Action<int> OnLevelChange;

    private void Awake()
    {
        _isGameStarted = false;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
       
    }

    private void Start()
    {
        OpenMenu(_mainMenu);
    }

    private void OnEnable()
    {
        _mainMenu.OnButtonClicked += OpenSelectedMenu;
        _settingsMenu.OnButtonClicked += OpenSelectedMenu;
    }

    private void OnDisable()
    {
        _mainMenu.OnButtonClicked -= OpenSelectedMenu;
        _settingsMenu.OnButtonClicked -= OpenSelectedMenu;
    }



    public void OpenSelectedMenu(string selectedMenu)
    {
        switch (selectedMenu)
        {
            case ButtonCommands.Commands.CommandButtonSettings:
                OpenMenu(_settingsMenu);
                break;
            case ButtonCommands.Commands.CommandButtonCloseSettingsMenu:
                OpenMenu(_mainMenu);
                break;
            case ButtonCommands.Commands.CommandButtonPlay:
                StartGame();
                break;
            case ButtonCommands.Commands.CommandButtonExit:
                CloseGame();
                break;
        }
    }

    public void OpenMenu(Menu menu)
    {
        if (_currentMenu != null)
            _currentMenu.Close();

        _currentMenu = menu;
        _currentMenu.Open();
    }


    private void CloseGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        _isGameStarted = true;
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _currentMenu.Close();
    }

    public void PauseToMain()
    {
        Time.timeScale = 0f;
        _isGameStarted = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OpenMenu(_settingsMenu);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _isGameStarted = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (_currentMenu != null)
            _currentMenu.Close();
    }
}