using GamerGAMEDEV.Currency;
using GamerGAMEDEV.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CurrencyHUD : MonoBehaviour
{
    #region Variables
    [Header("UI")]
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image icon;
    [SerializeField] CurrencyType currencyType;
    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        RefreshHUD();
    }

    private void OnEnable()
    {
        SaveManager.OnSaveLoadComplete += RefreshHUD;
    }

    private void OnDisable()
    {
        SaveManager.OnSaveLoadComplete -= RefreshHUD;
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void RefreshHUD()
    {
        GameState state = GameManager.Instance.gameState;
        ResourceManager resourceManager = ResourceManager.Instance;

        switch (currencyType)
        {
            case CurrencyType.COIN:

                amountText.text = state.coin.ToString();
                icon.sprite = resourceManager.spriteResources._coin;

                break;
            case CurrencyType.GEM:

                amountText.text = state.gems.ToString();
                icon.sprite = resourceManager.spriteResources._gem;

                break;
            default:
                break;
        }
    }

    #endregion Main
}