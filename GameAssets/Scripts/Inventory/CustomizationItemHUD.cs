using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationItemHUD : PoolableObject
{
    #region Variables
    [Header("UI")]
    public Image icon;
    public CustomizationItemSO item;
    public GamerGAMEDEV.UI.Toggle toggle;

    [Space]
    [SerializeField] CustomizationScreen screen;

    [SerializeField] CustomizationCategorySelectionButton myCategoryButton;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        //if (item != null)
        //    ApplySettings(item);
    }

    #endregion Unity Methods

    #region Main

    public void ApplySettings(CustomizationCategorySelectionButton categoryButton, CustomizationItemSO item, CustomizationScreen screen)
    {
        this.item = item;
        icon.sprite = item.icon;
        this.screen = screen;
        this.toggle.AssignToggleGroup(screen.toggleGroup);
        myCategoryButton = categoryButton;
    }

    public void Equip(bool equip)
    {
        if (equip)
        {
            toggle.interactable = false;
            screen.ApplyItem(item);
            myCategoryButton.SetState(item.itemID);
            SaveManager.SaveModified();
        }
    }

    #endregion Main
}
