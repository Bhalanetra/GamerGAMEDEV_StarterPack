using System;
using GamerGAMEDEV.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoyItemHUD : PoolableObject
{
    #region Variables
    [Header("PREVIEW")]
    public TextMeshProUGUI nameText;
    public Image imagePreview;

    [Header("BUTTON"), Space]
    public Button useButton;
    public GamerGAMEDEV.UI.Toggle toggle;

    InventoryItem _inventoryItem;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        //useButton.onClick.AddListener(() =>
        //{
        //    OnUse();
        //});
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void OnEquipButton(bool on)
    {
        if (_inventoryItem == null)
        {
            Debug.LogError($"Inventory Item is NULL");
            return;
        }

        _inventoryItem.isEquipped = on;

        InventoryManager inventoryManager = InventoryManager.Instance;

        Action<InventoryItem, Action> Event = on ? inventoryManager.AddItemToTheBag : inventoryManager.RemoveItemFromTheBag;

        Event?.Invoke(_inventoryItem, () =>
        {
            SaveManager.SaveModified();
        });
    }

    public void ApplySettings(InventoryItem inventoryItem)
    {
        nameText.text = inventoryItem.item.name;
        imagePreview.sprite = inventoryItem.item.icon;

        _inventoryItem = inventoryItem;

        toggle.SetState(inventoryItem.isEquipped);
    }

    public void OnUse()
    {
        _inventoryItem.item.UseItem();
    }

    #endregion Main
}