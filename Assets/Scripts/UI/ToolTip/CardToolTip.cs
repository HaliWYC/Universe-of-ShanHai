using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardToolTip : MonoBehaviour
{
    [SerializeField] private CardDataSO cardData;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDescription;
    [SerializeField] private TextMeshProUGUI cardType;
    [SerializeField] private TextMeshProUGUI cardRarity;

    public RectTransform Middle;
    public RectTransform Bottom;
    public RectTransform tagContainer;
    public GameObject tagPrefab;

    public void SetCardData(CardDataSO CardData)
    {
        cardData = CardData;
        cardName.text = cardData.cardName;
        cardDescription.text = cardData.cardDescription;
        cardType.text = cardData.cardType.ToString();
        cardRarity.text = cardData.cardRarity.ToString();
        UpdateCardDescription();
        SetUpTag();
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        gameObject.SetActive(true);
    }

    public void UpdateCardDescription()
    {
        for (int j = 0; j < cardData.effectList.Count; j++)
        {
            cardData.effectList[j].UpdateUI();
        }
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
        cardDescription.text = returnString;
    }


    public void SetUpTag()
    {
        if (cardData != null)
        {
            if (cardData.cardTagList.Count > 0)
            {
                // Clear previous tags
                for (int i = 0; i < tagContainer.childCount; i++)
                {
                    Destroy(tagContainer.GetChild(i).gameObject);
                }
                // Add new tags
                for (int i = 0; i < cardData.cardTagList.Count; i++)
                {
                    CardTag newTag = Instantiate(tagPrefab, tagContainer).GetComponent<CardTag>();
                    newTag.SetCardTag(cardData.cardTagList[i]);
                }
                Bottom.gameObject.SetActive(true);
            }
            else
            {
                Bottom.gameObject.SetActive(false);
            }
        }

    }
}
