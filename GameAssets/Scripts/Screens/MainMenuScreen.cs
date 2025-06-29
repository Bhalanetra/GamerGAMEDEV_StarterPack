using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuScreen : ScreenBase
{
    #region Variables
    [Space(10), Header("BUTTONS")]
    [SerializeField] Button StartButton;
    [SerializeField] Button ProfileButton;
    [SerializeField] Button MapButton;
    [SerializeField] Button InventoryButton;
    [SerializeField] Button CustomizationButton;
    [SerializeField] Button LeaderboardsButton;
    [SerializeField] Button StoreButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button RateGameButton;

    [Space, Header("MAP DISPLAY")]
    [SerializeField] TextMeshProUGUI selectedMapName;
    [SerializeField] Image selectedMapIcon;

    MainMenu_ScreensManager screensManager;

    #endregion Variables

    #region Unity Methods

    public void Start()
    {
        AddCallbacks();
        screensManager = ScreensManager.Instance as MainMenu_ScreensManager;
    }

    private void OnEnable()
    {
        MainMenuManager.OnLevelSelected += ApplySelectedMapSettings;
    }

    private void OnDisable()
    {
        MainMenuManager.OnLevelSelected -= ApplySelectedMapSettings;
    }

    #endregion Unity Methods

    #region Base

    public override void AddCallbacks()
    {
        StartButton.onClick.AddListener(() =>
        {

        });

        ProfileButton.onClick.AddListener(() =>
        {

        });

        MapButton.onClick.AddListener(() =>
        {
            screensManager.OnMapButton?.Invoke();
        });

        InventoryButton.onClick.AddListener(() =>
        {
            screensManager.OnInventoryButton?.Invoke();
        });

        CustomizationButton.onClick.AddListener(() =>
        {
            screensManager.OnCustomizeButton?.Invoke();
        });

        LeaderboardsButton.onClick.AddListener(() =>
        {
            screensManager.OnLeaderboardsButton?.Invoke();
        });

        StoreButton.onClick.AddListener(() =>
        {
            screensManager.OnStoreButton?.Invoke();
        });

        SettingsButton.onClick.AddListener(() =>
        {
            screensManager.OnSettingsButton?.Invoke();
        });

        RateGameButton.onClick.AddListener(() =>
        {

        });
    }

    public override void AddListeners()
    {
        base.AddListeners();
    }

    public override void RemoveCallbacks()
    {

    }

    #endregion Base

    #region Main

    public void ApplySelectedMapSettings(MapSO map)
    {
        selectedMapName.text = map.mapName;
        selectedMapIcon.sprite = map.mapIcon;
    }

    #endregion Main
}