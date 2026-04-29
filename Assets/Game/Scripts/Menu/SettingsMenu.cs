using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [SerializeField] private Button _buttonCloseSettingsMenu;
    [SerializeField] private Button _buttonMainToMenu;

    private UnityAction _closeSettingsAction;
    private UnityAction _ReturnToMainAction;

    private new void Awake()
    {
        base.Awake();

        _closeSettingsAction = () => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonCloseSettingsMenu);
        _ReturnToMainAction = () => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonMainToMenu);
    }

    private void OnEnable()
    {
        _buttonCloseSettingsMenu.onClick.AddListener(_closeSettingsAction);
        _buttonMainToMenu.onClick.AddListener(_ReturnToMainAction);

    }

    private void OnDisable()
    {
        _buttonCloseSettingsMenu.onClick.RemoveListener(_closeSettingsAction);
        _buttonMainToMenu.onClick.RemoveListener(_ReturnToMainAction);
    }
}
