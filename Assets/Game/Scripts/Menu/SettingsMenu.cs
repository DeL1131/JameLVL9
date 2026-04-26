using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [SerializeField] private Button _buttonCloseSettingsMenu;

    private UnityAction _closeSettingsAction;

    private new void Awake()
    {
        base.Awake();

        _closeSettingsAction = () => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonCloseSettingsMenu);
    }

    private void OnEnable()
    {
        _buttonCloseSettingsMenu.onClick.AddListener(_closeSettingsAction);
    }

    private void OnDisable()
    {
        _buttonCloseSettingsMenu.onClick.RemoveListener(_closeSettingsAction);
    }
}
