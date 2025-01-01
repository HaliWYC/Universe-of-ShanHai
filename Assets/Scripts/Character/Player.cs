using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;


public class Player : CharacterBase
{
    public IntVariable playerMana;
    public int maxMana;
    private int manaIncrementCounter = 0;

    public int CurrentMana { get => playerMana.currentValue; set => playerMana.SetValue(value); }

    public void NewTurn()
    {
        manaIncrementCounter++;
        if (manaIncrementCounter == 2)
        {
            playerMana.maxValue++;
            manaIncrementCounter = 0;
        }
        CurrentMana = playerMana.maxValue;
        CardDeck.Instance.NewTurnDrawCards();
    }

    public void UpdateMana(int cost)
    {
        CurrentMana -= cost;
        if (CurrentMana <= 0)
            CurrentMana = 0;
    }

    public void NewRoomEvent()
    {
        manaIncrementCounter = 0;
        playerMana.maxValue = maxMana;
    }
}
