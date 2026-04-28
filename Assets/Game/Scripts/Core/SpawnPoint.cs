using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool _isPointPlacement = false;

    public bool IsPointPlacement => _isPointPlacement;

    public void SetState(bool state)
    {
        _isPointPlacement = state;
    }
}
