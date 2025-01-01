using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public string chapterName;
    public CharacterDataSO currentPlayerData;
    public int playerCurrentHealth;
    public int playerMana;
    /// <summary>
    /// The room layout of the current chapter.
    /// </summary>
    public Dictionary<string, MapLayoutSO> roomLayoutDict;
    /// <summary>
    /// The current cards in the player's hand.
    /// </summary>
    public CardLibrarySO playerCurrentCardLibraries;
    public List<bool> guidanceCheckList;
}
