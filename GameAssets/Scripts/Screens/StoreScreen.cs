using GamerGAMEDEV.Categories;
using GamerGAMEDEV.Screens;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StoreScreen : ScreenBase
{
    #region Variables

    public List<ShopItemSO> items;

    HashSet<ItemCategory> totalCategories;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        AddListeners();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    #endregion Unity Methods

    #region Main

    public void SetCategoryButtons()
    {
        RefreshTotalCategories();
        Debug.Log($"Called {totalCategories.Count}");

        if(totalCategories.Count > 0)
        {
            for (int i = 0; i < categoryButtons.Length; i++)
            {
                if (i < totalCategories.Count)
                {
                    categoryButtons[i].gameObject.SetActive(true);
                    //categoryButtons[i].InitializeData(shop.categories[i]);
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
        totalCategories = new HashSet<ItemCategory>(items.Select(item => item.ItemCategory));

        totalCategories.Add(ItemCategory.ALL);
    }

    public List<ShopItemSO> GetItemsByCategory(ItemCategory category)
    {
        if(category == ItemCategory.ALL) return items;

        return items.Where(item => item.ItemCategory == category).ToList();
    }


    #endregion Main

    #region Base

    public override void InitializeScreen()
    {
        SetCategoryButtons();
        base.InitializeScreen();
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