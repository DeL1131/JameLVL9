using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SettingsMenu _settingsMenu;

    private Menu _currentMenu;

    public Menu MainMenu => _mainMenu;

    private int _easyLevelComplexity = 1;
    private int _mediumLevelComplexity = 2;
    private int _hardLevelComplexity = 3;
    private bool _isGameStarted;

    public event Action<int> OnLevelChange;

    private void Awake()
    {
        EnsureEventSystem();
        _isGameStarted = false;
        //Time.timeScale = 0f;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
       
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

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        _currentMenu.Close();
    }

    public void PauseToMain()
    {
        //Time.timeScale = 0f;
        _isGameStarted = false;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        OpenMenu(_mainMenu);
    }

    private void EnsureEventSystem()
    {
        if (Object.FindFirstObjectByType<EventSystem>() != null)
            return;

        GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        DontDestroyOnLoad(eventSystem);
    }
}
