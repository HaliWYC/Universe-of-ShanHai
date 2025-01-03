using UnityEngine;
using UnityEngine.UI;

public class CharacterStateTip : MonoBehaviour
{
    [SerializeField] private RectTransform stateHolder;
    [SerializeField] private GameObject statePrefab;

    public void SetCharacterStateTip(CharacterDataSO characterData)
    {
        if (stateHolder.childCount > 0)
        {
            for (int i = 0; i < stateHolder.childCount; i++)
            {
                Destroy(stateHolder.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < characterData.buffList.Count; i++)
        {
            var state = Instantiate(statePrefab, stateHolder).GetComponent<CharacterStateSlot>();
            state.SetState(characterData.buffList[i]);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        gameObject.SetActive(true);
    }
}
