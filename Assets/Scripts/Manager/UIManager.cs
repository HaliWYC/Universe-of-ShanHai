using UnityEngine;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [Header("Panel")]
    public GameObject gamePlayPanel;
    public GameObject gameWinPanel;
    public GameObject gameOverPanel;
    public GameObject pickCardPanel;
    public GameObject restRoomPanel;

    [Header("Color")]
    public Color Normal;
    public Color Superior;
    public Color Elite;
    public Color Epic;
    public Color Legendary;
    public Color Mythical;

    [Header("TagColor")]

    public Color Metal;
    public Color Wood;
    public Color Water;
    public Color Fire;
    public Color Earth;
    public Color Air;
    public Color Lighting;
    public Color Darkness;

    [Header("Sprite")]
    public Sprite NormalSprite;
    public Sprite SuperiorSprite;
    public Sprite EliteSprite;
    public Sprite EpicSprite;
    public Sprite LegendarySprite;
    public Sprite MythicalSprite;
    public Sprite AttackSprite;
    public Sprite DefenseSprite;
    public Sprite AbilitiesSprite;

    public void OnLoadRoomEvent(object data)
    {
        Room currentRoom = data as Room;

        switch (currentRoom.roomData.roomType)
        {
            case RoomType.Guidance:
            case RoomType.MiniorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                gamePlayPanel.SetActive(true);
                break;
            case RoomType.Shop:
                break;
            case RoomType.Treasure:
                break;
            case RoomType.RestRoom:
                restRoomPanel.SetActive(true);
                break;
        }
        UIPanel.Instance.UpdateCurrencyText();
    }

    public void HideAllPanels()
    {
        gamePlayPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        pickCardPanel.SetActive(false);
        restRoomPanel.SetActive(false);
        UIPanel.Instance.UpdateCurrencyText();
    }

    public void OnGameWinEvent()
    {
        gamePlayPanel.SetActive(false);
        gameWinPanel.SetActive(true);
    }

    public void OnGameOverEvent()
    {
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void OnPickCardEvent()
    {
        pickCardPanel.SetActive(true);
    }

    public void OnFinishPickCardEvent()
    {
        pickCardPanel.SetActive(false);
    }


    public Color GetColorByCardRarity(Rarity cardRarity)
    {
        return cardRarity switch
        {
            Rarity.Normal => Normal,
            Rarity.Superior => Superior,
            Rarity.Elite => Elite,
            Rarity.Epic => Epic,
            Rarity.Legendary => Legendary,
            Rarity.Mythical => Mythical,
            _ => Normal,
        };
    }

    public Color GetColorByCardTagType(CardTagType cardTag)
    {
        return cardTag switch
        {
            CardTagType.Metal => Metal,
            CardTagType.Wood => Wood,
            CardTagType.Water => Water,
            CardTagType.Fire => Fire,
            CardTagType.Earth => Earth,
            CardTagType.Air => Air,
            CardTagType.Lighting => Lighting,
            _ => Darkness
        };
    }

    public Sprite GetSpriteByCardRarity(Rarity cardRarity)
    {
        return cardRarity switch
        {
            Rarity.Normal => NormalSprite,
            Rarity.Superior => SuperiorSprite,
            Rarity.Elite => EliteSprite,
            Rarity.Epic => EpicSprite,
            Rarity.Legendary => LegendarySprite,
            Rarity.Mythical => MythicalSprite,
            _ => NormalSprite,
        };
    }

    public Sprite GetSpriteByCardType(CardType cardType)
    {
        return cardType switch
        {
            CardType.Attack => AttackSprite,
            CardType.Defense => DefenseSprite,
            CardType.Abilities => AbilitiesSprite,
            _ => null,
        };
    }
}
