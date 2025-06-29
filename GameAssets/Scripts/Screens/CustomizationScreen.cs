using GamerGAMEDEV.Categories;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GamerGAMEDEV.Customization;
using GamerGAMEDEV.Resources;


public class CustomizationScreen : ScreenBase
{
    #region Variables
    [Space, Header("CUSTOMIZABLE ENTITY")]
    public CustomizableObject customizableObject;
    [Space]
    public InventoryManager inventoryManager;
    public GamerGAMEDEV.UI.ToggleGroup toggleGroup;
    public ToggleGroupResetter resetter;

    HashSet<CustomizationCategory> totalCategories;
    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
        AddListeners();
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void ApplyItem(CustomizationItemSO item)
    {
        customizableObject.ApplyItem(item);
    }

    public void SetCategoryButtons()
    {
        RefreshTotalCategories();
        Debug.Log($"Called");

        if (totalCategories.Count > 0)
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

        resetter.ResetToggleGroup();
    }

    void HideCategoryButtons()
    {
        for (int i = 0; i < categoryButtons.Length; i++)
        {
            categoryButtons[i].gameObject.SetActive(false);
        }
    }

    public void RefreshTotalCategories()
    {
        totalCategories = new HashSet<CustomizationCategory>(inventoryManager.customizationItemsInInventory.Select(inventoryItem => inventoryItem.category));
    }

    public List<CustomizationItemSO> GetItemsByCategory(CustomizationCategory category)
    {
        return inventoryManager.customizationItemsInInventory.Where(inventoryItem => inventoryItem.category == category).ToList();
    }

    #endregion Main

    #region Base
    public override void InitializeScreen()
    {
        base.InitializeScreen();
        SetCategoryButtons();
    }

    public override void AddListeners()
    {
        base.AddListeners();
    }

    public override void AddCallbacks()
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveCallbacks()
    {
        throw new System.NotImplementedException();
    }

    public override void CloseScreen()
    {
        base.CloseScreen();
        //HideCategoryButtons();
    }

    #endregion Base
}