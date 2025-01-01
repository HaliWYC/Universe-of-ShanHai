using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Components")]
    public CardDataSO cardData;
    public SpriteRenderer cardSprite, cardRarity, cardBackground, cardType;
    public TextMeshPro cardName, costText, descriptionText;
    public RectTransform tagContainer;
    public GameObject tagPrefab;

    [Header("OriginalData")]
    public Vector3 originPosition;
    public Quaternion originRotation;
    public int originLayerOrder;

    public bool isMoving;
    public bool isAvailable;
    public ObjectEventSO discardEvent;
    public IntEventSO costEvent;
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

    public void UpdatePositionRotation(Vector3 position, Quaternion rotation)
    {
        originPosition = position;
        originRotation = rotation;
        originLayerOrder = GetComponent<SortingGroup>().sortingOrder;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isMoving) return;
        transform.position = new Vector3(originPosition.x, -4.2f, originPosition.z);
        transform.rotation = Quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = 20;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UIPanel.Instance.ShowCardToolTip(cardData, transform, false);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isMoving) return;
        ResetCardPositionRotation();
        UIPanel.Instance.cardToolTip.gameObject.SetActive(false);
    }

    public void ResetCardPositionRotation()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
        GetComponent<SortingGroup>().sortingOrder = originLayerOrder;
    }

    public void ExecuteCardEffects(CharacterBase from, CharacterBase target)
    {
        costEvent.RaiseEvent(cardData.cardCost, this);
        discardEvent.RaiseEvent(this, this);
        foreach (var effect in cardData.effectList)
        {
            effect.Setup(from, target);
        }
        CardDeck.Instance.SetCardLayout(CardDeck.Instance.handDeck.Count, false);
    }

    public void UpdateCardState()
    {
        isAvailable = cardData.cardCost <= GameManager.Instance.player.CurrentMana;
        costText.color = isAvailable ? Color.green : Color.red;
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


}
