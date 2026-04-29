using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class TorchObject : InteractableObject
{
    [Header("Save")]
    [SerializeField] private string _saveId;

    [SerializeField] private Light2D torchLight;

    public bool isLit { get; private set; } = true;

    private Animator torchAnimator;
    private string _runtimeSaveId;

    private void Awake()
    {
        torchAnimator = GetComponent<Animator>();
        _runtimeSaveId = GetSaveId();
    }

    protected override void Start()
    {
        base.Start();
        RestoreSavedState();
    }

    protected override void Interact()
    {
        SetLitState(!isLit);

        if (_audioSource != null)
            _audioSource.Play();

        SaveState();
    }

    private void RestoreSavedState()
    {
        if (GameSession.Instance == null)
            return;

        if (GameSession.Instance.TryGetTorchState(_runtimeSaveId, out bool savedIsLit) == false)
            return;

        SetLitState(savedIsLit);
    }

    private void SaveState()
    {
        if (GameSession.Instance == null)
            return;

        GameSession.Instance.SaveTorchState(_runtimeSaveId, isLit);
    }

    private void SetLitState(bool lit)
    {
        isLit = lit;

        if (torchLight != null)
            torchLight.enabled = lit;

        if (torchAnimator != null)
            torchAnimator.SetBool("IsLit", lit);
    }

    private string GetSaveId()
    {
        if (string.IsNullOrWhiteSpace(_saveId) == false)
            return _saveId;

        return $"{SceneManager.GetActiveScene().name}/{GetHierarchyPath()}";
    }

    private string GetHierarchyPath()
    {
        Transform currentTransform = transform;
        string path = $"{currentTransform.name}[{currentTransform.GetSiblingIndex()}]";

        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
            path = $"{currentTransform.name}[{currentTransform.GetSiblingIndex()}]/{path}";
        }

        return path;
    }
}
