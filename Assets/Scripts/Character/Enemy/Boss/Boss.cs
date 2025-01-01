using UnityEngine;

public class Boss : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        if (!GuidanceManager.Instance.guidanceCheckList[22])
            StartCoroutine(GuidanceManager.Instance.BossRoomGuidance(5));
    }
}
