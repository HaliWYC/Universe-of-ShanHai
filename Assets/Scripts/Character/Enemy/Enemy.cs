using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionData;
    [Range(1, 8)]
    public int maxActionPerTurn;
    private List<EnemyAction> ActionWaitingList = new();
    public List<EnemyAction> currentTurnAction;

    protected Player player;

    protected override void Awake()
    {
        base.Awake();
        if (templateCharacterData != null)
        {
            characterData = Instantiate(templateCharacterData);
        }
        player = GameManager.Instance.player;
        InitEnemyReward();
    }

    protected override void OnEnable()
    {
        hp.maxValue = characterData.maxHP;
        CurrentHP = MaxHP;
        base.OnEnable();
    }

    public virtual void OnEnemyActionBegin()
    {
        ActionWaitingList.Clear();
        currentTurnAction.Clear();
        float randomPoss = Random.Range(0, 1f);
        foreach (var action in actionData.enemyActions)
        {
            if (action.possibility <= randomPoss)
            {
                ActionWaitingList.Add(action);
            }
        }
        if (ActionWaitingList.Count == 0)
        {
            var Action = Random.Range(0, actionData.enemyActions.Count);
            currentTurnAction.Add(actionData.enemyActions[Action]);
        }
        else
        {
            int randomNum = Random.Range(1, ActionWaitingList.Count);
            for (int i = 0; i < Mathf.Min(randomNum, maxActionPerTurn); i++)
            {
                var randomAction = Random.Range(0, ActionWaitingList.Count);
                currentTurnAction.Add(ActionWaitingList[randomAction]);
            }
        }
    }

    public virtual void OnEnemyTurnBegin()
    {
        StartCoroutine(ProcessDelayAction());
    }
    private IEnumerator ProcessDelayAction()
    {
        for (int i = 0; i < currentTurnAction.Count; i++)
        {
            switch (currentTurnAction[i].effect.targetType)
            {
                case EffectTargetType.Self:
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("skill"))
                    {
                        animator.SetTrigger("skill");
                        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime / animatorSkillSpeed % 1.0f > 0.55f
                        && !animator.IsInTransition(0)
                        && animator.GetCurrentAnimatorStateInfo(0).IsName("skill"));
                    }
                    currentTurnAction[i].effect.Setup(this, this);
                    break;
                case EffectTargetType.Enemy:
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                    {
                        animator.SetTrigger("attack");
                        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.55f
                        && !animator.IsInTransition(0)
                        && animator.GetCurrentAnimatorStateInfo(0).IsName("attack"));
                    }
                    currentTurnAction[i].effect.Setup(this, player);
                    break;
                case EffectTargetType.AllEnemies:
                    break;
            }
        }
    }

    private void InitEnemyReward()
    {
        float modifier = Random.Range(0.8f, 1.2f);
        characterData.Money = System.Convert.ToInt32(characterData.Money * modifier);
    }
}
