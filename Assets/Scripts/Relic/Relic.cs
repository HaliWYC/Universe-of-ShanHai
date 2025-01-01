using UnityEngine;
public abstract class Relic : ScriptableObject
{
    [Header("Info")]
    public int relicID;
    public string relicName;
    public Sprite relicIcon;
    public Rarity relicRarity;
    [TextArea]
    public string description;
    [Header("Trade")]
    public int relicCost;

    [Header("Combat")]
    public int relicValue;

    [Header("bool")]
    public bool isEquipped = false;
    private void OnEnable()
    {
        RelicEvent.OnNewGameEvent += OnNewGame;
        RelicEvent.OnNewRoomEvent += OnNewRoom;
        RelicEvent.OnLoadMapEvent += OnLoadMap;

        RelicEvent.OnCreateEvent += OnCreate;
        RelicEvent.OnEquipEvent += OnEquip;
        RelicEvent.OnUnequipEvent += OnUnequip;

        RelicEvent.OnPlayerAttackBeginEvent += OnPlayerAttackBegin;
        RelicEvent.OnPlayerAttackEndEvent += OnPlayerAttackEnd;
        RelicEvent.OnEnemyAttackBeginEvent += OnEnemyAttackBegin;
        RelicEvent.OnEnemyAttackEndEvent += OnEnemyAttackEnd;

        RelicEvent.OnCardDrawEvent += OnCardDraw;
        RelicEvent.OnCardPlayEvent += OnCardPlay;
        RelicEvent.OnCardDiscardEvent += OnCardDiscard;

        RelicEvent.OnPlayerTurnBeginEvent += OnPlyaerTurnBegin;
        RelicEvent.OnPlayerTurnEndEvent += OnPlayerTurnEnd;
        RelicEvent.OnEnemyTurnBeginEvent += OnEnemyTurnBegin;
        RelicEvent.OnEnemyTurnEndEvent += OnEnemyTurnEnd;

        RelicEvent.OnGainMoneyEvent += OnGainMoney;
        RelicEvent.OnLoseMoneyEvent += OnLoseMoney;
    }

    private void OnDisable()
    {
        RelicEvent.OnNewGameEvent -= OnNewGame;
        RelicEvent.OnNewRoomEvent -= OnNewRoom;
        RelicEvent.OnLoadMapEvent -= OnLoadMap;

        RelicEvent.OnCreateEvent -= OnCreate;
        RelicEvent.OnEquipEvent -= OnEquip;
        RelicEvent.OnUnequipEvent -= OnUnequip;

        RelicEvent.OnPlayerAttackBeginEvent -= OnPlayerAttackBegin;
        RelicEvent.OnPlayerAttackEndEvent -= OnPlayerAttackEnd;
        RelicEvent.OnEnemyAttackBeginEvent -= OnEnemyAttackBegin;
        RelicEvent.OnEnemyAttackEndEvent -= OnEnemyAttackEnd;

        RelicEvent.OnCardDrawEvent -= OnCardDraw;
        RelicEvent.OnCardPlayEvent -= OnCardPlay;
        RelicEvent.OnCardDiscardEvent -= OnCardDiscard;

        RelicEvent.OnPlayerTurnBeginEvent -= OnPlyaerTurnBegin;
        RelicEvent.OnPlayerTurnEndEvent -= OnPlayerTurnEnd;
        RelicEvent.OnEnemyTurnBeginEvent -= OnEnemyTurnBegin;
        RelicEvent.OnEnemyTurnEndEvent -= OnEnemyTurnEnd;

        RelicEvent.OnGainMoneyEvent -= OnGainMoney;
        RelicEvent.OnLoseMoneyEvent -= OnLoseMoney;
    }


    public abstract void OnNewRoom();
    public abstract void OnNewGame();
    public abstract void OnLoadMap();


    //Initialize relic
    public abstract void OnCreate();
    public abstract void OnEquip(Relic relic);
    public abstract void OnUnequip();

    //Combat
    public abstract void OnPlayerAttackBegin();
    public abstract void OnPlayerAttackEnd();
    public abstract void OnEnemyAttackBegin();
    public abstract void OnEnemyAttackEnd();

    //Card
    public abstract void OnCardDraw();
    public abstract void OnCardPlay(CharacterBase self, CharacterBase target);
    public abstract void OnCardDiscard();

    //Turn
    public abstract void OnPlyaerTurnBegin();
    public abstract void OnPlayerTurnEnd();
    public abstract void OnEnemyTurnBegin();
    public abstract void OnEnemyTurnEnd();


    //Money
    public abstract void OnGainMoney();
    public abstract void OnLoseMoney();
}
