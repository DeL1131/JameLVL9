using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonSettings;
    [SerializeField] private Button _buttonExit;
    

    private UnityAction _playAction;
    private UnityAction _settingsAction;
    private UnityAction _exitAction;

    private new void Awake()
    {
        base.Awake();

        _playAction = () => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonPlay);
        _settingsAction = () => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonSettings);
        _exitAction = () => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonExit);
    }

    private void OnEnable()
    {
        _buttonPlay.onClick.AddListener(_playAction);
        _buttonSettings.onClick.AddListener(_settingsAction);
        _buttonExit.onClick.AddListener(_exitAction);
    }

    private void OnDisable()
    {
        _buttonExit.onClick.RemoveListener(_exitAction);
        _buttonPlay.onClick.RemoveListener(_playAction);
        _buttonSettings.onClick.RemoveListener(_settingsAction);
    }
}
