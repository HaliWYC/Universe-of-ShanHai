using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RelicManager : Singleton<RelicManager>
{
    [SerializeField] private List<RelicData> relicDataList;
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
        InitializeRelicDataList();
    }

    public void SetAlwaysAvailableRelic()
    {
        for (int i = 0; i < alwaysAvailableRelicList.Count; i++)
        {
            alwaysAvailableRelicList[i].isAvailable = true;
            if (!relicDataList.Contains(alwaysAvailableRelicList[i]))
                relicDataList.Add(alwaysAvailableRelicList[i]);
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
            switch (availableRelicList[i].relicRarity)
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
            SetAlwaysAvailableRelic();
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
            GameManager.Instance.player.characterData.relics.Add(Instantiate(relic));
            foreach (var item in GameManager.Instance.player.characterData.relics)
            {
                if (item == relic)
                    Debug.Log("1");
            }
            relic.OnEquip(GameManager.Instance.player);
            UIPanel.Instance.InitRelics();
        }
    }

    public int GetRelicPriceByRarity(RelicData relic)
    {
        if (relic.designPrice)
            return relic.relicPrice;
        var price = relic.relicRarity switch
        {
            Rarity.Normal => 100,
            Rarity.Superior => 200,
            Rarity.Elite => 350,
            Rarity.Epic => 500,
            Rarity.Legendary => 800,
            Rarity.Mythical => 1200,
            _ => 1000,
        };
        return Convert.ToInt32(math.round((1 + UnityEngine.Random.Range(-0.1f, 0.1f)) * price));
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
        return relicList;
    }
}
