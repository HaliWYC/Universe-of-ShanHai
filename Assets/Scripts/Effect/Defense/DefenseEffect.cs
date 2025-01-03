using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "DefenseEffect", menuName = "Card Effect/DefenseEffect")]
public class DefenseEffect : Effect
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
    public override void UpdateCurrentValue()
    {
        if (GameManager.Instance.player.characterData != null)
            currentValue = math.round(value * GameManager.Instance.player.characterData.currentDefenseMultiplier + GameManager.Instance.player.characterData.nextDefenseIncrement);
    }
    public void SetupRound(CharacterBase currentTarget)
    {
        foreach (var buff in currentTarget.characterData.buffList)
        {
            if (buff is DefenseEffect effect && effect.value == value && effect.effectDurationType == EffectDurationType.Sustainable)
            {
                effect.round += round;
                currentTarget.vfxController.buff.SetActive(true);
                currentTarget.healthBarController.UpdateBuff();
                return;
            }
        }
        var defense = SetUpValue(CreateInstance<DefenseEffect>());
        if (!IsOnceEffect(defense))
            currentTarget.characterData.buffList.Add(defense);
        defense.Execute(currentTarget);
    }

    private DefenseEffect SetUpValue(DefenseEffect effect)
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
        PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails(from.shieldSound));
        currentTarget.UpdateShield((int)math.round(value * from.characterData.currentDefenseMultiplier + from.characterData.nextDefenseIncrement));
        GamePlayPanel.Instance.PopText(currentTarget.transform.position, (int)math.round(value * from.characterData.currentDefenseMultiplier + from.characterData.nextDefenseIncrement), this);
        currentTarget.healthBarController.UpdateBuff();
    }

    public override void End(CharacterBase currentTarget)
    {

    }
}
