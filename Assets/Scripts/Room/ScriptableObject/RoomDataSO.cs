using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "RoomDataSO", menuName = "Map/RoomDataSO")]
public class RoomDataSO : ScriptableObject
{
    public Sprite roomIcon;
    public RoomType roomType;
    public MultipleRarity rarity;
    public List<CardDataSO> rewardList;
    public AssetReference sceneToLoad;
}