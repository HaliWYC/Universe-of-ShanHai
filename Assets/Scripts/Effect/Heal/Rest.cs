using UnityEngine;

[CreateAssetMenu(fileName = "RestRoom", menuName = "Card Effect/Rest")]
public class Rest : Effect
{
    public override void End(CharacterBase currentTarget)
    {

    }

    public override void Execute(CharacterBase currentTarget)
    {
        PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails(from.healSound));
        currentTarget.CurrentHP = currentTarget.CurrentHP + (int)value > currentTarget.MaxHP ? currentTarget.MaxHP : currentTarget.CurrentHP + (int)value;
        if (currentTarget.vfxController != null)
            currentTarget.vfxController.buff.SetActive(true);
        currentTarget.healthBarController.UpdateBuff();
    }

    public override void Init()
    {
        Execute(from);
    }

    public override void UpdateCurrentValue()
    {

    }

}
