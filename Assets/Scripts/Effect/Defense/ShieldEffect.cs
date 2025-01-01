using UnityEngine;
using System.Collections.Generic;
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
    public override void UpdateUI()
    {

    }
    public override void Execute(CharacterBase currentTarget)
    {
        if (Positive)
            currentTarget.vfxController.buff.SetActive(true);
        else
            currentTarget.vfxController.debuff.SetActive(true);
        if (currentTarget.CompareTag("Enemy"))
            currentTarget.healthBarController.UpdateIntentElement();
        GamePlayPanel.Instance.PopText(currentTarget.transform.position, value, this);
        currentTarget.healthBarController.UpdateBuff();
    }

    public override void End(CharacterBase currentTarget)
    {

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
                return;
            }
        }
        var Shield = SetUpValue(CreateInstance<ShieldEffect>());
        if (!IsOnceEffect(Shield))
            currentTarget.characterData.buffList.Add(Shield);

        if (isPositive)
            currentTarget.characterData.currentDefenseMultiplier *= 1 + value;
        else
            currentTarget.characterData.currentDefenseMultiplier /= 1 + value;
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
