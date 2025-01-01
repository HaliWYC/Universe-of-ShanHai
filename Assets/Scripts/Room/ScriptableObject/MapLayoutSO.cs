using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLayoutSO", menuName = "Map/MapLayoutSO")]
public class MapLayoutSO : ScriptableObject
{
    public string ChapterName;
    public List<MapRoomData> mapRoomDataList = new();
    public List<LinePosition> linePositions = new();
}

[System.Serializable]
public class MapRoomData
{
    public float posX, posY;
    public int row, column;
    public RoomDataSO roomData;
    public RoomState roomState;
    public List<Vector2Int> LinkTo;
}

[System.Serializable]
public class LinePosition
{
    public SerializeVector3 startPos, endPos;
}
