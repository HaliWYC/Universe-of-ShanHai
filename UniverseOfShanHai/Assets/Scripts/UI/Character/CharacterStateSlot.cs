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
        description.text = GetDescription(effect);
    }

    private string GetDescription(Effect effect)
    {

        return effect.round > 1 ? effect.description + ", remain " + effect.round.ToString() + " turns" : effect.description + ", remain " + effect.round.ToString() + " turn";
    }

    //TODO:Effect Value Change
}
