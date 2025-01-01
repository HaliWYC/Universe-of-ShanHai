using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfigSO", menuName = "Map/MapConfigSO")]
public class MapConfigSO : ScriptableObject 
{
    public List<RoomBluePrint> roomBluePrints;
}

[System.Serializable]
public class RoomBluePrint
{
    public int min,max;
    public RoomType roomType;
}
