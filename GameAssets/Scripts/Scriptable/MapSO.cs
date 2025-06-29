using UnityEngine;

[CreateAssetMenu(fileName = "MapSO", menuName = "Gamedev_StarterPack/Maps/MapSO")]
public class MapSO : ScriptableObject
{
    public string mapName;
    public Sprite mapIcon;
    public string mapIndex;
    public LevelConfigBaseSO levelConfig;
}
