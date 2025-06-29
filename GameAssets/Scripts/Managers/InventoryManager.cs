using UnityEngine;
using System.Collections.Generic;
using GamerGAMEDEV.States;
using GamerGAMEDEV.Resources;
using System;
using System.Linq;


public class InventoryManager : SavableMonoBehaviour<InventoryState>
{
    #region Variables

    public static InventoryManager instance;

    public static InventoryManager Instance => instance;


    [Header("INVENTORY")]
    public List<InventoryItem> itemsInInventory;

    public List<CustomizationItemSO> customizationItemsInInventory;

    [Space]
    [Header("BAG")]
    public Inventory Bag;

    InventoryState inventoryState = new InventoryState();

    #endregion Variables

    #region Unity Methods

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    private void Start()
    {
        Debug.Log(gameObject.name);
        SaveManager.LoadSingle(this, () =>
        {
            Initialize();
        });
    }

    private void Initialize()
    {
        Debug.Log($"Loading Completed! {inventoryState.inventoryItemsState.Count}");
        if (inventoryState.inventoryItemsState.Count > 0)
        {
            StartCoroutine(ResourceManager.Instance.LoadResources(inventoryState.inventoryItemsState));
        }
    }

    private void OnEnable()
    {
        SaveManager.Register(this);
    }

    private void OnDisable()
    {
        SaveManager.Unregister(this);
    }

    #endregion Unity Methods

    #region Main

    #region ItemInventory

    public void AddItemToTheBag(InventoryItem inventoryItem, Action OnComplete = null)
    {
        if (Bag == null)
        {
            Debug.LogError("Bag Is Null");
            return;
        }

        Bag.AddItemToInventory(inventoryItem, () =>
        {
            SetItemEquippedState(inventoryItem, true);
            OnComplete?.Invoke();
        });
    }

    public void RemoveItemFromTheBag(InventoryItem inventoryItem, Action OnComplete = null)
    {
        if (Bag == null)
        {
            Debug.LogError("Bag Is Null");
            return;
        }

        Bag.RemoveItemFromInventory(inventoryItem, () =>
        {
            SetItemEquippedState(inventoryItem, false);
            OnComplete?.Invoke();
        });
    }

    public void RestoreInventory(InventoryItem inventoryItem)
    {
        itemsInInventory.Add(inventoryItem);
    }

    public void AddItemToInventory(ItemSO item)
    {
        itemsInInventory.Add(new InventoryItem(item, false));
        inventoryState.inventoryItemsState.Add(new ItemState(item.itemID, false));

        MarkDirty();

        SaveManager.SaveModified();
    }

    public void RemoveItemFromInventory(ItemSO item)
    {
        // Find index of the exact InventoryItem (first one with matching item reference)
        int index = itemsInInventory.FindIndex(i => i.item == item);

        if (index >= 0 && index < inventoryState.inventoryItemsState.Count)
        {
            itemsInInventory.RemoveAt(index);
            inventoryState.inventoryItemsState.RemoveAt(index);

            MarkDirty();
            SaveManager.SaveModified();
        }
        else
        {
            Debug.LogWarning("Item not found or mismatch in inventory state.");
        }
    }


    private void SetItemEquippedState(InventoryItem item, bool isEquipped)
    {
        // Find index of the exact item reference
        int index = itemsInInventory.IndexOf(item);

        if (index >= 0 && index < inventoryState.inventoryItemsState.Count)
        {
            itemsInInventory[index].isEquipped = isEquipped;
            inventoryState.inventoryItemsState[index].equipped = isEquipped;

            MarkDirty();
            //SaveManager.SaveModified();
        }
        else
        {
            Debug.LogWarning("Item not found or mismatched index in inventory.");
        }
    }

    #endregion ItemInventory

    #region SkinInventory

    public void RestoreSkinInventory(CustomizationItemSO inventoryItem)
    {
        customizationItemsInInventory.Add(inventoryItem);
    }

    public void AddItemToSkinInventory(CustomizationInventoryItem item)
    {
        customizationItemsInInventory.Add(item.item);
        inventoryState.skinInventoryState.Add(item.item.itemID);
    }

    #endregion SkinInventory

    public void TestSave()
    {
        SaveManager.SaveModified(() =>
        {
            Debug.Log($"Saving Complete for : {gameObject.name}");
        });
    }

    #endregion Main

    #region Save/Load

    public override InventoryState Capture()
    {
        inventoryState = new InventoryState();

        foreach(InventoryItem inventoryItem in itemsInInventory)
        {
            inventoryState.inventoryItemsState.Add(new ItemState(inventoryItem.item.itemID, inventoryItem.isEquipped));
        }

        return inventoryState;
    }

    public override void Restore(InventoryState state)
    {
        inventoryState = new InventoryState();
        inventoryState = state ?? new();
    }

    #endregion Save/Load
}