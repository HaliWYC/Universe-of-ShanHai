using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : Singleton<CardManager>, ISavable
{
    public List<CardDataSO> cardDataList;
    public List<CardDataSO> normalCardList;
    public List<CardDataSO> superiorCardList;
    public List<CardDataSO> eliteCardList;
    public List<CardDataSO> epicCardList;
    public List<CardDataSO> legendaryCardList;
    public List<CardDataSO> mythicalCardList;

    public CardLibrarySO newGameLibrary;
    public CardLibrarySO currentLibrary;

    public string GUID => GetComponent<DataGUID>().guid;

    protected override void Awake()
    {
        base.Awake();
        InitializeCardDataList();
        currentLibrary.cardLibraryList.Clear();
        foreach (var card in newGameLibrary.cardLibraryList)
        {
            currentLibrary.cardLibraryList.Add(card);
        }
    }

    private void Start()
    {
        ISavable savable = this;
        savable.RegisterSavable();
    }
    public void InitCardList()
    {
        normalCardList.Clear();
        superiorCardList.Clear();
        eliteCardList.Clear();
        epicCardList.Clear();
        legendaryCardList.Clear();
        mythicalCardList.Clear();
        for (int i = 0; i < cardDataList.Count; i++)
        {
            switch (cardDataList[i].cardRarity)
            {
                case Rarity.Normal:
                    normalCardList.Add(cardDataList[i]);
                    break;
                case Rarity.Superior:
                    superiorCardList.Add(cardDataList[i]);
                    break;
                case Rarity.Elite:
                    eliteCardList.Add(cardDataList[i]);
                    break;
                case Rarity.Epic:
                    epicCardList.Add(cardDataList[i]);
                    break;
                case Rarity.Legendary:
                    legendaryCardList.Add(cardDataList[i]);
                    break;
                case Rarity.Mythical:
                    mythicalCardList.Add(cardDataList[i]);
                    break;
            }
        }
    }
    private void InitializeCardDataList()
    {
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cardDataList = new List<CardDataSO>(handle.Result);
            InitCardList();
        }
        else
        {
            Debug.LogError("Failed to load card data");
        }
    }

    public GameObject GetCard()
    {
        var card = PoolTool.Instance.GetObjectFromPool();
        card.transform.localScale = Vector3.zero;
        return card;
    }

    public void ReturnCard(GameObject card)
    {
        PoolTool.Instance.ReleaseObject(card);
    }
    public void UnlockCard(CardDataSO cardData)
    {
        var newCard = new CardLibraryEntry { cardData = cardData, amount = 1 };
        for (int i = 0; i < currentLibrary.cardLibraryList.Count; i++)
        {
            if (currentLibrary.cardLibraryList[i].cardData == newCard.cardData)
            {
                newCard.amount += currentLibrary.cardLibraryList[i].amount;
                currentLibrary.cardLibraryList[i] = newCard;
                return;
            }
        }
        currentLibrary.cardLibraryList.Add(newCard);
    }
    public int GetCardPrice(CardDataSO cardData)
    {
        return cardData.cardRarity switch
        {
            Rarity.Normal => 25,
            Rarity.Superior => 75,
            Rarity.Elite => 120,
            Rarity.Epic => 160,
            Rarity.Legendary => 220,
            Rarity.Mythical => 300,
            _ => 10
        };
    }

    public List<CardDataSO> GetCardRarityList(Rarity cardRarity)
    {
        return cardRarity switch
        {
            Rarity.Normal => normalCardList,
            Rarity.Superior => superiorCardList,
            Rarity.Elite => eliteCardList,
            Rarity.Epic => epicCardList,
            Rarity.Legendary => legendaryCardList,
            Rarity.Mythical => mythicalCardList,
            _ => null,
        };
    }
    public List<CardDataSO> GetCardRarityList(MultipleRarity cardRarity)
    {

        return cardRarity switch
        {
            MultipleRarity.Normal => normalCardList,
            MultipleRarity.Superior => superiorCardList,
            MultipleRarity.Elite => eliteCardList,
            MultipleRarity.Epic => epicCardList,
            MultipleRarity.Legendary => legendaryCardList,
            MultipleRarity.Mythical => mythicalCardList,
            _ => cardDataList,
        };
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.playerCurrentCardLibraries = currentLibrary;
        return null;
    }

    public void RestoreData(GameSaveData data)
    {
        currentLibrary = data.playerCurrentCardLibraries;
    }

    public List<CardDataSO> GetCardListByMultipleRarity(MultipleRarity multipleRarity)
    {
        List<CardDataSO> cardList = new List<CardDataSO>();
        string[] rarities = multipleRarity.ToString().Split(',');
        for (int i = 0; i < rarities.Length; i++)
        {
            MultipleRarity cardRarity = (MultipleRarity)Enum.Parse(typeof(MultipleRarity), rarities[i]);
            cardList.AddRange(GetCardRarityList(cardRarity));
        }
        return cardList;
    }
}
