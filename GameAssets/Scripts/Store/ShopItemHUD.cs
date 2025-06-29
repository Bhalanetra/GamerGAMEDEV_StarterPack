using GamerGAMEDEV.Currency;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopItemHUD : PoolableObject
{
    #region Variables
    [Header("PREVIEW")]
    public TextMeshProUGUI nameText;
    public Image imagePreview;
    public TextMeshProUGUI oldPriceText;
    public TextMeshProUGUI priceText;
    public Image label;
    public TextMeshProUGUI labelText;
    public CurrencyType currencyType;

    [Header("BUTTONS"), Space]
    public Button buyButton;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void ApplySettings(ShopItemSO itemSO)
    {
        nameText.text = itemSO.item.name;
        imagePreview.sprite = itemSO.item.icon;
        priceText.text = itemSO.price.ToString();

        if (itemSO.onDiscount && !itemSO.showLabel)
        {
            priceText.text = (itemSO.price - Mathf.RoundToInt(itemSO.price * (itemSO.discount / 100))).ToString();
            oldPriceText.text = itemSO.price.ToString();
            oldPriceText.gameObject.SetActive(true);

            label.sprite = itemSO.label;
            label.gameObject.SetActive(true);
            labelText.text = $"{itemSO.discount}% {itemSO.labelText}";
        }
        else if (itemSO.showLabel && !itemSO.onDiscount)
        {
            oldPriceText.gameObject.SetActive(false);

            label.sprite = itemSO.label;
            label.gameObject.SetActive(true);
            labelText.text = $"{itemSO.labelText}";
        }
        else
        {
            oldPriceText.gameObject.SetActive(false);
            label.gameObject.SetActive(false);
        }

        buyButton.onClick.RemoveAllListeners();


        buyButton.onClick.AddListener(() =>
        {
            StoreManager.instance.BuyItem(itemSO);
        });
    }

    #endregion Main
}