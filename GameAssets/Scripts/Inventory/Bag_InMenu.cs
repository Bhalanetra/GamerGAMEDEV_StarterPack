using GamerGAMEDEV.Resources;
using System;
using UnityEngine;


public class Bag_InMenu : Inventory
{
    #region Variables
    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        SaveManager.LoadSingle(this, () =>
        {
            Init();
        });
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public override void AddItemToInventory(InventoryItem inventoryItem, Action OnComplete)
    {
        base.AddItemToInventory(inventoryItem, OnComplete);
    }

    public override void RemoveItemFromInventory(InventoryItem inventoryItem, Action OnComplete)
    {
        base.RemoveItemFromInventory(inventoryItem, OnComplete);
    }

    #endregion Main
}