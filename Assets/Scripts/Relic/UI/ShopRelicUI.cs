using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.Mathematics;
using System;
public class ShopRelicUI : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public RelicData relicData;
    public Image relicIcon;
    public RectTransform Parent;
    public TextMeshProUGUI price;
    public Color priceColor;
    public bool isMoving;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMoving) return;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UIPanel.Instance.ShowRelicToolTip(relicData, transform, true);
        }
        if (eventData.clickCount % 2 == 0)
        {
            if (GameManager.Instance.player.characterData.Money >= relicData.relicPrice)
            {
                GameManager.Instance.player.characterData.Money -= relicData.relicPrice;
                RelicManager.Instance.EquipRelic(relicData);
                UIPanel.Instance.UpdateCurrencyText();
                ShopPanel.Instance.UpdateAllShopUI();
                gameObject.SetActive(false);
                UIPanel.Instance.relicToolTip.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIPanel.Instance.relicToolTip.gameObject.SetActive(false);
    }

    public void SetRelic(RelicData relic)
    {
        relicData = relic;
        relicIcon.sprite = relic.relicIcon;
        relic.relicPrice = RelicManager.Instance.GetRelicPriceByRarity(relic);
        relic.relicPrice = Convert.ToInt32(math.round((1 + UnityEngine.Random.Range(-0.1f, 0.1f)) * relic.relicPrice));
        UpdateRelicUI();
    }

    public void UpdateRelicUI()
    {
        price.text = relicData.relicPrice.ToString();
        price.color = GameManager.Instance.player.characterData.Money > relicData.relicPrice ? priceColor : Color.red;
    }
}
