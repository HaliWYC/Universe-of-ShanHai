using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicUI : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public Relic relicData;
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

    public void SetRelic(Relic relic)
    {
        relicData = relic;
        relicIcon.sprite = relic.relicIcon;
    }

    public void UpdateRelicValue(Relic relic)
    {
        relicValue.text = relic.relicValue.ToString();
    }

}
