using UnityEngine;
using UnityEngine.UI;

public class CharacterStateTip : MonoBehaviour
{
    [SerializeField] private RectTransform stateHolder;
    [SerializeField] private GameObject statePrefab;

    public void SetCharacterStateTip(CharacterBase target)
    {
        if (stateHolder.childCount > 0)
        {
            for (int i = 0; i < stateHolder.childCount; i++)
            {
                Destroy(stateHolder.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < target.buffList.Count; i++)
        {
            var state = Instantiate(statePrefab, stateHolder).GetComponent<CharacterStateSlot>();
            state.SetState(target.buffList[i]);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        gameObject.SetActive(true);
    }
}
