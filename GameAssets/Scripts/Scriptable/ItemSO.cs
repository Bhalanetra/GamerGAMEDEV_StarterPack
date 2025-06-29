using GamerGAMEDEV.Categories;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Gamedev_StarterPack/Inventory/ItemSO")]
public class ItemSO : ItemSO_Base
{
    public ItemCategory category;
    public Sprite icon;

    public void UseItem()
    {
        Debug.Log($"Used Item {name}");
    }
}
