using UnityEngine;
using System.Collections.Generic;
using GamerGAMEDEV.States;
using System.Collections;
using GamerGAMEDEV.Resources;
using System;


public class Inventory : SavableMonoBehaviour<InventoryState>
{
    #region Variables
    [Header("ITEMS IN INVENTORY")]
    public List<InventoryItem> InventoryItems;


    private InventoryState inventoryState = new InventoryState();

    public InventoryState InventoryState => inventoryState;

    [SerializeField] public RectTransform parent;
    [SerializeField] public GameObject panel;

    #endregion Variables

    #region Unity Methods
    public void Init()
    {
        Debug.Log($"Loading Completed for : {gameObject.name}, and Count is : {inventoryState.inventoryItemsState.Count}");
        if (inventoryState.inventoryItemsState.Count > 0)
        {
            StartCoroutine(LoadResources(inventoryState.inventoryItemsState));
        }
    }

    private void OnEnable()
    {
        SaveManager.Register(this);

        SaveManager.OnLoadindComplete += Init;
    }

    private void OnDisable()
    {
        SaveManager.Unregister(this);
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main
    public void RestoreInventory(InventoryItem item)
    {
        InventoryItems.Add(item);
    }

    public virtual void AddItemToInventory(InventoryItem inventoryItem, Action OnComplete)
    {
        InventoryItems.Add(inventoryItem);
        inventoryState.inventoryItemsState.Add(new ItemState(inventoryItem.item.itemID, false));

        MarkDirty();

        OnComplete?.Invoke();
    }

    public virtual void RemoveItemFromInventory(InventoryItem inventoryItem, Action OnComplete)
    {
        if(InventoryItems.Contains(inventoryItem)) InventoryItems.Remove(inventoryItem);

        MarkDirty();

        OnComplete?.Invoke();
    }

    public virtual void OpenInventory()
    {
        if(panel) panel.SetActive(true);
    }

    public virtual void CloseInventory()
    {
        if(panel) panel.SetActive(false);
    }

    #endregion Main

    #region Save/Load

    public override InventoryState Capture()
    {
        return inventoryState;
    }

    public override void Restore(InventoryState state)
    {
        inventoryState = state ?? new();
    }

    public IEnumerator LoadResources(List<ItemState> savedItemStates)
    {
        foreach (ItemState itemState in savedItemStates)
        {
            RestoreInventory(new InventoryItem(ResourceManager.instance.GetInventoryItemResourceByID(itemState.id) as ItemSO, itemState.equipped));

            yield return null;
        }
    }

    #endregion Save/Load
}