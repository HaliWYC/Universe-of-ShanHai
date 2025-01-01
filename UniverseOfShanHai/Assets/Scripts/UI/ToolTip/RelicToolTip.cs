using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI relicName;
    [SerializeField] private TextMeshProUGUI relicRarity;
    [SerializeField] private TextMeshProUGUI relicDescription;

    public void SetRelicData(Relic relic)
    {
        relicName.text = relic.relicName;
        relicRarity.text = relic.relicRarity.ToString();
        relicDescription.text = relic.description;
        gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponentInParent<RectTransform>());
    }
}
