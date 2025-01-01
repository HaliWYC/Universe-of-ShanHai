using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyActionDataSO", menuName = "Enemy/EnemyActionDataSO", order = 0)]
public class EnemyActionDataSO : ScriptableObject 
{
    public List<EnemyAction> enemyActions;
}

[System.Serializable]
public struct EnemyAction
{
    public Effect effect;
    public float possibility;
}
