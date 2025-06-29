using UnityEngine;
using UnityEngine.UI;


public abstract class ScreenBase : MonoBehaviour
{
    #region Variables
    [Header("SCREEN")]
    [SerializeField] GameObject screen;

    [Space(10)]
    [SerializeField] Button backButton;


    [Space, Header("CATEGORY SELECTION BUTTONS")]
    public CategorySelectionButton[] categoryButtons;
    #endregion Variables

    #region Unity Methods

    //public virtual void OnEnable()
    //{

    //}

    //public virtual void OnDisable()
    //{

    //}

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public virtual void InitializeScreen()
    {
        OpenScreen();
    }

    public virtual void AddListeners()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(() =>
            {
                CloseScreen();
            });
        }
    }

    public abstract void AddCallbacks();
    public abstract void RemoveCallbacks();

    public virtual void OpenScreen()
    {
        screen.SetActive(true);
    }

    public virtual void CloseScreen()
    {
        screen.SetActive(false);
    }

    #endregion Main
}