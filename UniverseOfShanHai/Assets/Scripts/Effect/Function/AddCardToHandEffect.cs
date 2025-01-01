using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddCardToHandEffect", menuName = "Card Effect/AddCardToHandEffect")]
public class AddCardToHandEffect : Effect
{
    public List<CardDataSO> cardList;
    public override void Init()
    {
        Execute(null);
    }

    public override void UpdateUI()
    {

    }

    public override void Execute(CharacterBase currentTarget)
    {
        if (cardList.Count <= 0) return;
        foreach (var card in cardList)
        {
            PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails("drawCard"));
            CardDeck.Instance.handDeck.Add(CardDeck.Instance.CreateCardFromData(card));
        }
        CardDeck.Instance.SetCardLayout(cardList.Count, true);
    }

    public override void End(CharacterBase currentTarget)
    {

    }


}
