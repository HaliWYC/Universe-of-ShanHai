using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopPanel : MonoBehaviour
{
    [Header("Components")]
    public GameObject CardPrefab;
    public GameObject relicPrefab;

    public RectTransform CardShopHolder;
    public RectTransform RelicShopHolder;
    public Button backButton;
    public ObjectEventSO loadMapEvent;

    [Header("Settings")]
    public MultipleRarity shopRarity;
    private bool isCardReady;
    private bool isRelicReady;

    [Header("Card")]

    private List<CardDataSO> waitingCardList = new();
    public List<CardDataSO> cardList = new();
    public int numOfCard = 8;

    [Header("Relic")]
    private List<RelicData> waitingRelicList = new();
    public List<RelicData> relicList = new();
    public int numOfRelic = 4;
    private List<RelicData> playerRelics;
    private List<RelicData> checkRelicList = new();
    private void Awake()
    {
        backButton.onClick.AddListener(BackToMap);
        backButton.interactable = false;
        playerRelics = GameManager.Instance.player.characterData.relics;
    }

    private void OnEnable()
    {
        InitCardList();
        InitRelicList();
        if (isCardReady && isRelicReady)
            backButton.interactable = true;
    }

    private void OnDisable()
    {
        cardList.Clear();
        relicList.Clear();
        for (int i = 0; i < CardShopHolder.childCount; i++)
        {
            Destroy(CardShopHolder.GetChild(i).gameObject);
        }
        for (int i = 0; i < RelicShopHolder.childCount; i++)
        {
            Destroy(RelicShopHolder.GetChild(i).gameObject);
        }

    }

    private void BackToMap()
    {
        loadMapEvent.RaiseEvent(null, this);
    }

    #region Card
    private void InitCardList()
    {
        cardList = CardManager.Instance.GetCardListByMultipleRarity(shopRarity);
        InitCards();
    }

    private void InitCards()
    {
        isCardReady = false;
        waitingCardList.Clear();
        GenerateCardWaitingList();
        int count = 0;
        for (int i = 0; i < waitingCardList.Count; i++)
        {
            var Card = Instantiate(CardPrefab, CardShopHolder.transform).GetComponent<ShopCardUI>();
            Card.gameObject.transform.localScale = Vector3.zero;
            Card.Parent = CardShopHolder;
            Card.SetCardData(waitingCardList[i]);
            Card.isMoving = true;
            Card.gameObject.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetDelay(i * 0.3f).onComplete = () =>
            {
                Card.isMoving = false;
                count++;
                if (count == numOfCard - 1)
                {
                    isCardReady = true;
                }
            };
        }
    }

    private void GenerateCardWaitingList()
    {
        for (int i = 0; i < numOfCard; i++)
        {
            CardDataSO wait = GetRandomCard();
            if (CheckNoRepeatCard(wait))
            {
                waitingCardList.Add(wait);
            }
            else
            {
                wait = GetRandomCard();
                waitingCardList.Add(wait);
            }
        }
    }

    private CardDataSO GetRandomCard()
    {
        int randomIndex = UnityEngine.Random.Range(0, cardList.Count);
        return cardList[randomIndex];
    }

    private bool CheckNoRepeatCard(CardDataSO cardDataSO)
    {
        for (int i = 0; i < waitingCardList.Count; i++)
        {
            if (waitingCardList[i] == cardDataSO)
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region Relic

    private void InitRelicList()
    {
        relicList = RelicManager.Instance.GetCardListByMultipleRarity(shopRarity);
        InitRelics();
    }
    private void InitRelics()
    {
        isRelicReady = false;
        waitingCardList.Clear();
        GenerateRelicWaitingList();
        int count = 0;
        for (int i = 0; i < waitingRelicList.Count; i++)
        {
            var Relic = Instantiate(relicPrefab, RelicShopHolder.transform).GetComponent<ShopRelicUI>();
            Relic.gameObject.transform.localScale = Vector3.zero;
            Relic.Parent = RelicShopHolder;
            Relic.SetRelic(waitingRelicList[i]);
            Relic.isMoving = true;
            Relic.gameObject.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetDelay(i * 0.3f).onComplete = () =>
            {
                Relic.isMoving = false;
                count++;
                if (count == numOfRelic - 1)
                {
                    isRelicReady = true;
                }
            };
        }
    }

    private void GenerateRelicWaitingList()
    {
        InitCheckRelicList();
        for (int i = 0; i < numOfRelic; i++)
        {
            RelicData wait = GetRandomRelic();
            if (wait != null)
            {
                if (CheckNoRepeatRelic(wait))
                {
                    waitingRelicList.Add(wait);
                }
            }

        }
    }

    private void InitCheckRelicList()
    {
        checkRelicList = relicList;
        for (int i = 0; i < playerRelics.Count; i++)
        {
            if (checkRelicList.Contains(playerRelics[i]))
            {
                checkRelicList.RemoveAt(checkRelicList.IndexOf(playerRelics[i]));
            }
        }
    }

    private RelicData GetRandomRelic()
    {
        int randomIndex = UnityEngine.Random.Range(0, checkRelicList.Count);
        return checkRelicList[randomIndex];
    }

    private bool CheckNoRepeatRelic(RelicData relicData)
    {
        for (int i = 0; i < waitingRelicList.Count; i++)
        {
            if (waitingRelicList[i].relicID == relicData.relicID)
                return false;
        }
        return true;
    }
    #endregion
}
