using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    private AssetReference currentScene;
    public AssetReference map;
    public AssetReference menu;
    public AssetReference intro;
    private Room currentRoom;
    public ObjectEventSO afterLoadSceneEvent;
    public ObjectEventSO updateRoomEvent;
    private Vector2Int currentRoomVector;
    public FadePanel fadePanel;

    private void Start()
    {
        currentRoomVector = Vector2Int.one * -1;
        LoadMenu();
    }
    /// <summary>
    /// 在房间加载时监听
    /// </summary>
    /// <param name="data"></param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            currentRoom = data as Room;

            var currentData = currentRoom.roomData;
            currentRoomVector = new(currentRoom.row, currentRoom.column);
            //Debug.Log(currentData.roomType);

            currentScene = currentData.sceneToLoad;
        }
        await UnloadSceneTask();
        await LoadSceneTask();
        afterLoadSceneEvent.RaiseEvent(currentRoom, this);
    }

    private async Awaitable LoadSceneTask()
    {
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        await s.Task;

        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            fadePanel.FadeOut(0.2f);
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    }

    private async Awaitable UnloadSceneTask()
    {
        fadePanel.FadeIn(0.4f);
        await Awaitable.WaitForSecondsAsync(0.45f);
        await Awaitable.FromAsyncOperation(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()));
    }

    /// <summary>
    /// 监听返回房间的事件函数
    /// </summary>
    /// <param name="data"></param>
    public async void LoadMap(object data)
    {
        await UnloadSceneTask();
        if (currentRoomVector != Vector2.one * -1)
            updateRoomEvent.RaiseEvent(currentRoomVector, this);
        currentScene = map;
        AudioManager.Instance.PlayGameSoundClip(AudioManager.Instance.soundDetailList.GetSoundDetails("Map"));
        RelicEvent.OnLoadMap();
        if (!GuidanceManager.Instance.isWelcomeChecked)
            GuidanceManager.Instance.StartGuidance();
        else
        {
            if (!GuidanceManager.Instance.isSecondRoomChecked)
                StartCoroutine(GuidanceManager.Instance.NextRoomGuidance(5));
        }
        await LoadSceneTask();
    }

    public async void LoadMenu()
    {
        if (currentScene != null)
            await UnloadSceneTask();
        currentScene = menu;
        await LoadSceneTask();
    }

    public async void LoadIntro()
    {
        if (currentScene != null)
            await UnloadSceneTask();
        currentScene = intro;
        await LoadSceneTask();
    }
}
