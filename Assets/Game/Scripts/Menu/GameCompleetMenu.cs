using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameCompleetMenu : Menu
{
    [SerializeField] private Button _buttonCloseGame;

    private void OnEnable()
    {
        _buttonCloseGame.onClick.AddListener(CloseGame);
    }

    private void OnDisable()
    {
        _buttonCloseGame.onClick.RemoveListener(CloseGame);
    }

    private void CloseGame()
    {
        Application.Quit();
        Debug.Log("Game quit.");
    }
}
