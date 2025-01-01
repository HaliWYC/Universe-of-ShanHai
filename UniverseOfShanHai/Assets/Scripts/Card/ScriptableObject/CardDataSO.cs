using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    public int cardID;
    public string cardName;
    public Sprite cardImage;
    public int cardCost;
    //public int cardPrice;
    public CardType cardType;
    public Rarity cardRarity;
    public List<CardTagType> cardTagList;

    [TextArea]
    public string cardDescription;

    public List<Effect> effectList;
}
