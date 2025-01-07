using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCardPanel : Singleton<PickCardPanel>
{
    private CardDataSO currentCardData;

    [SerializeField] private RectTransform cardContainer;

    [SerializeField] private GameObject cardPreb;

    public List<CardDataSO> rewardList;

    [HideInInspector] public List<CardDataSO> waitingCardList;



    private List<Button> cardButtons = new();

    [SerializeField] private Button confirmButton;

    public ObjectEventSO finishPickCardEvent;
    protected override void Awake()
    {
        base.Awake();
        confirmButton.onClick.AddListener(ConfirmButtonClicked);
    }
    private void OnEnable()
    {

        CreateCards();
    }

    private void ConfirmButtonClicked()
    {
        if (currentCardData != null)
        {
            CardManager.Instance.UnlockCard(currentCardData);
            finishPickCardEvent.RaiseEvent(null, this);
        }

    }

    public void OnCardClicked(Button cardButton, CardDataSO cardData)
    {
        currentCardData = cardData;
        for (int i = 0; i < cardButtons.Count; i++)
        {
            if (cardButtons[i] == cardButton)
            {
                cardButtons[i].interactable = false;
            }
            else
            {
                cardButtons[i].interactable = true;
            }
        }
    }

    public CardDataSO GetRandomCard()
    {
        int randomIndex = 0;
        do
        {
            randomIndex = Random.Range(0, rewardList.Count);
        }
        while (CheckNoRepeatCard(rewardList[randomIndex]));
        return rewardList[randomIndex];
    }

    public void CreateCards()
    {
        if (cardContainer.childCount > 0)
        {
            for (int i = 0; i < cardContainer.childCount; i++)
            {
                Destroy(cardContainer.GetChild(i).gameObject);
            }
        }
        if (rewardList.Count == 0) return;
        waitingCardList.Clear();
        int num = rewardList.Count > 3 ? 3 : rewardList.Count;
        for (int i = 0; i < num; i++)
        {
            var card = Instantiate(cardPreb, cardContainer).GetComponent<CardUI>();
            var data = rewardList.Count > 3 ? GetRandomCard() : rewardList[i];
            waitingCardList.Add(data);
            card.SetCardData(data);
            cardButtons.Add(card.button);
        }
    }


    private bool CheckNoRepeatCard(CardDataSO cardDataSO)
    {
        for (int i = 0; i < waitingCardList.Count; i++)
        {
            if (waitingCardList[i] == cardDataSO)
            {
                return true;
            }
        }
        return false;
    }
}
