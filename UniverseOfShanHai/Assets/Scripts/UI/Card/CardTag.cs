using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardTag : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Image image;
    public TextMeshProUGUI text;

    public void SetCardTag(CardTagType cardTagType)
    {
        if (image != null)
            image.color = UIManager.Instance.GetColorByCardTagType(cardTagType);
        if (sprite != null)
            sprite.color = UIManager.Instance.GetColorByCardTagType(cardTagType);
        if (text != null)
        {
            text.text = GetDescriptionByTag(cardTagType);
        }
    }

    //TODO: Add description for each tag
    public string GetDescriptionByTag(CardTagType cardTagType)
    {
        return cardTagType switch
        {
            CardTagType.Metal => cardTagType.ToString() + ": " + "Keep in hand, will not enter the discard deck each round",
            CardTagType.Wood => cardTagType.ToString() + ": " + "",
            CardTagType.Water => cardTagType.ToString() + ": " + "",
            CardTagType.Fire => cardTagType.ToString() + ": " + "",
            CardTagType.Earth => cardTagType.ToString() + ": " + "",
            CardTagType.Air => cardTagType.ToString() + ": " + "Disappear after each battle",
            CardTagType.Lighting => cardTagType.ToString() + ": " + "Disappear after entering the discard deck after use",
            CardTagType.Darkness => cardTagType.ToString() + ": " + "Disappear after entering the discard deck",
            _ => ""
        };
    }
}
