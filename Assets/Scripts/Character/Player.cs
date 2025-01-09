public class Player : CharacterBase, ISavable
{
    public IntVariable playerMana;
    private int manaIncrementCounter = 0;
    public int CurrentMana { get => playerMana.currentValue; set => playerMana.SetValue(value); }
    public string GUID => GetComponent<DataGUID>().guid;

    private void Start()
    {
        ISavable savable = this;
        savable.RegisterSavable();
    }

    public void NewTurn()
    {
        manaIncrementCounter++;
        if (manaIncrementCounter == 2)
        {
            playerMana.maxValue++;
            manaIncrementCounter = 0;
        }
        CurrentMana = playerMana.maxValue;
        CardDeck.Instance.NewTurnDrawCards();
    }

    public void UpdateMana(int cost)
    {
        CurrentMana -= cost;
        if (CurrentMana <= 0)
            CurrentMana = 0;
    }

    public void NewRoomEvent()
    {
        manaIncrementCounter = 0;
        playerMana.maxValue = characterData.mana;
        RefreshProperty();
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.playerMana = playerMana.maxValue;
        saveData.playerCurrentHealth = hp.currentValue;
        saveData.currentPlayerData = characterData;
        return saveData;
    }

    public void RestoreData(GameSaveData data)
    {
        playerMana.maxValue = data.playerMana;
        hp.currentValue = data.playerCurrentHealth;
        characterData = data.currentPlayerData;
    }
}
