using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MapSelectionHUD : PoolableObject
{
    #region Variables
    [Header("PREVIEW")]
    public TextMeshProUGUI nameText;
    public Image imagePreview;
    public Button selectButton;
    public MapSO map;

    #endregion Variables

    #region Unity Methods

    private void OnEnable()
    {
        selectButton.onClick.AddListener(() =>
        {

        });
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void ApplySettings(MapSO map)
    {
        this.map = map;
        nameText.text = map.mapName;
        imagePreview.sprite = map.mapIcon;
    }

    public void SelectLevel()
    {
        MainMenuManager.OnLevelSelected?.Invoke(map);
    }

    #endregion Main
}