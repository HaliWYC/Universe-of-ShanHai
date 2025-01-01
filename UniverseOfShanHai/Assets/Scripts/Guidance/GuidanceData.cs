using UnityEngine;


[CreateAssetMenu(fileName = "GuidanceData", menuName = "Guidance/GuidanceData")]
public class GuidanceData : ScriptableObject
{

    public Vector3 position;
    [TextArea]
    public string prompt;

    public bool showCharacter;
}
