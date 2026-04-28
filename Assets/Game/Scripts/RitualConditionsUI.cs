using UnityEngine;
using TMPro;

public class RitualConditionsUI : MonoBehaviour
{
    public static RitualConditionsUI instance;
    [SerializeField] private TextMeshProUGUI text;

    private bool[] conditions = new bool[6];

    private string[] lines =
    {
        "1. Правды отражение",
        "2. Туман хрустальный",
        "3. Силу жизненную",
        "4. Подспорье мертвеца",
        "5. Знаний хранилище",
        "6. Истина открывается только во тьме"
    };
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        CheckConditions();
        UpdateText();
    }
    public void CheckConditions()
    {
        conditions[0] = PentagramObject.instance.HasItem(ItemObject.ItemType.Mirror);
        conditions[1] = PentagramObject.instance.HasItem(ItemObject.ItemType.Orb);
        conditions[2] = PentagramObject.instance.HasItem(ItemObject.ItemType.BloodFlask);
        conditions[3] = PentagramObject.instance.HasItem(ItemObject.ItemType.Skull) || PentagramObject.instance.HasItem(ItemObject.ItemType.Bone);
        conditions[4] = PentagramObject.instance.HasItem(ItemObject.ItemType.Grimoir);
        conditions[5] = true;
        Collider2D[] litObjects = Physics2D.OverlapCircleAll(PedestalObject.instance.transform.position, 8, LayerMask.GetMask("light"));

        foreach (var obj in litObjects)
        {

            bool isCurrentLit = false;
            if (obj.TryGetComponent(out TorchObject torch))
            {
                isCurrentLit = torch.isLit;

            }
            if (obj.TryGetComponent(out CandleObject candle))
            {
                isCurrentLit = candle.isLit;

            }
            if (isCurrentLit) conditions[5] = false;
        }
    }

    private void UpdateText()
    {
        string result = "Полотно реальности требует пяти аспектов\n";

       

        for (int i = 0; i < lines.Length; i++)
        {
            string color = conditions[i] ? "green" : "red";
            result += $"<color={color}>{lines[i]}</color>\n";
        }
         
        result += "Нажмите Q - Чтобы выбросить предмет \n Нажмите E - Чтобы взаимодействовать с объектом \n WASD - Для передвижения";
        text.text = result;
    }

    public bool GetIfAllTrue()
    {
        foreach (bool condition in conditions)
        {
            if (!condition) return false;
        }
        return true;
    }
}