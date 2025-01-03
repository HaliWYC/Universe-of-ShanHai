using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Components")]
    public CardDataSO cardData;
    public Image cardSprite, cardRarity, cardBackground, cardType;
    public TextMeshProUGUI cardName, costText, descriptionText;
    public Button button;
    public RectTransform tagContainer;
    public GameObject tagPrefab;

    private void Awake()
    {
        button.onClick.AddListener(OnCardClicked);
    }

    private void OnCardClicked()
    {
        PickCardPanel.Instance.OnCardClicked(button, cardData);
    }

    public void SetCardData(CardDataSO data)
    {
        cardData = data;
        cardSprite.sprite = cardData.cardImage;
        costText.text = cardData.cardCost.ToString();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UIPanel.Instance.ShowCardToolTip(cardData, transform, true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails("EnterCardUI"));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIPanel.Instance.cardToolTip.gameObject.SetActive(false);
    }

}
