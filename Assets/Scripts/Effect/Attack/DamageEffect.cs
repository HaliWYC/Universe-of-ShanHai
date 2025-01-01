using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effect/DamageEffect")]
public class DamageEffect : Effect
{
    public override void Init()
    {
        if (target == null) return;
        for (int i = 0; i < round; i++)
        {
            switch (targetType)
            {
                case EffectTargetType.Enemy:
                    SetupRound(target);
                    //Debug.Log($"{target.name} received {value*from.characterData.currentAttackMultiplier} damage!");
                    break;
                case EffectTargetType.AllEnemies:
                    foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        SetupRound(enemy.GetComponent<CharacterBase>());
                    }
                    break;
            }
        }
    }
    public override void UpdateUI()
    {
        if (GameManager.Instance.player.characterData != null)
            currentValue = math.round(value * GameManager.Instance.player.characterData.currentAttackMultiplier + GameManager.Instance.player.characterData.nextAttackIncrement);
    }
    public void SetupRound(CharacterBase currentTarget)
    {
        foreach (var buff in currentTarget.buffList)
        {
            if (buff is DamageEffect effect && effect.value == value && effect.effectDurationType == EffectDurationType.Sustainable)
            {
                effect.round += round;
                currentTarget.vfxController.debuff.SetActive(true);
                currentTarget.healthBarController.UpdateBuff();
                return;
            }
        }
        var damage = SetUpValue(CreateInstance<DamageEffect>());
        if (!IsOnceEffect(damage))
            target.buffList.Add(damage);
        damage.Execute(currentTarget);
    }

    private DamageEffect SetUpValue(DamageEffect effect)
    {
        effect.from = from;
        effect.target = target;
        effect.effectIcon = effectIcon;
        effect.effectDurationType = effectDurationType;
        effect.targetType = targetType;
        effect.value = value;
        effect.currentValue = currentValue;
        effect.round = round;
        effect.description = description;
        return effect;
    }
    public override void Execute(CharacterBase currentTarget)
    {
        PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails(from.damageSound));
        // Debug.Log("round" + round);
        // Debug.Log("Value: " + value);
        // Debug.Log("Multiplier: " + from.characterData.currentAttackMultiplier);
        // Debug.Log("Increment: " + from.characterData.nextAttackIncrement);
        // Debug.Log("Final Damage: " + (int)math.round(value * from.characterData.currentAttackMultiplier + from.characterData.nextAttackIncrement));
        currentTarget.TakeDamage((int)math.round(value * from.characterData.currentAttackMultiplier + from.characterData.nextAttackIncrement));
        GamePlayPanel.Instance.PopText(currentTarget.transform.position, (int)math.round(value * from.characterData.currentAttackMultiplier + from.characterData.nextAttackIncrement), this);
    }
    public override void End(CharacterBase currentTarget)
    {

    }
}
