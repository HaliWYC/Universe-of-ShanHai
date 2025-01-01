using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour
{
    public int row;
    public int column;
    private SpriteRenderer spriteRenderer;
    public RoomDataSO roomData;
    public RoomState roomState;

    public List<Vector2Int> linkTo = new();

    [Header("Broadcast")]
    public ObjectEventSO loadRoomEvent;

    public void Awake() 
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnMouseDown() 
    {
        //Debug.Log("Room clicked"+roomData.roomType);
        if(roomState==RoomState.Attainable)
        loadRoomEvent.RaiseEvent(this,this);
    }

    /// <summary>
    /// 外部创造房间时调用配置房间
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="roomData"></param>
    public void SetupRoom(int row, int column,RoomDataSO roomData)
    {
        this.row = row;
        this.column = column;
        this.roomData = roomData;
        spriteRenderer.sprite = roomData.roomIcon;
        spriteRenderer.color = roomState switch
        {
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1f),
            RoomState.Visited => new Color(0.5f, 0.8f, 0.5f, 0.5f),
            RoomState.Attainable => Color.white,
            _ => throw new System.NotImplementedException()
        };
    }
}
