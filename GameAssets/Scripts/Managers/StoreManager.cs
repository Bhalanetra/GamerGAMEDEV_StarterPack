using UnityEngine;


public class StoreManager : MonoBehaviour
{
    #region Variables
    public static StoreManager instance;

    public static StoreManager Instance => instance;

    #endregion Variables

    #region Unity Methods

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void BuyItem(ShopItemSO item)
    {
        InventoryManager.instance.AddItemToInventory(item.item);
    }

    #endregion Main
}