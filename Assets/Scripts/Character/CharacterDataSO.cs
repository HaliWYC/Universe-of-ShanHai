using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Character/CharacterDataSO")]
public class CharacterDataSO : ScriptableObject
{
    [Header("Basic Property")]
    public int maxHP;
    public float baseAttackMultiplier = 1f;
    public float currentAttackMultiplier;
    public float nextAttackIncrement;
    public float baseDefenseMultiplier = 1f;
    public float currentDefenseMultiplier;
    public float nextDefenseIncrement;

    public List<Effect> buffList;

    public List<Relic> relics;

    [Header("Currency")]
    public int Money;

    public void RefreshProperty()
    {
        currentAttackMultiplier = baseAttackMultiplier;
        currentDefenseMultiplier = baseDefenseMultiplier;
    }
}
