using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public MapLayoutSO mapLayout;
    public Player player;

    public PickCardPanel pickCardPanel;

    public List<Enemy> aliveEnemyList;

    [Header("Event")]
    public ObjectEventSO gameWinEvent;
    public ObjectEventSO gameOverEvent;
    public ObjectEventSO newGameEvent;

    protected override void Awake()
    {
        base.Awake();
        if (player.templateCharacterData != null)
        {
            player.characterData = Instantiate(player.templateCharacterData);
        }
    }

    /// <summary>
    /// 更新房间状态事件监听函数
    /// </summary>
    /// <param name="value"></param>
    public void UpdateMapLayoutData(object value)
    {
        if (mapLayout.mapRoomDataList.Count == 0) return;
        var roomVector = (Vector2Int)value;
        var currentRoom = mapLayout.mapRoomDataList.Find(r => r.row == roomVector.x && r.column == roomVector.y);
        currentRoom.roomState = RoomState.Visited;
        //更新相邻房间数据
        var SameColumnRooms = mapLayout.mapRoomDataList.FindAll(r => r.column == roomVector.y);
        foreach (var room in SameColumnRooms)
        {
            if (room.row != roomVector.x)
                room.roomState = RoomState.Locked;
        }

        foreach (var link in currentRoom.LinkTo)
        {
            var linkToRoom = mapLayout.mapRoomDataList.Find(r => r.row == link.x && r.column == link.y);
            linkToRoom.roomState = RoomState.Attainable;
        }
        aliveEnemyList.Clear();
    }

    public void OnCharacterDeadEvent(object character)
    {
        if (character is Player)
        {
            StartCoroutine(EventDelayAction(gameOverEvent));
        }

        if (character is Boss)
        {
            StartCoroutine(EventDelayAction(gameOverEvent));
        }
        else if (character is Enemy)
        {
            Enemy enemy = character as Enemy;
            aliveEnemyList.Remove(enemy);
            player.characterData.Money += enemy.characterData.Money;
            UIPanel.Instance.currencyText.text = player.characterData.Money.ToString();
            if (aliveEnemyList.Count == 0)
            {
                StartCoroutine(EventDelayAction(gameWinEvent));
            }
        }
    }

    private IEnumerator EventDelayAction(ObjectEventSO eventSO)
    {
        yield return new WaitForSeconds(1.5f);
        eventSO.RaiseEvent(null, this);
    }

    public void OnRoomLoadEvent()
    {
        player.NewRoomEvent();
        var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        aliveEnemyList.AddRange(enemies);
    }

    public void OnNewGameEvent()
    {
        mapLayout.mapRoomDataList.Clear();
        mapLayout.linePositions.Clear();
        newGameEvent.RaiseEvent(null, this);
        aliveEnemyList.Clear();
        //FIXME: 初始化玩家数据
        player.templateCharacterData.Money = 0;
        UIPanel.Instance.currencyText.text = player.templateCharacterData.Money.ToString();
    }

    public void InitRewardList(object obj)
    {
        pickCardPanel.rewardList.Clear();
        var Room = obj as Room;
        if (Room.roomData.roomType == RoomType.Treasure)
            pickCardPanel.rewardList = new List<CardDataSO>(CardManager.Instance.cardDataList);
        else
        {
            if (Room.roomData.rewardList.Count < 3)
            {
                pickCardPanel.rewardList = CardManager.Instance.GetCardListByMultipleRarity(Room.roomData.rarity);
            }
            else
            {
                pickCardPanel.rewardList = new List<CardDataSO>(Room.roomData.rewardList);
            }
        }

    }
}
