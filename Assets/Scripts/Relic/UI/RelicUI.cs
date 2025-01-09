using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicUI : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public RelicData relicData;
    public Image relicIcon;
    public Text relicValue;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UIPanel.Instance.ShowRelicToolTip(relicData, transform, false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIPanel.Instance.relicToolTip.gameObject.SetActive(false);
    }

    public void SetRelic(RelicData relic)
    {
        relicData = relic;
        if (!relic.showRelicValue)
            relicValue.gameObject.SetActive(false);
        relicIcon.sprite = relic.relicIcon;
    }

    public void UpdateRelicValue(RelicData relic)
    {
        relicValue.text = relic.relicValue.ToString();
    }

}
