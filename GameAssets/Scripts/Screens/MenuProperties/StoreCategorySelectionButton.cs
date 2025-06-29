using System.Collections.Generic;
using GamerGAMEDEV.Categories;
using UnityEngine;
using UnityEngine.UI;


public class StoreCategorySelectionButton : CategorySelectionButton
{
    #region Variables
    [Header("CATEGORY DATA")]
    public ShopCategorySO categoryData;
    public StoreScreen storScreen;

    public ItemCategory category;

    List<ShopItemSO> itemsByCategory = new List<ShopItemSO>();

    List<ShopItemHUD> shopItemHUDs = new List<ShopItemHUD>();
    #endregion Variables

    #region Unity Methods

    private void OnDisable()
    {
        HideContent();
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main
    public override void InitializeData(CategoryBaseSO categoryData)
    {
        this.categoryData = categoryData as ShopCategorySO;
        nameText.text = categoryData.name;
    }

    public override void InitializeData(ScreenBase screen)
    {
        this.storScreen = screen as StoreScreen;
        nameText.text = category.ToString();
    }

    public override void ShowContent()
    {
        itemsByCategory = storScreen.GetItemsByCategory(category);

        if(itemsByCategory.Count > 0)
        {
            foreach (ShopItemSO shopItemSO in itemsByCategory)
            {
                Debug.Log("Reached Here Out InventoryHUD");
                PoolableObject poolableObject = PoolManager.Instance.SpawnFromPool("ShopItemHUD", contentParent, Quaternion.identity);

                if (poolableObject.TryGetComponent<ShopItemHUD>(out ShopItemHUD shopItemHUD))
                {
                    Debug.Log("Reached Here In InventoryHUD");
                    shopItemHUD.transform.SetParent(contentParent, false);
                    shopItemHUD.transform.localScale = Vector3.one;
                    shopItemHUD.ApplySettings(shopItemSO);
                    shopItemHUDs.Add(shopItemHUD);
                }
            }
        }
    }

    public override void HideContent()
    {
        if (shopItemHUDs.Count > 0)
        {
            foreach (ShopItemHUD shopItemHUD in shopItemHUDs)
            {
                PoolManager.Instance.ReturnToPool(shopItemHUD.poolID, shopItemHUD);
            }

            shopItemHUDs.Clear();
        }
    }
    #endregion Main
}