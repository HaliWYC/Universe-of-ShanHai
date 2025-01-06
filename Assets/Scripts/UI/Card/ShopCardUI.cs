using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Components")]
    public CardDataSO cardData;
    public Image cardSprite, cardRarity, cardBackground, cardType;
    public TextMeshProUGUI cardName, costText, descriptionText, priceText, amountText;
    public RectTransform tagContainer;
    public GameObject tagPrefab;

    public Color PriceColor;
    public GameObject sellOut;
    public RectTransform Parent;
    public Vector3 originalPosition;
    private int cardPrice;
    private int CardAmount = 1;
    public bool isSold = false;
    public bool isMoving = false;
    private void Awake()
    {
        originalPosition = transform.localScale;
        sellOut.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSold || isMoving) return;
        PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails("EnterCardUI"));
        originalPosition = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSold || isMoving) return;
        transform.localScale = originalPosition;

        UIPanel.Instance.cardToolTip.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSold || isMoving) return;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UIPanel.Instance.ShowCardToolTip(cardData, transform, true);
        }
        if (eventData.clickCount % 2 == 0)
        {
            if (GameManager.Instance.player.characterData.Money >= cardPrice)
            {
                CardAmount--;
                GameManager.Instance.player.characterData.Money -= cardPrice;
                CardManager.Instance.UnlockCard(cardData);
                UIPanel.Instance.UpdateCurrencyText();
                ShopPanel.Instance.UpdateAllShopUI();
            }
            if (CardAmount == 0)
            {
                isSold = true;
                sellOut.SetActive(true);
                cardBackground.color = new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.5f);
                transform.localScale = originalPosition;
            }
        }
    }

    public void SetCardData(CardDataSO data)
    {
        cardData = data;
        cardSprite.sprite = cardData.cardImage;
        costText.text = cardData.cardCost.ToString();
        cardPrice = Convert.ToInt32(math.round(CardManager.Instance.GetCardPrice(cardData) * (1 + UnityEngine.Random.Range(-0.1f, 0.1f))));
        UpdateCardUI();
        UpdateCardDescription();
        cardName.text = cardData.cardName;
        cardType.sprite = UIManager.Instance.GetSpriteByCardType(cardData.cardType);
        cardRarity.color = UIManager.Instance.GetColorByCardRarity(cardData.cardRarity);
        cardBackground.sprite = UIManager.Instance.GetSpriteByCardRarity(cardData.cardRarity);
        SetUpTag();
    }

    public void UpdateCardDescription()
    {
        string[] strings = cardData.cardDescription.Split("#");
        int effectIndex = 0;
        string returnString = "";
        for (int i = 0; i < strings.Length; i++)
        {
            if (i % 2 == 1)
            {
                while (effectIndex < cardData.effectList.Count && cardData.effectList[effectIndex] is AddCardToHandEffect)
                    effectIndex++;
                strings[i] = cardData.effectList[effectIndex].GetCurrentValue(cardData.effectList[effectIndex]).ToString();
            }
            returnString += strings[i];
        }
        descriptionText.text = returnString;
    }

    public void UpdateCardUI()
    {
        priceText.text = cardPrice.ToString();
        priceText.color = GameManager.Instance.player.characterData.Money > cardPrice ? PriceColor : Color.red;
        amountText.text = "X" + CardAmount;
    }

    public void SetUpTag()
    {
        for (int i = 0; i < tagContainer.childCount; i++)
        {
            Destroy(tagContainer.GetChild(i).gameObject);
        }
        for (int i = 0; i < cardData.cardTagList.Count; i++)
        {
            var tag = Instantiate(tagPrefab, tagContainer).GetComponent<CardTag>();
            tag.SetCardTag(cardData.cardTagList[i]);
        }
    }
}
