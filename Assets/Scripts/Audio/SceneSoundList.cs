using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSoundList", menuName = "Sound/SceneSoundList")]
public class SceneSoundList : ScriptableObject
{
    public List<SceneSound> sceneSoundList;
    public SceneSound GetSceneSound(RoomType sceneName)
    {
        return sceneSoundList.Find(x => x.sceneName == sceneName);
    }
}

[System.Serializable]
public class SceneSound
{
    public RoomType sceneName;
    public string gameSound;
    public string ambientSound;

}
