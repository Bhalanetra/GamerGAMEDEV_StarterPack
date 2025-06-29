using UnityEngine;


public class LeaderboardScreen : ScreenBase
{
    #region Variables
    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        AddListeners();
    }

    private void OnEnable()
    {
    
    }

    private void OnDisable()
    {
    
    }

    #endregion Unity Methods

    #region Main

    public void SetCategoryButtons()
    {

    }


    #endregion Main

    #region Base

    public override void AddCallbacks()
    {

    }

    public override void RemoveCallbacks()
    {

    }

    public override void AddListeners()
    {
        base.AddListeners();
    }

    public override void OpenScreen()
    {
        base.OpenScreen();
    }

    public override void CloseScreen()
    {
        base.CloseScreen();
    }

    #endregion Base
}