using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "StrengthEffect", menuName = "Card Effect/StrengthEffect")]
public class StrengthEffect : Effect
{
    public bool Positive;

    public override void Init()
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                SetupRound(from, true);
                break;
            case EffectTargetType.Enemy:
                SetupRound(target, false);
                break;
            case EffectTargetType.AllEnemies:
                break;
        }
    }
    public override void UpdateUI()
    {

    }

    public override void Execute(CharacterBase currentTarget)
    {
        if (Positive)
        {
            currentTarget.vfxController.buff.SetActive(true);
            PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails(from.buffSound));
        }
        else
        {
            currentTarget.vfxController.debuff.SetActive(true);
            PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails(from.debuffSound));
        }
        if (currentTarget.CompareTag("Enemy"))
            currentTarget.healthBarController.UpdateIntentElement();
        currentTarget.healthBarController.UpdateBuff();
        GamePlayPanel.Instance.PopText(currentTarget.transform.position, value, this);
    }

    public override void End(CharacterBase currentTarget)
    {
        if (currentTarget != null && round <= 0)
        {
            if (Positive)
                math.round(currentTarget.characterData.currentAttackMultiplier /= 1 + value);
            else
                math.round(currentTarget.characterData.currentAttackMultiplier /= 1 - value);
        }
        if (currentTarget.CompareTag("Enemy"))
            currentTarget.healthBarController.UpdateIntentElement();
    }

    public void SetupRound(CharacterBase currentTarget, bool isPositive)
    {
        foreach (var buff in currentTarget.characterData.buffList)
        {
            if (buff is StrengthEffect effect && effect.Positive == isPositive && effect.value == value && effect.effectDurationType == EffectDurationType.Sustainable)
            {
                effect.round += round;
                if (effect.Positive)
                    currentTarget.vfxController.buff.SetActive(true);
                else
                    currentTarget.vfxController.debuff.SetActive(true);
                buff.Execute(currentTarget);
                return;
            }
        }
        var Strength = SetUpValue(CreateInstance<StrengthEffect>());

        if (!IsOnceEffect(Strength))
            currentTarget.characterData.buffList.Add(Strength);
        if (isPositive)
            currentTarget.characterData.currentAttackMultiplier *= 1 + value;
        else
            currentTarget.characterData.currentAttackMultiplier *= 1 - value;
        Strength.Execute(currentTarget);
    }
    private StrengthEffect SetUpValue(StrengthEffect effect)
    {
        effect.from = from;
        effect.target = target;
        effect.effectIcon = effectIcon;
        effect.effectDurationType = effectDurationType;
        effect.value = value;
        effect.round = round;
        effect.Positive = Positive;
        effect.description = description;
        return effect;
    }
}

