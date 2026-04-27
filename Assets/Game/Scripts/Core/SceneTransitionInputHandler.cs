using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneTransitionInputHandler : MonoBehaviour
{
    [SerializeField] private GameFlowManager _gameFlowManager;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference _returnToMainAction;
    [SerializeField] private InputActionReference _restartAction;

    [Header("Debug Mini Game Actions")]
    [SerializeField] private InputActionReference _miniGame1Action;
    [SerializeField] private InputActionReference _miniGame2Action;
    [SerializeField] private InputActionReference _miniGame3Action;
    [SerializeField] private InputActionReference _miniGame4Action;
    [SerializeField] private InputActionReference _miniGame5Action;

    private InputAction _runtimeReturnToRitualAction;
    private InputAction _runtimeRestartAction;

    private InputAction _runtimeMiniGame1Action;
    private InputAction _runtimeMiniGame2Action;
    private InputAction _runtimeMiniGame3Action;
    private InputAction _runtimeMiniGame4Action;
    private InputAction _runtimeMiniGame5Action;

    private InputAction ReturnToRitualAction => _returnToMainAction != null
        ? _returnToMainAction.action
        : _runtimeReturnToRitualAction;

    private InputAction RestartAction => _restartAction != null
        ? _restartAction.action
        : _runtimeRestartAction;

    private InputAction MiniGame1Action => _miniGame1Action != null
        ? _miniGame1Action.action
        : _runtimeMiniGame1Action;

    private InputAction MiniGame2Action => _miniGame2Action != null
        ? _miniGame2Action.action
        : _runtimeMiniGame2Action;

    private InputAction MiniGame3Action => _miniGame3Action != null
        ? _miniGame3Action.action
        : _runtimeMiniGame3Action;

    private InputAction MiniGame4Action => _miniGame4Action != null
        ? _miniGame4Action.action
        : _runtimeMiniGame4Action;

    private InputAction MiniGame5Action => _miniGame5Action != null
        ? _miniGame5Action.action
        : _runtimeMiniGame5Action;

    private void Awake()
    {
        if (_gameFlowManager == null)
            _gameFlowManager = GameFlowManager.Instance;

        CreateFallbackActionsIfNeeded();
    }

    private void OnEnable()
    {
        Subscribe(ReturnToRitualAction, OnReturnToMainGame);
        Subscribe(RestartAction, OnRestartPerformed);

        Subscribe(MiniGame1Action, OnMiniGame1Performed);
        Subscribe(MiniGame2Action, OnMiniGame2Performed);
        Subscribe(MiniGame3Action, OnMiniGame3Performed);
        Subscribe(MiniGame4Action, OnMiniGame4Performed);
        Subscribe(MiniGame5Action, OnMiniGame5Performed);
    }

    private void OnDisable()
    {
        Unsubscribe(ReturnToRitualAction, OnReturnToMainGame);
        Unsubscribe(RestartAction, OnRestartPerformed);

        Unsubscribe(MiniGame1Action, OnMiniGame1Performed);
        Unsubscribe(MiniGame2Action, OnMiniGame2Performed);
        Unsubscribe(MiniGame3Action, OnMiniGame3Performed);
        Unsubscribe(MiniGame4Action, OnMiniGame4Performed);
        Unsubscribe(MiniGame5Action, OnMiniGame5Performed);
    }

    private void OnDestroy()
    {
        _runtimeReturnToRitualAction?.Dispose();
        _runtimeRestartAction?.Dispose();

        _runtimeMiniGame1Action?.Dispose();
        _runtimeMiniGame2Action?.Dispose();
        _runtimeMiniGame3Action?.Dispose();
        _runtimeMiniGame4Action?.Dispose();
        _runtimeMiniGame5Action?.Dispose();
    }

    private void OnReturnToMainGame(InputAction.CallbackContext context)
    {
        GetGameFlowManager()?.LoadMainGameScene();
    }

    private void OnRestartPerformed(InputAction.CallbackContext context)
    {
        GetGameFlowManager()?.ReloadCurrentScene();
    }

    private void OnMiniGame1Performed(InputAction.CallbackContext context)
    {
        LoadMiniGame(1);
    }

    private void OnMiniGame2Performed(InputAction.CallbackContext context)
    {
        LoadMiniGame(2);
    }

    private void OnMiniGame3Performed(InputAction.CallbackContext context)
    {
        LoadMiniGame(3);
    }

    private void OnMiniGame4Performed(InputAction.CallbackContext context)
    {
        LoadMiniGame(4);
    }

    private void OnMiniGame5Performed(InputAction.CallbackContext context)
    {
        LoadMiniGame(5);
    }

    private void LoadMiniGame(int miniGameIndex)
    {
        GetGameFlowManager()?.LoadMiniGame(miniGameIndex);
    }

    private GameFlowManager GetGameFlowManager()
    {
        if (_gameFlowManager == null)
            _gameFlowManager = GameFlowManager.Instance;

        if (_gameFlowManager == null)
            Debug.LogWarning("SceneTransitionInputHandler could not find a GameFlowManager.");

        return _gameFlowManager;
    }

    private void Subscribe(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        if (action == null)
            return;

        action.performed += callback;
        action.Enable();
    }

    private void Unsubscribe(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        if (action == null)
            return;

        action.performed -= callback;
        action.Disable();
    }

    private void CreateFallbackActionsIfNeeded()
    {
        if (_returnToMainAction == null)
        {
            _runtimeReturnToRitualAction = new InputAction("ReturnToRitual", InputActionType.Button);
            _runtimeReturnToRitualAction.AddBinding("<Keyboard>/tab");
            _runtimeReturnToRitualAction.AddBinding("<Gamepad>/start");
            _runtimeReturnToRitualAction.AddBinding("<Gamepad>/select");
        }

        if (_restartAction == null)
        {
            _runtimeRestartAction = new InputAction("Restart", InputActionType.Button);
            _runtimeRestartAction.AddBinding("<Keyboard>/r");
        }

        if (_miniGame1Action == null)
            _runtimeMiniGame1Action = new InputAction("MiniGame1", InputActionType.Button, "<Keyboard>/1");

        if (_miniGame2Action == null)
            _runtimeMiniGame2Action = new InputAction("MiniGame2", InputActionType.Button, "<Keyboard>/2");

        if (_miniGame3Action == null)
            _runtimeMiniGame3Action = new InputAction("MiniGame3", InputActionType.Button, "<Keyboard>/3");

        if (_miniGame4Action == null)
            _runtimeMiniGame4Action = new InputAction("MiniGame4", InputActionType.Button, "<Keyboard>/4");

        if (_miniGame5Action == null)
            _runtimeMiniGame5Action = new InputAction("MiniGame5", InputActionType.Button, "<Keyboard>/5");
    }
}