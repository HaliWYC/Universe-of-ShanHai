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
    private int relicPrice;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMoving) return;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UIPanel.Instance.ShowRelicToolTip(relicData, transform, true);
        }
        if (eventData.clickCount % 2 == 0)
        {
            if (GameManager.Instance.player.characterData.Money >= relicPrice)
            {
                GameManager.Instance.player.characterData.Money -= relicPrice;
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

    public void SetRelicUI(RelicData relic)
    {
        relicData = relic;
        relicIcon.sprite = relic.relicIcon;
        relicPrice = RelicManager.Instance.GetRelicPriceByRarity(relicData);
        UpdateRelicUI();
    }

    public void SetRelicPrice(int newPrice)
    {
        relicPrice = newPrice;
        UpdateRelicUI();
    }

    public void UpdateRelicUI()
    {
        price.text = relicPrice.ToString();
        price.color = GameManager.Instance.player.characterData.Money > relicPrice ? priceColor : Color.red;
    }
}
