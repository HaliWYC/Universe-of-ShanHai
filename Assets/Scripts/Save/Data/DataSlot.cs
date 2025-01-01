using System.Collections.Generic;
using UnityEngine;

public class DataSlot
{
    /// <summary>
    /// string is GUID
    /// </summary>
    public Dictionary<string, GameSaveData> dataDict = new Dictionary<string, GameSaveData>();
}
