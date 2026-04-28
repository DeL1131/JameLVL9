using UnityEngine;

public class PrefabReference : MonoBehaviour
{
    public static PrefabReference instance { get; private set; }
    void Awake()
    {
        instance = this;
    }
    public GameObject tmpText3D;
    public GameObject infoScroll;
}
