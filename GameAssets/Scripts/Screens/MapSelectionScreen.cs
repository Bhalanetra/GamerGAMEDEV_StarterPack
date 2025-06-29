using GamerGAMEDEV.Screens;
using UnityEngine;


public class MapSelectionScreen : ScreenBase
{
    #region Variables
    [Space]
    public MapCategories maps;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        AddListeners();
        MainMenuManager.OnLevelSelected?.Invoke(maps.categories[0].maps[0]);
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
        Debug.Log($"Called");

        for (int i = 0; i < categoryButtons.Length; i++)
        {
            if(i < maps.categories.Length)
            {
                categoryButtons[i].gameObject.SetActive(true);
                categoryButtons[i].InitializeData(maps.categories[i]);
            }
            else
            {
                categoryButtons[i].gameObject.SetActive(false);
            }
        }
    }


    #endregion Main

    #region Base

    public override void InitializeScreen()
    {
        SetCategoryButtons();
        base.InitializeScreen();
    }

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