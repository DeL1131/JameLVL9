using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuNew : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private bool _startAsGameplayScene;
    [SerializeField] private GameObject _settingsBackground;
    [SerializeField] private GameObject _canvas;

    private Menu _currentMenu;

    private bool _isGameStarted;
    private bool _isPaused;

    public bool IsPaused => _isPaused;

    private void Awake()
    {
        bool shouldStartAsGameplayScene = _startAsGameplayScene || IsGameAlreadyStarted();

        if (shouldStartAsGameplayScene)
        {
            _isGameStarted = true;
            _isPaused = false;

            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (_canvas != null)
                _canvas.SetActive(true);
        }
        else
        {
            _isGameStarted = false;
            _isPaused = false;

            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (_canvas != null)
                _canvas.SetActive(false);
        }
    }

    private void Start()
    {
        if (_isGameStarted)
        {
            CloseMenusForGameplayStart();
            return;
        }

        if (_mainMenu != null)
            OpenMenu(_mainMenu);
    }

    private void Update()
    {
        if (_isGameStarted == false)
            return;

        if (Keyboard.current == null || Keyboard.current.escapeKey.wasPressedThisFrame == false)
            return;

        ToggleSettingsPause();
    }

    private void OnEnable()
    {
        if (_mainMenu != null)
            _mainMenu.OnButtonClicked += HandleButtonClicked;

        if (_settingsMenu != null)
            _settingsMenu.OnButtonClicked += HandleButtonClicked;
    }

    private void OnDisable()
    {
        if (_mainMenu != null)
            _mainMenu.OnButtonClicked -= HandleButtonClicked;

        if (_settingsMenu != null)
            _settingsMenu.OnButtonClicked -= HandleButtonClicked;
    }

    private void HandleButtonClicked(string command)
    {
        switch (command)
        {
            case ButtonCommands.Commands.CommandButtonPlay:
                StartGame();
                break;

            case ButtonCommands.Commands.CommandButtonSettings:
                OpenSettings();
                break;

            case ButtonCommands.Commands.CommandButtonCloseSettingsMenu:
                CloseSettings();
                break;

            case ButtonCommands.Commands.CommandButtonExit:
                QuitGame();
                break;
        }
    }

    public void StartGame()
    {
        if (GameSession.Instance != null)
            GameSession.Instance.MarkGameStarted();

        _isGameStarted = true;
        _isPaused = false;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CloseCurrentMenu();
        CloseMenusForGameplayStart();

        if (_canvas != null)
            _canvas.SetActive(true);
    }

    public void PauseToSettings()
    {
        if (_isGameStarted == false)
            return;

        _isPaused = true;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (_canvas != null)
            _canvas.SetActive(true);

        if (_isGameStarted == false && _settingsBackground != null)
            _settingsBackground.SetActive(true);

        OpenMenu(_settingsMenu);
    }

    public void ResumeGame()
    {
        if (_isGameStarted == false)
            return;

        _isPaused = false;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CloseCurrentMenu();

        if (_settingsBackground != null)
            _settingsBackground.SetActive(false);
    }

    public void ToggleSettingsPause()
    {
        if (_isPaused)
        {
            ResumeGame();
            return;
        }

        PauseToSettings();
    }

    private void OpenSettings()
    {
        // Фон включаем ТОЛЬКО если игра не начата (мы в главном меню)
        if (_isGameStarted == false && _settingsBackground != null)
            _settingsBackground.SetActive(true);

        OpenMenu(_settingsMenu);
    }

    private void CloseSettings()
    {
        if (_isGameStarted == false && _settingsBackground != null)
            StartCoroutine(DisableBackgroundDelayed(1f));

        if (_isGameStarted)
        {
            ResumeGame();
        }
        else
        {
            OpenMenu(_mainMenu);
        }
    }

    private void OpenMenu(Menu menu)
    {
        if (menu == null)
            return;

        if (_currentMenu != null)
            _currentMenu.Close();

        _currentMenu = menu;
        _currentMenu.Open();
    }

    private void CloseCurrentMenu()
    {
        if (_currentMenu == null)
            return;

        _currentMenu.Close();
        _currentMenu = null;
    }

    private void CloseMenusForGameplayStart()
    {
        if (_mainMenu != null)
            _mainMenu.Close();

        if (_settingsMenu != null)
            _settingsMenu.Close();

        if (_settingsBackground != null)
            _settingsBackground.SetActive(false);

        _currentMenu = null;
    }

    private System.Collections.IEnumerator DisableBackgroundDelayed(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (_settingsBackground != null)
            _settingsBackground.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private bool IsGameAlreadyStarted()
    {
        return GameSession.Instance != null && GameSession.Instance.IsGameStarted;
    }
}
