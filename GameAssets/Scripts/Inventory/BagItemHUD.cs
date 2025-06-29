using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BagItemHUD : PoolableObject
{
    #region Variables
    [Space]
    [Header("UI")]
    public Image _icon;
    public TextMeshProUGUI _name;
    public RectTransform _rectTransform;

    [Header("SIZE")]
    [SerializeField] LayoutElement _layoutElement;
    public float defaultHeight;
    public float selectedHeight;
    #endregion Variables

    #region Unity Methods

    #endregion Unity Methods

    #region Other

    public void OnSelected(bool isSelected)
    {
        if (isSelected)
        {
            _rectTransform.sizeDelta = new Vector2(_rectTransform.rect.width, selectedHeight);
        }
        else
        {
            _rectTransform.sizeDelta = new Vector2(_rectTransform.rect.width, defaultHeight);
        }

        _layoutElement.preferredHeight = isSelected ? selectedHeight : defaultHeight;
    }

    #endregion Other

    #region Main

    public void ApplySettings(ItemSO item)
    {
        _icon.sprite = item.icon;
        _name.text = item.name;
    }

    #endregion Main
}