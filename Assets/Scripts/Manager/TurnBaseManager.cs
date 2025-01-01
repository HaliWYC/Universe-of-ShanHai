using UnityEngine;

public class TurnBaseManager : MonoBehaviour
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
    }
    public void EnemyTurnBegin()
    {
        isEnemyTurn = true;
        enemyTurnBegin.RaiseEvent(null, this);
    }
    public void EnemyTurnEnd()
    {
        isEnemyTurn = false;
        enemyTurnEnd.RaiseEvent(null, this);
    }

    public void AfterLoadRoomEvent(object data)
    {
        Room currentRoom = data as Room;

        switch (currentRoom.roomData.roomType)
        {
            case RoomType.Guidance:
                GameManager.Instance.player.gameObject.SetActive(true);
                GameStart();
                if (!GuidanceManager.Instance.isEnemyChecked)
                    StartCoroutine(GuidanceManager.Instance.EnemyGuidance(5));
                break;
            case RoomType.MiniorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                GameManager.Instance.player.gameObject.SetActive(true);
                GameStart();
                break;
            case RoomType.Shop:
                GameManager.Instance.player.gameObject.SetActive(false);
                if (!GuidanceManager.Instance.isShopChecked)
                    StartCoroutine(GuidanceManager.Instance.ShopGuidance(8));
                break;
            case RoomType.Treasure:
                GameManager.Instance.player.gameObject.SetActive(false);
                if (!GuidanceManager.Instance.isTreasureChecked)
                    StartCoroutine(GuidanceManager.Instance.TreasureGuidance(5));
                break;
            case RoomType.RestRoom:
                GameManager.Instance.player.gameObject.SetActive(true);
                if (!GuidanceManager.Instance.isRestRoomChecked)
                    StartCoroutine(GuidanceManager.Instance.RestRoomGuidance(5));
                GameManager.Instance.player.GetComponent<PlayerAnimation>().SetSleepAnimation();
                break;
        }
        RelicEvent.OnNewRoom();
    }

    public void OnStopTurnSystem(object obj)
    {
        battleEnd = true;
        GameManager.Instance.player.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        Player player = GameManager.Instance.player;
        player.hp.maxValue = player.templateCharacterData.maxHP;
        player.CurrentHP = player.MaxHP;
        player.CurrentMana = player.playerMana.maxValue;
        player.isDead = false;
    }
}
