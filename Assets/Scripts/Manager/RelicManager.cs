using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RelicManager : Singleton<RelicManager>
{
    private List<RelicData> relicDataList;
    [SerializeField] private List<RelicData> alwaysAvailableRelicList;
    public List<RelicData> availableRelicList;
    public List<RelicData> normalRelicList;
    public List<RelicData> superiorRelicList;
    public List<RelicData> eliteRelicList;
    public List<RelicData> epicRelicList;
    public List<RelicData> legendaryRelicList;
    public List<RelicData> mythicalRelicList;


    protected override void Awake()
    {
        base.Awake();
        SetRelicAvailable();
        InitializeRelicDataList();
    }

    private void SetRelicAvailable()
    {
        for (int i = 0; i < alwaysAvailableRelicList.Count; i++)
        {
            alwaysAvailableRelicList[i].isAvailable = true;
        }
    }
    private void InitRelicList()
    {
        for (int i = 0; i < relicDataList.Count; i++)
        {
            if (relicDataList[i].isAvailable)
            {
                availableRelicList.Add(relicDataList[i]);
            }
        }
        InitRelicRarityList();
    }

    private void InitRelicRarityList()
    {
        normalRelicList.Clear();
        superiorRelicList.Clear();
        eliteRelicList.Clear();
        epicRelicList.Clear();
        legendaryRelicList.Clear();
        mythicalRelicList.Clear();
        for (int i = 0; i < availableRelicList.Count; i++)
        {
            switch (relicDataList[i].relicRarity)
            {
                case Rarity.Normal:
                    normalRelicList.Add(availableRelicList[i]);
                    break;
                case Rarity.Superior:
                    superiorRelicList.Add(availableRelicList[i]);
                    break;
                case Rarity.Elite:
                    eliteRelicList.Add(availableRelicList[i]);
                    break;
                case Rarity.Epic:
                    epicRelicList.Add(availableRelicList[i]);
                    break;
                case Rarity.Legendary:
                    legendaryRelicList.Add(availableRelicList[i]);
                    break;
                case Rarity.Mythical:
                    mythicalRelicList.Add(availableRelicList[i]);
                    break;
            }
        }
    }

    private void InitializeRelicDataList()
    {
        Addressables.LoadAssetsAsync<RelicData>("RelicData", null).Completed += OnRelicDataLoaded;
    }

    private void OnRelicDataLoaded(AsyncOperationHandle<IList<RelicData>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            relicDataList = new List<RelicData>(handle.Result);
            InitRelicList();
        }
        else
        {
            Debug.LogError("Failed to load relic data");
        }
    }

    public void UnlockRelic(RelicData relic)
    {
        for (int i = 0; i < relicDataList.Count; i++)
        {
            if (relicDataList[i].relicID == relic.relicID)
            {
                return;
            }
        }
        alwaysAvailableRelicList.Add(relic);
        availableRelicList.Add(relic);
        relic.isAvailable = true;
        InitRelicRarityList();
    }

    public void EquipRelic(RelicData relic)
    {
        if (availableRelicList.Contains(relic))
        {
            GameManager.Instance.player.characterData.relics.Add(relic);
            UIPanel.Instance.InitRelics();
        }

    }

    public int GetRelicPriceByRarity(RelicData relic)
    {
        if (relic.designPrice)
            return relic.relicPrice;
        return relic.relicRarity switch
        {
            Rarity.Normal => 70,
            Rarity.Superior => 140,
            Rarity.Elite => 220,
            Rarity.Epic => 350,
            Rarity.Legendary => 500,
            Rarity.Mythical => 800,
            _ => 70
        };
    }

    public List<RelicData> GetRelicRarityList(MultipleRarity relicRarity)
    {

        return relicRarity switch
        {
            MultipleRarity.Normal => normalRelicList,
            MultipleRarity.Superior => superiorRelicList,
            MultipleRarity.Elite => eliteRelicList,
            MultipleRarity.Epic => epicRelicList,
            MultipleRarity.Legendary => legendaryRelicList,
            MultipleRarity.Mythical => mythicalRelicList,
            _ => relicDataList
        };
    }

    public List<RelicData> GetCardListByMultipleRarity(MultipleRarity multipleRarity)
    {
        List<RelicData> relicList = new List<RelicData>();
        string[] rarities = multipleRarity.ToString().Split(',');
        for (int i = 0; i < rarities.Length; i++)
        {
            MultipleRarity relicRarity = (MultipleRarity)Enum.Parse(typeof(MultipleRarity), rarities[i]);
            relicList.AddRange(GetRelicRarityList(relicRarity));
        }
        return relicDataList;
    }
}
