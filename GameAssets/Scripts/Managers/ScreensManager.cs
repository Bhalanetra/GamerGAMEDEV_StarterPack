using UnityEngine;


public abstract class ScreensManager : MonoBehaviour
{
    #region Variables
    protected static ScreensManager instance;

    public static ScreensManager Instance => instance.GetScreenBasedOnType();

    public abstract ScreenType ScreenType { get; }
    #endregion Variables

    #region Unity Methods
    private void Awake()
    {
        if(instance == null) instance = this;
    }
    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public ScreensManager GetScreenBasedOnType()
    {
        switch (ScreenType)
        {
            case ScreenType.MAIN_MENU:
                return this as MainMenu_ScreensManager;

            case ScreenType.GAMEPLAY:
                return this as GameplayScene_ScreensManager;

            default:
                return null;
        }
    }

    #endregion Main
}