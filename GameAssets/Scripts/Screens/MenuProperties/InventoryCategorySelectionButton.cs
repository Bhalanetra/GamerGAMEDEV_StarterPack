using System.Collections.Generic;
using GamerGAMEDEV.Categories;
using GamerGAMEDEV.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryCategorySelectionButton : CategorySelectionButton
{
    #region Variables
    [Header("CATEGORY DATA")]
    public InventoryCategorySO categoryData;
    public InventoryScreen inventoryScreen;

    public ItemCategory category;

    List<InventoryItem> itemsByCategory;

    List<InventoyItemHUD> inventoryHUDs = new List<InventoyItemHUD>();

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
        this.categoryData = categoryData as InventoryCategorySO;
        nameText.text = categoryData.name;
    }

    public override void InitializeData(ScreenBase screen)
    {
        inventoryScreen = screen as InventoryScreen;
        nameText.text = category.ToString();
    }

    public override void ShowContent()
    {
        if (inventoryScreen == null) return;

        itemsByCategory = inventoryScreen.GetItemsByCategory(category);

        if(itemsByCategory.Count > 0)
        {
            foreach (InventoryItem inventoryItem in itemsByCategory)
            {
                Debug.Log("Reached Here Out InventoryHUD");
                PoolableObject poolableObject = PoolManager.Instance.SpawnFromPool("InventoryItemHUD", contentParent, Quaternion.identity);

                if (poolableObject.TryGetComponent<InventoyItemHUD>(out InventoyItemHUD inventoryItemHUD))
                {
                    Debug.Log("Reached Here In InventoryHUD");
                    inventoryItemHUD.transform.SetParent(contentParent, false);
                    inventoryItemHUD.transform.localScale = Vector3.one;
                    inventoryItemHUD.ApplySettings(inventoryItem);
                    inventoryHUDs.Add(inventoryItemHUD);
                }
            }
        }
    }

    public override void HideContent()
    {
        if (inventoryHUDs.Count > 0)
        {
            foreach (InventoyItemHUD inventoryHUD in inventoryHUDs)
            {
                PoolManager.Instance.ReturnToPool(inventoryHUD.poolID, inventoryHUD);
            }

            inventoryHUDs.Clear();
        }
    }

    #endregion Main
}