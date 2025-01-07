using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
public class CardDeck : Singleton<CardDeck>
{
    public List<CardDataSO> drawDeck = new();//抽卡堆
    public List<CardDataSO> discardDeck = new();//弃牌堆
    public List<Card> handDeck = new();//手牌堆
    public Vector3 deckPoint;

    [Header("Event")]
    public IntEventSO drawCardCountEvent;
    public IntEventSO discardCardCountEvent;

    private void Start()
    {
        InitializeDeck();
    }
    public void InitializeDeck()
    {
        drawDeck.Clear();
        foreach (var entry in CardManager.Instance.currentLibrary.cardLibraryList)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }
        ShuffleDeck();
    }

    [ContextMenu("Draw Card")]
    public void DrawACard()
    {
        DrawCard(3);
    }

    public void NewTurnDrawCards()
    {
        DrawCard(GameManager.Instance.player.cardDrawEachTurn);
        SetCardLayout(GameManager.Instance.player.cardDrawEachTurn, true);
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="count"></param>
    public void DrawCard(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (drawDeck.Count <= 0)
            {
                for (int j = 0; j < discardDeck.Count; j++)
                {
                    drawDeck.Add(discardDeck[j]);
                }
                ShuffleDeck();
            }
            if (drawDeck.Count > 0)
            {
                var Card = CreateCardFromData(drawDeck[0]);
                drawDeck.RemoveAt(0);
                RelicEvent.OnCardDraw();
                drawCardCountEvent.RaiseEvent(drawDeck.Count, this);
                handDeck.Add(Card);
            }
        }
    }

    /// <summary>
    /// 设置布局
    /// </summary>
    /// <param name="count"></param>
    public void SetCardLayout(int count, bool activeSound)
    {
        List<CardTransform> cardTransforms = CardLayoutManager.Instance.GetCardTransform(handDeck.Count);
        for (int i = 0; i < cardTransforms.Count; i++)
        {
            Card currentCard = handDeck[i];
            CardTransform cardTransform = cardTransforms[i];
            currentCard.UpdateCardState();

            currentCard.isMoving = true;

            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(0.2f * (i + count - cardTransforms.Count)).onComplete = () =>
            {
                currentCard.transform.DOMove(cardTransform.position, 0.6f).onComplete = () => currentCard.isMoving = false;
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.6f);
                if (activeSound)
                    PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails("drawCard"));
            };
            //设置Sorting Order
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            currentCard.UpdatePositionRotation(cardTransform.position, cardTransform.rotation);
        }
    }
    /// <summary>
    /// 洗牌
    /// </summary>
    public void ShuffleDeck()
    {
        discardDeck.Clear();
        drawCardCountEvent.RaiseEvent(drawDeck.Count, this);
        discardCardCountEvent.RaiseEvent(discardDeck.Count, this);
        for (int i = 0; i < drawDeck.Count; i++)
        {
            CardDataSO temp = drawDeck[i];
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
    }

    /// <summary>
    /// 弃牌
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCard(object obj)
    {
        Card card = obj as Card;
        if (!CheckLighting(card.cardData) && !CheckDarkness(card.cardData))
            discardDeck.Add(card.cardData);
        handDeck.Remove(card);
        CardManager.Instance.ReturnCard(card.gameObject);
        RelicEvent.OnCardDiscard(); //弃牌时触发遗物事件
        discardCardCountEvent.RaiseEvent(discardDeck.Count, this);
        SetCardLayout(0, false);
    }

    public void OnPlayerTurnEnd()
    {
        for (int i = 0; i < handDeck.Count; i++)
        {
            if (!CheckDarkness(handDeck[i].cardData) && !CheckMetal(handDeck[i].cardData))
                discardDeck.Add(handDeck[i].cardData);
            if (!CheckMetal(handDeck[i].cardData))
            {
                CardManager.Instance.ReturnCard(handDeck[i].gameObject);
                handDeck.RemoveAt(i);
                i--;
            }
        }

        SetCardLayout(handDeck.Count, false);
        discardCardCountEvent.RaiseEvent(discardDeck.Count, this);
    }

    public void AfterRoomFightEnd(object obj)
    {
        for (int i = 0; i < CardManager.Instance.currentLibrary.cardLibraryList.Count; i++)
        {
            if (CheckAir(CardManager.Instance.currentLibrary.cardLibraryList[i].cardData))
            {
                CardManager.Instance.currentLibrary.cardLibraryList.RemoveAt(i);
                i--;
            }
        }
        ReleaseAllCards();
    }

    public Card CreateCardFromData(CardDataSO cardData)
    {
        var Card = CardManager.Instance.GetCard().GetComponent<Card>();

        Card.SetCardData(CreateCloneCardData(cardData));
        Card.transform.position = deckPoint;
        return Card;
    }

    private void ReleaseAllCards()
    {
        foreach (var card in handDeck)
        {
            CardManager.Instance.ReturnCard(card.gameObject);
        }
        handDeck.Clear();
        InitializeDeck();
    }

    private CardDataSO CreateCloneCardData(CardDataSO cardData)
    {
        CardDataSO card = Instantiate(cardData);
        for (int i = 0; i < card.effectList.Count; i++)
        {
            card.effectList[i] = Instantiate(card.effectList[i]);
        }
        return card;
    }

    public void UpdateCardUI()
    {
        for (int i = 0; i < handDeck.Count; i++)
        {
            handDeck[i].UpdateCardDescription();
        }
    }

    #region CardTagCheck
    /// <summary>
    /// 卡牌是否在回合结束进入弃牌堆
    /// </summary>
    /// <param name="cardData"></param>
    /// <returns></returns>
    public bool CheckMetal(CardDataSO cardData)
    {
        for (int i = 0; i < cardData.cardTagList.Count; i++)
        {
            if (cardData.cardTagList[i] == CardTagType.Metal)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 卡牌是否在单局对战结束后消失
    /// </summary>
    /// <param name="cardData"></param>
    /// <returns></returns>
    public bool CheckAir(CardDataSO cardData)
    {
        for (int i = 0; i < cardData.cardTagList.Count; i++)
        {
            if (cardData.cardTagList[i] == CardTagType.Air)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 卡牌是否使用后进入弃牌堆后就消失
    /// </summary>
    /// <param name="cardData"></param>
    /// <returns></returns>
    public bool CheckLighting(CardDataSO cardData)
    {
        for (int i = 0; i < cardData.cardTagList.Count; i++)
        {
            if (cardData.cardTagList[i] == CardTagType.Lighting)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 卡牌是否进入弃牌堆后就消失
    /// </summary>
    /// <param name="cardData"></param>
    /// <returns></returns>
    public bool CheckDarkness(CardDataSO cardData)
    {
        for (int i = 0; i < cardData.cardTagList.Count; i++)
        {
            if (cardData.cardTagList[i] == CardTagType.Darkness)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}
