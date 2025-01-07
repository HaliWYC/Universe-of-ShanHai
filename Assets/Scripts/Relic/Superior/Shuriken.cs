using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Shuriken", menuName = "Relic/Superior/Shuriken")]
public class Shuriken : RelicData
{
    private CharacterBase lastTarget;

    public override void OnAfterCharacterDead(CharacterBase character)
    {

    }

    public override void OnAfterFatalDamage(CharacterBase character, int damage)
    {

    }

    public override void OnBeforeCharacterDead(CharacterBase character)
    {

    }

    public override void OnBeforeFatalDamage(CharacterBase character, int damage)
    {

    }

    public override void OnCardDiscard()
    {

    }

    public override void OnCardDraw()
    {

    }

    public override void OnCardPlay(CharacterBase from, CharacterBase target)
    {
        if (!from.characterData.relics.Contains(this)) return;
        if (target != null)
        {
            lastTarget = target;
        }
        relicValue++;
        if (relicValue == 4)
        {
            relicValue = 0;
            if (lastTarget != null)
            {
                PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails("Shuriken"));
                lastTarget.TakeDamage(5);
                GamePlayPanel.Instance.PopText(lastTarget.transform.position, 5, CreateInstance<DamageEffect>());
            }
        }
        UIPanel.Instance.UpdateRelicsValue();
    }

    public override void OnCreate()
    {

    }

    public override void OnEnemyAttackBegin()
    {

    }

    public override void OnEnemyAttackEnd()
    {

    }

    public override void OnEnemyTurnBegin()
    {

    }

    public override void OnEnemyTurnEnd()
    {

    }

    public override void OnEquip(RelicData relic)
    {

    }

    public override void OnGainMoney()
    {

    }

    public override void OnLoadMap()
    {
        relicValue = 0;
        UIPanel.Instance.UpdateRelicsValue();
    }

    public override void OnLoseMoney()
    {

    }

    public override void OnNewGame()
    {

    }

    public override void OnNewRoom()
    {
        relicValue = 0;
        UIPanel.Instance.UpdateRelicsValue();
    }

    public override void OnPlayerAttackBegin()
    {

    }

    public override void OnPlayerAttackEnd()
    {

    }

    public override void OnPlayerTurnEnd()
    {

    }

    public override void OnPlayerTurnBegin()
    {

    }

    public override void OnUnequip()
    {

    }
}
