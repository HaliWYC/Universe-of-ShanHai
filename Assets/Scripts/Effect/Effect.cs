using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public Sprite effectIcon;
    public float value;
    protected float currentValue;
    public int round;
    [TextArea]
    public string description;
    public EffectDurationType effectDurationType;
    public EffectTargetType targetType;
    protected CharacterBase from;
    protected CharacterBase target;
    private void OnEnable()
    {
        if (value != 0)
        {
            currentValue = value;
        }
    }

    public void Setup(CharacterBase f, CharacterBase t)
    {
        from = f;
        target = t;
        Init();
    }
    public abstract void Init();

    /// <summary>
    /// 执行效果
    /// </summary>
    /// <param name="from">发起者</param>
    /// <param name="target">目标</param>
    public abstract void Execute(CharacterBase currentTarget);
    public abstract void UpdateCurrentValue();
    public abstract void End(CharacterBase currentTarget);

    protected bool IsOnceEffect(Effect effect)
    {
        return effect.effectDurationType == EffectDurationType.Once;
    }

    public float GetCurrentValue(Effect effect)
    {
        float currentValue = effect.currentValue == 0 ? effect.value : effect.currentValue;
        return effect switch
        {
            StrengthEffect => currentValue * 100,
            ShieldEffect => currentValue * 100,
            _ => currentValue
        };
    }
}
