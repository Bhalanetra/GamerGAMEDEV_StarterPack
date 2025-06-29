using GamerGAMEDEV.Categories;
using System.Collections.Generic;
using GamerGAMEDEV.Screens;
using UnityEngine;
using System.Linq;
using GamerGAMEDEV.Resources;


public class InventoryScreen : ScreenBase
{
    #region Variables
    [Space]
    public InventoryManager inventoryManager;

    HashSet<ItemCategory> totalCategories;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
        AddListeners();
    }

    #endregion Unity Methods

    #region Main

    public void SetCategoryButtons()
    {
        RefreshTotalCategories();
        Debug.Log($"Called");

        if(totalCategories.Count > 0 )
        {
            for (int i = 0; i < categoryButtons.Length; i++)
            {
                if (i < totalCategories.Count)
                {
                    categoryButtons[i].gameObject.SetActive(true);
                    //categoryButtons[i].InitializeData(inventory.categories[i]);
                    categoryButtons[i].InitializeData(this);
                }
                else
                {
                    categoryButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void RefreshTotalCategories()
    {
        totalCategories = new HashSet<ItemCategory>(inventoryManager.itemsInInventory.Select(inventoryItem => inventoryItem.item.category));

        totalCategories.Add(ItemCategory.ALL);
    }

    public List<InventoryItem> GetItemsByCategory(ItemCategory category)
    {
        if (category == ItemCategory.ALL) return inventoryManager.itemsInInventory;

        return inventoryManager.itemsInInventory.Where(inventoryItem => inventoryItem.item.category == category).ToList();
    }

    #endregion Main

    #region Base

    public override void InitializeScreen()
    {
        base.InitializeScreen();
        SetCategoryButtons();
    }

    public override void AddCallbacks()
    {

    }

    public override void RemoveCallbacks()
    {

    }

    public override void AddListeners()
    {
        base.AddListeners();
    }

    public override void OpenScreen()
    {
        base.OpenScreen();
    }

    public override void CloseScreen()
    {
        base.CloseScreen();
    }

    #endregion Base
}