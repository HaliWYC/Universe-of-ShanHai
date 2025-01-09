using UnityEngine;
public abstract class RelicData : ScriptableObject
{
    [Header("Info")]
    public int relicID;
    public string relicName;
    public Sprite relicIcon;
    public Rarity relicRarity;
    [TextArea]
    public string description;
    [Header("Trade")]
    public int relicPrice;

    [Header("Combat")]
    public int relicValue;

    [Header("bool")]
    public bool designPrice;
    public bool isAvailable;
    public bool showRelicValue;
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

        RelicEvent.OnPlayerTurnBeginEvent += OnPlayerTurnBegin;
        RelicEvent.OnPlayerTurnEndEvent += OnPlayerTurnEnd;
        RelicEvent.OnEnemyTurnBeginEvent += OnEnemyTurnBegin;
        RelicEvent.OnEnemyTurnEndEvent += OnEnemyTurnEnd;
        RelicEvent.OnBeforeCharacterDeadEvent += OnBeforeCharacterDead;
        RelicEvent.OnAfterCharacterDeadEvent += OnAfterCharacterDead;
        RelicEvent.OnBeforeFatalDamageEvent += OnBeforeFatalDamage;
        RelicEvent.OnAfterFatalDamageEvent += OnAfterFatalDamage;

        RelicEvent.OnGainMoneyEvent += OnGainMoney;
        RelicEvent.OnLoseMoneyEvent += OnLoseMoney;
        OnCreate();
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

        RelicEvent.OnPlayerTurnBeginEvent -= OnPlayerTurnBegin;
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
    public abstract void OnEquip(CharacterBase character);
    public abstract void OnUnequip(CharacterBase character);

    //Combat
    public abstract void OnPlayerAttackBegin();
    public abstract void OnPlayerAttackEnd();
    public abstract void OnEnemyAttackBegin();
    public abstract void OnEnemyAttackEnd();
    public abstract void OnBeforeCharacterDead(CharacterBase character);
    public abstract void OnAfterCharacterDead(CharacterBase character);
    public abstract void OnBeforeFatalDamage(CharacterBase character, int damage);
    public abstract void OnAfterFatalDamage(CharacterBase character, int damage);

    //Card
    public abstract void OnCardDraw();
    public abstract void OnCardPlay(CharacterBase self, CharacterBase target);
    public abstract void OnCardDiscard();

    //Turn
    public abstract void OnPlayerTurnBegin();
    public abstract void OnPlayerTurnEnd();
    public abstract void OnEnemyTurnBegin();
    public abstract void OnEnemyTurnEnd();


    //Money
    public abstract void OnGainMoney();
    public abstract void OnLoseMoney();
}
