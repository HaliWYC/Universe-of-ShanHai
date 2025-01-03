using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStateSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text turn;
    [SerializeField] private TextMeshProUGUI description;

    public void SetState(Effect effect)
    {
        icon.sprite = effect.effectIcon;
        turn.text = effect.round.ToString();
        UpdateCardDescription(effect);
    }

    private string GetDescription(Effect effect, string description)
    {
        return effect.round > 1 ? description + ", remain " + effect.round.ToString() + " turns" : description + ", remain " + effect.round.ToString() + " turn";
    }
    public void UpdateCardDescription(Effect effect)
    {
        effect.UpdateCurrentValue();
        string[] strings = effect.description.Split("#");
        string returnString = "";
        for (int i = 0; i < strings.Length; i++)
        {
            if (i % 2 == 1)
            {
                strings[i] = effect.GetCurrentValue(effect).ToString();
            }
            returnString += strings[i];
        }
        description.text = GetDescription(effect, returnString);
    }
}
