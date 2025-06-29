using GamerGAMEDEV.Customization;
using UnityEngine;

[CreateAssetMenu(fileName = "Customization Item", menuName = "Gamedev_StarterPack/Customization/Customization Item")]
public class CustomizationItemSO : ItemSO_Base
{
    [Header("DETAILS")]
    public bool isDefaultItem = false;
    public Sprite icon;
    public string displayName;
    public CustomizationCategory category;
    public CustomizationType type;

    [Space, Header("SLOTS")]
    public Mesh meshToSwap; // For MeshFilter
    public GameObject prefabToSwap; // For ObjectSwap
    public Material materialToApply;
    public Color colorToApply;

    public Sprite previewIcon;
}