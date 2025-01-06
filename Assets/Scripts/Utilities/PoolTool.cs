using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[DefaultExecutionOrder(-100)]
public class PoolTool : Singleton<PoolTool>
{
    public GameObject objPrefab;
    public GameObject soundPrefab;
    public ObjectPool<GameObject> cardPool;
    public ObjectPool<GameObject> soundPool;

    protected override void Awake()
    {
        base.Awake();
        cardPool = new ObjectPool<GameObject>
        (createFunc: () => Instantiate(objPrefab, transform),
        actionOnGet: (obj) => obj.SetActive(true),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: false,
        defaultCapacity: 10,
        maxSize: 40
        );

        soundPool = new ObjectPool<GameObject>
        (createFunc: () => Instantiate(soundPrefab, transform),
        actionOnGet: (obj) => obj.SetActive(true),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: false,
        defaultCapacity: 10,
        maxSize: 40);
        PreFillPool(7);
    }

    private void PreFillPool(int count)
    {
        var PreFillArray = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            PreFillArray[i] = cardPool.Get();
        }
        foreach (var obj in PreFillArray)
        {
            cardPool.Release(obj);
        }
    }
    private void PreFillSoundPool(int count)
    {
        var PreFillArray = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            PreFillArray[i] = soundPool.Get();
        }
        foreach (var obj in PreFillArray)
        {
            soundPool.Release(obj);
        }
    }
    public GameObject GetObjectFromPool()
    {
        return cardPool.Get();
    }

    public void ReleaseObject(GameObject obj)
    {
        cardPool.Release(obj);
    }

    public void ReleaseSound(GameObject obj)
    {
        soundPool.Release(obj);
    }

    public void InitSoundEffect(SoundDetails soundDetails)
    {
        if (soundDetails == null) return;
        var obj = soundPool.Get();
        obj.GetComponent<Sound>().SetSound(soundDetails);
        StartCoroutine(DisableSound(obj, soundDetails));
    }

    private IEnumerator DisableSound(GameObject obj, SoundDetails soundDetails)
    {
        yield return new WaitForSeconds(soundDetails.audioClip.length);
        soundPool.Release(obj);
    }
}
