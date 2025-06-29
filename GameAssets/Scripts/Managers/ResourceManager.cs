using GamerGAMEDEV.Resources;
using GamerGAMEDEV.States;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{
    #region Variables
    public static ResourceManager instance;

    public static ResourceManager Instance => instance;


    [Header("ITEMS")]
    public ItemsCollection itemResources;

    private Dictionary<string, ItemSO_Base> inventoryResourceLookup;
    private Dictionary<string, CustomizationItemSO> customizeResourceLookup;

    [Space, Header("SPRITES")]
    public SpritesCollection spriteResources;

    #endregion Variables

    #region Unity Methods

    private void Awake()
    {
        if(instance == null) instance = this;

        inventoryResourceLookup = itemResources.items.ToDictionary(resources => resources.itemID);
        customizeResourceLookup = itemResources.customizationItems.ToDictionary(resources => resources.itemID);
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public ItemSO_Base GetInventoryItemResourceByID(string id)
    {
        return inventoryResourceLookup.TryGetValue(id, out var result) ? result : null;
    }

    public IEnumerator LoadResources(List<ItemState> itemStates)
    {
        foreach (ItemState itemState in itemStates)
        {
            InventoryManager.Instance.RestoreInventory(new InventoryItem(GetInventoryItemResourceByID(itemState.id) as ItemSO, itemState.equipped));

            yield return null;
        }
    }

    public CustomizationItemSO GetCustomizationItemResourceByID(string id)
    {
        return customizeResourceLookup.TryGetValue(id, out var result) ? result : null;
    }

    public IEnumerator LoadSkinResources(List<ItemState> itemStates)
    {
        foreach (ItemState itemState in itemStates)
        {
            InventoryManager.Instance.RestoreSkinInventory(GetCustomizationItemResourceByID(itemState.id));

            yield return null;
        }
    }

    #endregion Main
}