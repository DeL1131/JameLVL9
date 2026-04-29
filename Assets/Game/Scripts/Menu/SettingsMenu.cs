using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [SerializeField] private Button _buttonCloseSettingsMenu;
    [SerializeField] private Button _buttonCloseGame;

    private UnityAction _closeSettingsAction;
    private UnityAction _closeGameAction;

    private new void Awake()
    {
        base.Awake();

        _closeSettingsAction = () => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonCloseSettingsMenu);
        _closeGameAction = () => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonExit);
    }

    private void OnEnable()
    {
        _buttonCloseSettingsMenu.onClick.AddListener(_closeSettingsAction);
        _buttonCloseGame.onClick.AddListener(_closeGameAction);

    }

    private void OnDisable()
    {
        _buttonCloseSettingsMenu.onClick.RemoveListener(_closeSettingsAction);
        _buttonCloseGame.onClick.RemoveListener(_closeGameAction);
    }
}
