using UnityEngine;

[CreateAssetMenu(fileName = "EliminateEffect", menuName = "Card Effect/EliminateEffect")]
public class EliminateEffect : Effect
{
    public override void Init()
    {
        Execute(null);
    }
    public override void UpdateUI()
    {

    }
    public override void Execute(CharacterBase currentTarget)
    {
        PoolTool.Instance.InitSoundEffect(AudioManager.Instance.soundDetailList.GetSoundDetails("Eliminate"));
        target.TakeDamage(99999);
        GamePlayPanel.Instance.PopText(currentTarget.transform.position, 99999, this);
    }
    public override void End(CharacterBase currentTarget)
    {

    }

}
