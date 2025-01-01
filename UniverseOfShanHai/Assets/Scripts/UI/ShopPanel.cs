using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopPanel : MonoBehaviour
{
    public GameObject CardPrefab;

    public RectTransform CardShopHolder;

    public ShopRarity shopRarity;

    public List<CardDataSO> cardList = new();
    public Button backButton;
    public ObjectEventSO loadMapEvent;
    public int numOfCard = 8;

    private void Awake()
    {
        backButton.onClick.AddListener(BackToMap);
        backButton.interactable = false;
    }

    private void OnEnable()
    {
        InitCardList();
        InitCards();
    }

    private void OnDisable()
    {
        cardList.Clear();
        for (int i = 0; i < CardShopHolder.childCount; i++)
        {
            Destroy(CardShopHolder.GetChild(i).gameObject);
        }
    }

    private void BackToMap()
    {
        loadMapEvent.RaiseEvent(null, this);
    }

    private void InitCardList()
    {
        string[] rarities = shopRarity.ToString().Split(',');
        for (int i = 0; i < rarities.Length; i++)
        {
            Rarity cardRarity = (Rarity)Enum.Parse(typeof(ShopRarity), rarities[i]);
            for (int c = 0; c < CardManager.Instance.GetCardRarityList(cardRarity).Count; c++)
            {
                cardList.Add(CardManager.Instance.GetCardRarityList(cardRarity)[c]);
            }
        }
    }

    private void InitCards()
    {
        int count = 0;
        for (int i = 0; i < numOfCard; i++)
        {

            var Card = Instantiate(CardPrefab, CardShopHolder.transform).GetComponent<ShopCardUI>();
            Card.gameObject.transform.localScale = Vector3.zero;
            Card.Parent = CardShopHolder;
            Card.SetCardData(GetRandomCard());
            Card.isMoving = true;
            Card.gameObject.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetDelay(i * 0.3f).onComplete = () =>
            {
                Card.isMoving = false;
                count++;
                if (count == numOfCard - 1)
                {
                    backButton.interactable = true;
                }
            };


        }
    }

    private CardDataSO GetRandomCard()
    {
        int randomIndex = UnityEngine.Random.Range(0, cardList.Count);
        return cardList[randomIndex];
    }
}
