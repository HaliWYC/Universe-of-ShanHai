using UnityEngine;

public class TurnBaseManager : Singleton<TurnBaseManager>
{
    private bool isPlayerTurn = false;
    private bool isEnemyTurn = false;
    public bool battleEnd = true;

    private float timeCounter;

    public float enemyTimeDuration;
    public float playerTimeDuration;

    [Header("Event")]
    public ObjectEventSO playerTurnBegin;
    public ObjectEventSO enemyTurnBegin;
    public ObjectEventSO enemyTurnEnd;
    private Player Player;


    private void Start()
    {
        Player = GameManager.Instance.player;
    }

    private void Update()
    {
        if (battleEnd)
            return;
        if (isEnemyTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= enemyTimeDuration)
            {
                timeCounter = 0;
                //Enemy Turn End
                EnemyTurnEnd();
                isPlayerTurn = true;
            }
        }
        if (isPlayerTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= playerTimeDuration)
            {
                timeCounter = 0;
                PlayerTurnBegin();
                isPlayerTurn = false;
            }
        }
    }

    [ContextMenu("Start Game")]
    public void GameStart()
    {
        isPlayerTurn = true;
        isEnemyTurn = false;
        battleEnd = false;
        timeCounter = 0;
    }


    public void PlayerTurnBegin()
    {
        playerTurnBegin.RaiseEvent(null, this);
        RelicEvent.OnPlayerTurnBegin();
    }
    public void EnemyTurnBegin()
    {
        isEnemyTurn = true;
        enemyTurnBegin.RaiseEvent(null, this);
        RelicEvent.OnEnemyTurnBegin();
    }
    public void EnemyTurnEnd()
    {
        isEnemyTurn = false;
        enemyTurnEnd.RaiseEvent(null, this);
        RelicEvent.OnEnemyTurnEnd();
    }

    public void AfterLoadRoomEvent(object data)
    {
        Room currentRoom = data as Room;
        Player.hp.maxValue = Player.characterData.maxHP;
        switch (currentRoom.roomData.roomType)
        {
            case RoomType.Guidance:
                Player.gameObject.SetActive(true);
                GameStart();
                if (!GuidanceManager.Instance.guidanceCheckList[3])
                    StartCoroutine(GuidanceManager.Instance.EnemyGuidance(5));
                break;
            case RoomType.MiniorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                Player.gameObject.SetActive(true);
                GameStart();
                GuidanceManager.Instance.guidanceList[18].SetActive(false);
                break;
            case RoomType.Shop:
                Player.gameObject.SetActive(false);
                if (!GuidanceManager.Instance.guidanceCheckList[20])
                    StartCoroutine(GuidanceManager.Instance.ShopGuidance(8));
                GuidanceManager.Instance.guidanceList[18].SetActive(false);
                break;
            case RoomType.Treasure:
                Player.gameObject.SetActive(false);
                if (!GuidanceManager.Instance.guidanceCheckList[19])
                    StartCoroutine(GuidanceManager.Instance.TreasureGuidance(5));
                GuidanceManager.Instance.guidanceList[18].SetActive(false);
                break;
            case RoomType.RestRoom:
                Player.gameObject.SetActive(true);
                if (!GuidanceManager.Instance.guidanceCheckList[21])
                    StartCoroutine(GuidanceManager.Instance.RestRoomGuidance(5));
                Player.GetComponent<PlayerAnimation>().SetSleepAnimation();
                GuidanceManager.Instance.guidanceList[18].SetActive(false);
                break;
        }
        RelicEvent.OnNewRoom();
    }

    public void OnStopTurnSystem(object obj)
    {
        battleEnd = true;
        Player.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        Player.hp.maxValue = Player.templateCharacterData.maxHP;
        Player.CurrentHP = Player.MaxHP;
        Player.CurrentMana = Player.playerMana.maxValue;
        Player.isDead = false;
    }
}
