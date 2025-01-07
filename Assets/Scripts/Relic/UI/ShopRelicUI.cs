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

    public void SetRelicUI(RelicData relic)
    {
        relicData = relic;
        relicIcon.sprite = relic.relicIcon;
        relicPrice = RelicManager.Instance.GetRelicPriceByRarity(relic);
        if (!relic.designPrice)
        {
            price.text = Convert.ToInt32(math.round((1 + UnityEngine.Random.Range(-0.1f, 0.1f)) * relicPrice)).ToString();
        }
        else
        {
            price.text = relic.relicPrice.ToString();
        }
        UpdateRelicUI();
    }

    public void SetRelicPrice(int newPrice)
    {
        relicPrice = newPrice;
    }

    public void UpdateRelicUI()
    {
        price.color = GameManager.Instance.player.characterData.Money > relicPrice ? priceColor : Color.red;
    }
}
