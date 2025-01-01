using System;
using UnityEngine;

public static class RelicEvent
{
    //Game
    public static event Action OnNewGameEvent;
    public static void OnNewGame()
    {
        OnNewGameEvent?.Invoke();
    }
    public static event Action OnNewRoomEvent;
    public static void OnNewRoom()
    {
        OnNewRoomEvent?.Invoke();
    }

    public static event Action OnLoadMapEvent;
    public static void OnLoadMap()
    {
        OnLoadMapEvent?.Invoke();
    }

    //Relic
    public static event Action OnCreateEvent;
    public static void OnCreate()
    {
        OnCreateEvent?.Invoke();
    }
    public static event Action<Relic> OnEquipEvent;
    public static void OnEquip(Relic relic)
    {
        OnEquipEvent?.Invoke(relic);
    }
    public static event Action OnUnequipEvent;
    public static void OnUnequip()
    {
        OnUnequipEvent?.Invoke();
    }

    //Combat
    public static event Action OnPlayerAttackBeginEvent;
    public static void OnPlayerAttackBegin()
    {
        OnPlayerAttackBeginEvent?.Invoke();
    }
    public static event Action OnPlayerAttackEndEvent;
    public static void OnPlayerAttackEnd()
    {
        OnPlayerAttackEndEvent?.Invoke();
    }
    public static event Action OnEnemyAttackBeginEvent;
    public static void OnEnemyAttackBegin()
    {
        OnEnemyAttackBeginEvent?.Invoke();
    }
    public static event Action OnEnemyAttackEndEvent;
    public static void OnEnemyAttackEnd()
    {
        OnEnemyAttackEndEvent?.Invoke();
    }

    //Card
    public static event Action OnCardDrawEvent;
    public static void OnCardDraw()
    {
        OnCardDrawEvent?.Invoke();
    }
    public static event Action<CharacterBase, CharacterBase> OnCardPlayEvent;
    public static void OnCardPlay(CharacterBase self, CharacterBase target)
    {
        OnCardPlayEvent?.Invoke(self, target);
    }
    public static event Action OnCardDiscardEvent;
    public static void OnCardDiscard()
    {
        OnCardDiscardEvent?.Invoke();
    }

    //Turn
    public static event Action OnPlayerTurnBeginEvent;
    public static void OnPlyaerTurnBegin()
    {
        OnPlayerTurnBeginEvent?.Invoke();
    }
    public static event Action OnPlayerTurnEndEvent;
    public static void OnPlayerTurnEnd()
    {
        OnPlayerTurnEndEvent?.Invoke();
    }
    public static event Action OnEnemyTurnBeginEvent;
    public static void OnEnemyTurnBegin()
    {
        OnEnemyTurnBeginEvent?.Invoke();
    }
    public static event Action OnEnemyTurnEndEvent;
    public static void OnEnemyTurnEnd()
    {
        OnEnemyTurnEndEvent?.Invoke();
    }


    //Money
    public static event Action OnGainMoneyEvent;
    public static void OnGainMoney()
    {
        OnGainMoneyEvent?.Invoke();
    }
    public static event Action OnLoseMoneyEvent;
    public static void OnLoseMoney()
    {
        OnLoseMoneyEvent?.Invoke();
    }
}
