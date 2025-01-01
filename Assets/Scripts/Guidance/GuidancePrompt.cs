using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GuidancePrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Image character;
    public void SetPrompt(GuidanceData guidance)
    {
        promptText.text = guidance.prompt;
        //transform.position = guidance.position;
        if (guidance.showCharacter)
            character.gameObject.SetActive(true);
        else
            character.gameObject.SetActive(false);

    }
}
