using System.Collections.Generic;
using GamerGAMEDEV.Customization;
using GamerGAMEDEV.Resources;
using GamerGAMEDEV.States;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem.HID;


public class CustomizationCategorySelectionButton : CategorySelectionButton
{
    #region Variables
    [Space, Header("CATEGORY DATA")]

    [SerializeField] CustomizationScreen customizationScreen;
    [SerializeField] CustomizationCategory category;

    List<CustomizationItemSO> itemsByCategory;

    List<CustomizationItemHUD> inventoryHUDs = new List<CustomizationItemHUD>();

    public CategorySelectionButtonState state = new CategorySelectionButtonState();

    #endregion Variables

    #region Unity Methods

    private void OnEnable()
    {
        SaveManager.Register(this);

        SaveManager.LoadSingle(this);
    }

    private void OnDisable()
    {
        SaveManager.Unregister(this);
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void SetState(string id)
    {
        state.id = id;
        MarkDirty();
        Debug.Log($"Set Customization ID = {state.id} , {id}");
    }

    public override void ShowContent()
    {
        if (customizationScreen == null) return;

        itemsByCategory = customizationScreen.GetItemsByCategory(category);

        if (itemsByCategory.Count > 0)
        {
            customizationScreen.toggleGroup.SetAllOff();

            CustomizationItemHUD selectedItem = null;

            foreach (CustomizationItemSO inventoryItem in itemsByCategory)
            {
                Debug.Log($"Reached Here Out Customization Item {state.id}");
                PoolableObject poolableObject = PoolManager.Instance.SpawnFromPool("CustomizationItem_HUD", contentParent, Quaternion.identity);

                if (poolableObject.TryGetComponent<CustomizationItemHUD>(out CustomizationItemHUD inventoryItemHUD))
                {
                    Debug.Log("Reached Here In InventoryHUD");

                    inventoryItemHUD.transform.SetParent(contentParent, false);
                    inventoryItemHUD.transform.localScale = Vector3.one;
                    inventoryItemHUD.ApplySettings(this, inventoryItem, customizationScreen);

                    if ((state == null || state.id == "") && inventoryItem.isDefaultItem || inventoryItem.itemID == state.id)
                    {
                        selectedItem = inventoryItemHUD;
                        inventoryHUDs.Insert(0, selectedItem);

                        selectedItem.transform.SetSiblingIndex(0);
                    }
                    else
                    {
                        inventoryHUDs.Add(inventoryItemHUD);
                    }
                }
            }

            if (selectedItem != null)
            {
                selectedItem.toggle.OnClick();
            }
        }
    }

    public override void HideContent()
    {
        if (inventoryHUDs.Count > 0)
        {
            foreach (CustomizationItemHUD inventoryHUD in inventoryHUDs)
            {
                PoolManager.Instance.ReturnToPool(inventoryHUD.poolID, inventoryHUD);
            }

            inventoryHUDs.Clear();
        }
    }

    #endregion Main

    #region Base

    public override void InitializeData(ScreenBase screen)
    {
        customizationScreen = screen as CustomizationScreen;
        nameText.text = category.ToString();
        base.InitializeData(screen);
    }

    public override CategorySelectionButtonState Capture()
    {
        Debug.Log($"SAVING : state.id : {state.id}");
        return state;
    }

    public override void Restore(CategorySelectionButtonState state)
    {
        this.state = state;
    }

    #endregion
}