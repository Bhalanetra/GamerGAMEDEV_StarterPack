using System.Collections.Generic;
using UnityEngine;


public class LevelsManager : MonoBehaviour
{
    #region Variables
    public static LevelsManager instance;

    [Space]
    public MapSO selectedMap;

    #endregion Variables

    #region Unity Methods

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnEnable()
    {
        MainMenuManager.OnLevelSelected += SetSelectedLevel;
    }

    private void OnDisable()
    {
        MainMenuManager.OnLevelSelected -= SetSelectedLevel;
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void SetSelectedLevel(MapSO map)
    {
        selectedMap = map;
    }

    #endregion Main
}