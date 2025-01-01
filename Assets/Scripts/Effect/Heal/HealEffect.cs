using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Card Effect/HealEffect")]
public class HealEffect : Effect
{
    public override void Init()
    {
        if (targetType == EffectTargetType.Self)
        {
            SetupRound(from);
        }

        if (targetType == EffectTargetType.Enemy)
        {
            SetupRound(target);
        }
    }
    public override void UpdateUI()
    {

    }

    public void SetupRound(CharacterBase currentTarget)
    {
        foreach (var buff in currentTarget.buffList)
        {
            if (buff is HealEffect effect && effect.value == value && effect.effectDurationType == EffectDurationType.Sustainable)
            {
                effect.round += round;
                currentTarget.vfxController.buff.SetActive(true);
                currentTarget.healthBarController.UpdateBuff();
                return;
            }
        }
        var Heal = SetUpValue(CreateInstance<HealEffect>());
        if (!IsOnceEffect(Heal))
            currentTarget.buffList.Add(Heal);
        Heal.Execute(currentTarget);
    }

    private HealEffect SetUpValue(HealEffect effect)
    {
        effect.from = from;
        effect.target = target;
        effect.effectIcon = effectIcon;
        effect.effectDurationType = effectDurationType;
        effect.value = value;
        effect.round = round;
        effect.description = description;
        return effect;
    }
    public override void Execute(CharacterBase currentTarget)
    {
        PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails(from.healSound));
        currentTarget.CurrentHP = currentTarget.CurrentHP + (int)value > currentTarget.MaxHP ? currentTarget.MaxHP : currentTarget.CurrentHP + (int)value;
        GamePlayPanel.Instance.PopText(currentTarget.transform.position, value, this);
        if (currentTarget.vfxController != null)
            currentTarget.vfxController.buff.SetActive(true);
        currentTarget.healthBarController.UpdateBuff();
    }
    public override void End(CharacterBase currentTarget)
    {

    }
}
