using UnityEngine;

public class Boss : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        if (!GuidanceManager.Instance.isBossChecked)
            StartCoroutine(GuidanceManager.Instance.BossRoomGuidance(5));
    }
}
