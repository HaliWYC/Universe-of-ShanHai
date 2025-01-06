using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Shuriken", menuName = "Relic/Card/Shuriken")]
public class Shuriken : RelicData
{
    private CharacterBase lastTarget;
    public override void OnCardDiscard()
    {

    }

    public override void OnCardDraw()
    {

    }

    public override void OnCardPlay(CharacterBase from, CharacterBase target)
    {
        if (!isEquipped) return;
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
                GamePlayPanel.Instance.PopText(target.transform.position, 5, CreateInstance<DamageEffect>());
                lastTarget.healthBarController.UpdateHealthBar();
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
        if (relic == this)
        {
            isEquipped = true;
        }
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

    public override void OnPlyaerTurnBegin()
    {

    }

    public override void OnUnequip()
    {

    }
}
