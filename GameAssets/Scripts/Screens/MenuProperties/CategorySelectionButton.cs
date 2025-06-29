using GamerGAMEDEV.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CategorySelectionButton : SavableMonoBehaviour<CategorySelectionButtonState>
{
    #region Variables
    [Header("REFRENCE")]
    [SerializeField] Toggle toggle;
    public RectTransform contentParent;

    [Header("PREVIEW"), Space]
    public TextMeshProUGUI nameText;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        //toggle.onValueChanged.AddListener((on) =>
        //{
        //    Debug.Log($"Called {on}");
        //    if (on)
        //    {
        //        OnSelected();
        //    }
        //    else
        //    {
        //        OnDeselected();
        //    }
        //});
    }

    private void OnEnable()
    {
        //toggle.onValueChanged.AddListener((on) =>
        //{
        //    Debug.Log($"Called {on}");
        //    if (on)
        //    {
        //        OnSelected();
        //    }
        //    else
        //    {
        //        OnDeselected();
        //    }
        //});
    }

    private void OnDisable()
    {
        //toggle.onValueChanged.RemoveAllListeners();
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void OnToggleSelected(bool on)
    {
        if (on)
        {
            OnSelected();
        }
        else
        {
            OnDeselected();
        }
    }

    public virtual void InitializeData(CategoryBaseSO categoryData)
    {
        
    }
    public virtual void InitializeData(ScreenBase screen)
    {

    }

    public virtual void OnSelected()
    {
        ShowContent();
        toggle.interactable = false;
    }

    public virtual void OnDeselected()
    {
        HideContent();
        toggle.interactable = true;
    }

    public virtual void ShowContent()
    {

    }

    public virtual void HideContent()
    {

    }

    public override CategorySelectionButtonState Capture()
    {
        throw new System.NotImplementedException();
    }

    public override void Restore(CategorySelectionButtonState state)
    {
        throw new System.NotImplementedException();
    }

    #endregion Main
}