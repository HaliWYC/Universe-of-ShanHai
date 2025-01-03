using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class ShopRelicUI : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public Relic relicData;
    public Image relicIcon;
    public TextMeshProUGUI price;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("1");
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("2");
            UIPanel.Instance.ShowRelicToolTip(relicData, transform, true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIPanel.Instance.relicToolTip.gameObject.SetActive(false);
    }

    public void SetRelic(Relic relic)
    {
        relicData = relic;
        relicIcon.sprite = relic.relicIcon;
        price.text = UIManager.Instance.GetRelicPriceByRarity(relicData.relicRarity).ToString();
    }
}
