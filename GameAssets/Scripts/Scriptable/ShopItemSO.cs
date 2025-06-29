using GamerGAMEDEV.Categories;
using GamerGAMEDEV.Currency;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "Gamedev_StarterPack/Shop/ShopItemSO")]
public class ShopItemSO : ScriptableObject
{
    public ItemCategory ItemCategory;
    public ItemSO item;
    public int price;
    public float discount;
    public CurrencyType currency;
    public bool showLabel = false;
    public bool onDiscount = false;
    public Sprite label;
    public string labelText;
}
