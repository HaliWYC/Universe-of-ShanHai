using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour, ISavable
{
    [Header("Map Config")]
    public MapConfigSO mapConfigSO;
    [Header("Map Layout")]
    public MapLayoutSO mapLayout;
    [Header("Prefab")]
    public Room roomPrefab;
    public LineRenderer linePrefab;

    private float screenHeight;
    private float screenWidth;
    private float columnWidth;
    public float border;
    private Vector3 generatePoint;

    private List<Room> rooms = new();
    private List<LineRenderer> lines = new();

    public List<RoomDataSO> roomDataList = new();

    public Dictionary<RoomType, RoomDataSO> roomDataDict = new();

    public string GUID => GetComponent<DataGUID>().guid;

    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
        columnWidth = screenWidth / (mapConfigSO.roomBluePrints.Count + 1);

        foreach (var roomData in roomDataList)
        {
            roomDataDict.Add(roomData.roomType, roomData);
        }
    }

    private void OnEnable()
    {
        if (mapLayout.mapRoomDataList.Count > 0)
        {
            LoadMap();
        }
        else
        {
            CreateMap();
        }
    }
    private void Start()
    {
        ISavable savable = this;
        savable.RegisterSavable();
    }
    public void CreateMap()
    {
        List<Room> previousColumnRooms = new();

        for (int column = 0; column < mapConfigSO.roomBluePrints.Count; column++)
        {
            var roomBluePrint = mapConfigSO.roomBluePrints[column];

            var amount = UnityEngine.Random.Range(roomBluePrint.min, roomBluePrint.max);

            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);
            generatePoint = new Vector3(-screenWidth / 2 + border + columnWidth * column, startHeight, 0);
            var newPosition = generatePoint;
            var roomGapY = screenHeight / (amount + 1);

            List<Room> currentColumnRooms = new();

            for (int i = 0; i < amount; i++)
            {
                if (column == mapConfigSO.roomBluePrints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border * 2;
                }
                else if (column != 0)
                {
                    newPosition.x = generatePoint.x + UnityEngine.Random.Range(-border / 2, border / 2);
                }
                newPosition.y = startHeight - roomGapY * i;


                var room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);
                RoomType newRoomType = GetRandomRoomType(mapConfigSO.roomBluePrints[column].roomType);
                if (column == 0)
                    room.roomState = RoomState.Attainable;
                else
                    room.roomState = RoomState.Locked;
                room.SetupRoom(i, column, GetRoomData(newRoomType));
                rooms.Add(room);
                currentColumnRooms.Add(room);
            }
            if (previousColumnRooms.Count > 0)
            {
                CreateConnections(previousColumnRooms, currentColumnRooms);
            }
            previousColumnRooms = currentColumnRooms;
        }
        SaveMap();
    }


    private void CreateConnections(List<Room> previousColumnRooms, List<Room> currentColumnRooms)
    {
        HashSet<Room> connectedColumnRooms = new();

        foreach (var room in previousColumnRooms)
        {
            var targetRoom = ConnectRandomRoom(room, currentColumnRooms, false);
            connectedColumnRooms.Add(targetRoom);
        }
        foreach (var room in currentColumnRooms)
        {
            if (!connectedColumnRooms.Contains(room))
            {
                ConnectRandomRoom(room, previousColumnRooms, true);
            }
        }
    }

    private Room ConnectRandomRoom(Room room, List<Room> currentColumnRooms, bool check)
    {
        Room targetRoom;
        targetRoom = currentColumnRooms[UnityEngine.Random.Range(0, currentColumnRooms.Count)];

        if (check)
        {
            targetRoom.linkTo.Add(new Vector2Int(room.row, room.column));
        }
        else
        {
            room.linkTo.Add(new Vector2Int(targetRoom.row, targetRoom.column));
        }

        var line = Instantiate(linePrefab, transform);
        line.SetPosition(0, room.transform.position);
        line.SetPosition(1, targetRoom.transform.position);
        lines.Add(line);
        return targetRoom;
    }

    [ContextMenu("Regenerate Map")]
    public void RegenerateMap()
    {
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }

        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }

        rooms.Clear();
        lines.Clear();
        CreateMap();
    }

    private RoomDataSO GetRoomData(RoomType roomType)
    {
        return roomDataDict[roomType];
    }

    private RoomType GetRandomRoomType(RoomType flags)
    {
        string[] options = flags.ToString().Split(",");
        string randomOption = options[UnityEngine.Random.Range(0, options.Length)];
        return (RoomType)Enum.Parse(typeof(RoomType), randomOption);
    }

    private void SaveMap()
    {
        mapLayout.mapRoomDataList.Clear();

        //添加所有房间
        for (int i = 0; i < rooms.Count; i++)
        {
            var room = new MapRoomData
            {
                posX = rooms[i].transform.position.x,
                posY = rooms[i].transform.position.y,
                row = rooms[i].row,
                column = rooms[i].column,
                roomData = rooms[i].roomData,
                roomState = rooms[i].roomState,
                LinkTo = rooms[i].linkTo
            };
            mapLayout.mapRoomDataList.Add(room);
        }

        mapLayout.linePositions.Clear();
        //添加所有连线
        for (int i = 0; i < lines.Count; i++)
        {
            var line = new LinePosition
            {
                startPos = new SerializeVector3(lines[i].GetPosition(0)),
                endPos = new SerializeVector3(lines[i].GetPosition(1))
            };
            mapLayout.linePositions.Add(line);
        }

    }

    private void LoadMap()
    {
        //读取房间
        for (int i = 0; i < mapLayout.mapRoomDataList.Count; i++)
        {
            var newPos = new Vector3(mapLayout.mapRoomDataList[i].posX, mapLayout.mapRoomDataList[i].posY, 0);
            var newRoom = Instantiate(roomPrefab, newPos, Quaternion.identity, transform);
            newRoom.roomState = mapLayout.mapRoomDataList[i].roomState;
            newRoom.SetupRoom(mapLayout.mapRoomDataList[i].row, mapLayout.mapRoomDataList[i].column, mapLayout.mapRoomDataList[i].roomData);
            newRoom.linkTo = mapLayout.mapRoomDataList[i].LinkTo;
            rooms.Add(newRoom);
        }

        //读取连线
        for (int i = 0; i < mapLayout.linePositions.Count; i++)
        {
            var line = Instantiate(linePrefab, transform);
            line.SetPosition(0, mapLayout.linePositions[i].startPos.ToVector3());
            line.SetPosition(1, mapLayout.linePositions[i].endPos.ToVector3());
            lines.Add(line);
        }
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.roomLayoutDict = new Dictionary<string, MapLayoutSO>();
        saveData.roomLayoutDict.Add(mapLayout.ChapterName, mapLayout);
        saveData.chapterName = mapLayout.ChapterName;
        return saveData;
    }

    public void RestoreData(GameSaveData data)
    {
        mapLayout = data.roomLayoutDict[data.chapterName];
    }
}
