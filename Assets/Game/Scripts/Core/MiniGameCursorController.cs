using UnityEngine;

public class MiniGameCursorController : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}