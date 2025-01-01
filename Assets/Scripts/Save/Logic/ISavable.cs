using UnityEngine;

public interface ISavable
{
    string GUID { get; }
    void RegisterSavable()
    {
        SaveLoadManager.Instance.RegisterSavable(this);
    }
    GameSaveData GenerateSaveData();
    void RestoreData(GameSaveData data);
}
