using UnityEngine;


[CreateAssetMenu(fileName = "GuidanceData", menuName = "Guidance/GuidanceData")]
public class GuidanceData : ScriptableObject
{
    [TextArea]
    public string prompt;
}
