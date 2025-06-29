using UnityEngine;


public class GameplayScene_ScreensManager : ScreensManager
{
    #region Variables

    #region Hidden Properties Region

    [ReadOnly, SerializeField, Space(10)]
    private ScreenType type = ScreenType.GAMEPLAY;

    /// <summary>
    /// Public property to Get Screen Type.
    /// </summary>
    public override ScreenType ScreenType => type;

    #endregion Hidden Properties Region


    #endregion Variables

    #region Unity Methods

    private void Start()
    {
    
    }

    private void Update()
    {
    
    }

    private void OnEnable()
    {
    
    }

    private void OnDisable()
    {
    
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main
    #endregion Main
}