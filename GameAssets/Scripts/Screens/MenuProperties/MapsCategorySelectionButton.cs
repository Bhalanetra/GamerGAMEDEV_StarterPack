using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MapsCategorySelectionButton : CategorySelectionButton
{
    #region Variables
    [Header("CATEGORY DATA")]
    public MapCategorySO categoryData;

    List<MapSelectionHUD> mapsHUD = new List<MapSelectionHUD>();

    #endregion Variables

    #region Unity Methods

    private void OnDisable()
    {
        HideContent();
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public override void InitializeData(CategoryBaseSO categoryData)
    {
        this.categoryData = categoryData as MapCategorySO;
        nameText.text = categoryData.name;
    }

    public override void ShowContent()
    {
        foreach (MapSO map in categoryData.maps)
        {
            PoolableObject poolableObject = PoolManager.Instance.SpawnFromPool("MapHUD", contentParent, Quaternion.identity);

            if(poolableObject.TryGetComponent<MapSelectionHUD>(out MapSelectionHUD mapSelectionHUD))
            {
                mapSelectionHUD.transform.SetParent(contentParent, false);
                mapSelectionHUD.transform.localScale = Vector3.one;
                mapSelectionHUD.ApplySettings(map);
                mapsHUD.Add(mapSelectionHUD);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent);
    }

    public override void HideContent()
    {
        if(mapsHUD.Count > 0)
        {
            foreach (MapSelectionHUD mapHud in mapsHUD)
            {
                PoolManager.Instance.ReturnToPool(mapHud.poolID, mapHud);
            }

            mapsHUD.Clear();
        }
    }
    #endregion Main
}