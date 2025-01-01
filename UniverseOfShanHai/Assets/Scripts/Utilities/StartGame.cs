using UnityEngine;
using UnityEngine.AddressableAssets;

public class StartGame : MonoBehaviour
{
    public AssetReference persistent;

    private void Awake() 
    {
        Addressables.LoadSceneAsync(persistent);
    }
}
