using GamerGAMEDEV.Resources;
using System.Collections.Generic;
using UnityEngine;


public class Bag_InGame : Inventory
{
    #region Variables

    List<BagItemHUD> bagHUDs = new List<BagItemHUD>();

    #endregion Variables

    #region Unity Methods

    #endregion Unity Methods

    #region Base

    private void Start()
    {
        SaveManager.LoadSingle(this, () =>
        {
            Init();
        });
    }

    public override void OpenInventory()
    {
        LoadItems();
        base.OpenInventory();
    }

    #endregion Base

    #region Main

    public void LoadItems()
    {
        if (InventoryItems.Count > 0)
        {
            foreach(InventoryItem item in InventoryItems)
            {
                PoolableObject poolableObject = PoolManager.Instance.SpawnFromPool("BagItem_HUD", parent, Quaternion.identity);

                if (poolableObject.TryGetComponent<BagItemHUD>(out BagItemHUD bagItemHUD))
                {
                    Debug.Log("Reached Here In InventoryHUD");
                    bagItemHUD.transform.SetParent(parent, false);
                    bagItemHUD.transform.localScale = Vector3.one;
                    bagItemHUD.ApplySettings(item.item);
                    bagHUDs.Add(bagItemHUD);
                }
            }
        }
    }

    #endregion Main
}