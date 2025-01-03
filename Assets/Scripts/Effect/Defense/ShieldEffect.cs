using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
[CreateAssetMenu(fileName = "ShieldEffect", menuName = "Card Effect/ShieldEffect")]
public class ShieldEffect : Effect
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
    public override void UpdateCurrentValue()
    {

    }
    public override void Execute(CharacterBase currentTarget)
    {
        GamePlayPanel.Instance.PopText(currentTarget.transform.position, value, this);
        currentTarget.healthBarController.UpdateBuff();
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
                math.round(currentTarget.characterData.currentDefenseMultiplier /= 1 + value);
            else
                math.round(currentTarget.characterData.currentDefenseMultiplier /= 1 - value);
        }
        if (currentTarget.CompareTag("Enemy"))
            currentTarget.healthBarController.UpdateIntentElement();
    }

    public void SetupRound(CharacterBase currentTarget, bool isPositive)
    {
        foreach (var buff in currentTarget.characterData.buffList)
        {
            if (buff is ShieldEffect effect && effect.Positive == isPositive && effect.value == value && effect.effectDurationType == EffectDurationType.Sustainable)
            {
                effect.round += round;
                if (effect.Positive)
                    currentTarget.vfxController.buff.SetActive(true);
                else
                    currentTarget.vfxController.debuff.SetActive(true);
                currentTarget.healthBarController.UpdateBuff();
                buff.Execute(currentTarget);
                return;
            }
        }
        var Shield = SetUpValue(CreateInstance<ShieldEffect>());
        if (!IsOnceEffect(Shield))
            currentTarget.characterData.buffList.Add(Shield);

        if (isPositive)
            currentTarget.characterData.currentDefenseMultiplier *= 1 + value;
        else
            currentTarget.characterData.currentDefenseMultiplier *= 1 - value;
        Shield.Execute(currentTarget);
    }
    private ShieldEffect SetUpValue(ShieldEffect effect)
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
