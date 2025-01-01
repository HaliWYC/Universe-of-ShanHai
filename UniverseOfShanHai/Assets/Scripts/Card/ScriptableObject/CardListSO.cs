using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardListSO", menuName = "Card/CardListSO")]
public class CardListSO : ScriptableObject
{
    public Rarity cardListRarity;
    public List<CardDataSO> cardDataList;
}
