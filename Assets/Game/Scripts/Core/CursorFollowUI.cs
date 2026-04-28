using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CursorFollowUI : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        transform.SetAsLastSibling();
    }

    private void LateUpdate()
    {
        _rectTransform.position = Input.mousePosition;
        transform.SetAsLastSibling();
    }
}