using System;
using UnityEngine;


public class MainMenu_ScreensManager : ScreensManager
{
    #region Variables

    #region Hidden Properties Region

    [ReadOnly, SerializeField, Space(10)]
    private ScreenType type = ScreenType.MAIN_MENU;

    /// <summary>
    /// Public property to Get Screen Type.
    /// </summary>
    public override ScreenType ScreenType => type;

    #endregion Hidden Properties Region

    [Space(10)]
    [Header("SCREENS")]
    [SerializeField] SettingsScreen settingsScreen;
    [SerializeField] LeaderboardScreen leaderboardScreen;
    [SerializeField] StoreScreen storeScreen;
    [SerializeField] InventoryScreen inventoryScreen;
    [SerializeField] CustomizationScreen customizationScreen;
    [SerializeField] MapSelectionScreen mapSelectionScreen;

    //Actions
    public Action OnStartButton;
    public Action OnProfileButton;
    public Action OnMapButton;
    public Action OnInventoryButton;
    public Action OnLeaderboardsButton;
    public Action OnStoreButton;
    public Action OnSettingsButton;
    public Action OnRateGameButton;
    public Action OnCustomizeButton;

    #endregion Variables

    #region Unity Methods

    private void OnEnable()
    {
        OnSettingsButton += settingsScreen.InitializeScreen;
        OnLeaderboardsButton += leaderboardScreen.InitializeScreen;
        OnStoreButton += storeScreen.InitializeScreen;
        OnInventoryButton += inventoryScreen.InitializeScreen;
        OnCustomizeButton += customizationScreen.InitializeScreen;
        OnMapButton += mapSelectionScreen.InitializeScreen;
    }

    private void OnDisable()
    {
        OnSettingsButton -= settingsScreen.InitializeScreen;
        OnLeaderboardsButton -= leaderboardScreen.InitializeScreen;
        OnStoreButton -= storeScreen.InitializeScreen;
        OnInventoryButton -= inventoryScreen.InitializeScreen;
        OnCustomizeButton -= customizationScreen.InitializeScreen;
        OnMapButton -= mapSelectionScreen.InitializeScreen;
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main
    #endregion Main
}