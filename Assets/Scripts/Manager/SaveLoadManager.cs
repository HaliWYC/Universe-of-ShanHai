using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private List<ISavable> savableList = new List<ISavable>();

    public List<DataSlot> dataSlots = new List<DataSlot>(new DataSlot[3]);

    [SerializeField] private string jsonFolder;
    private int currentIndex;

    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE DATA/";
    }
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.I))
    //     {
    //         Save(0);
    //     }
    //     if (Input.GetKeyDown(KeyCode.O))
    //     {
    //         Load(0);
    //     }
    // }
    public void RegisterSavable(ISavable savable)
    {
        if (!savableList.Contains(savable))
            savableList.Add(savable);
    }

    private void Save(int index)
    {
        DataSlot dataSlot = new DataSlot();
        foreach (ISavable savable in savableList)
        {
            dataSlot.dataDict.Add(savable.GUID, savable.GenerateSaveData());
        }
        dataSlots[index] = dataSlot;

        var resultPath = jsonFolder + "data" + index + ".json";

        var jsonData = JsonConvert.SerializeObject(dataSlots[index], Formatting.Indented);

        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);

        }
        File.WriteAllText(resultPath, jsonData);
    }

    private void Load(int index)
    {
        currentIndex = index;
        var resultPath = jsonFolder + "data" + index + ".json";
        var stringData = File.ReadAllText(resultPath);
        var jsonData = JsonConvert.DeserializeObject<DataSlot>(stringData);
        foreach (ISavable savable in savableList)
        {
            if (jsonData.dataDict.ContainsKey(savable.GUID))
            {
                savable.RestoreData(jsonData.dataDict[savable.GUID]);
            }
        }
    }


}
