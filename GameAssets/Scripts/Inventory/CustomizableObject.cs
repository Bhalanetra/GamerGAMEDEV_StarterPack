using System.Collections.Generic;
using GamerGAMEDEV.Customization;
using UnityEngine;

public class CustomizableObject : SavableMonoBehaviour<CharacterCustomizeState>
{
    #region Variables

    public List<CustomizationSlot> customizationSlots;

    [SerializeField] CharacterCustomizeState characterCustomizeState;

    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        SaveManager.LoadSingle(this, () =>
        {
            LoadSettings();
        });
    }

    private void OnEnable()
    {
        SaveManager.Register(this);
    }

    private void OnDisable()
    {
        SaveManager.Unregister(this);
    }

    #endregion Unity Methods

    #region Main

    public void LoadSettings()
    {
        if (characterCustomizeState != null && characterCustomizeState.assignedItemID.Count > 0)
        {
            foreach (string id in characterCustomizeState.assignedItemID)
            {
                CustomizationItemSO customizationItem = ResourceManager.instance.GetCustomizationItemResourceByID(id);

                ApplyItem(customizationItem);
            }
        }
    }

    public void ApplyItem(CustomizationItemSO item)
    {
        var slot = customizationSlots.Find(s => s.category == item.category);
        if (slot == null) return;

        slot.selectedItem = item;

        switch (item.type)
        {
            case CustomizationType.MeshSwap:

                if(item.meshToSwap == null)
                {
                    Debug.Log("Applied Mesh, But Mesh is Not Assigned");
                    return;
                }

                if (slot.meshFilter)
                    slot.meshFilter.sharedMesh = item.meshToSwap;
                if (slot.skinnedMeshRenderer)
                    slot.skinnedMeshRenderer.sharedMesh = item.meshToSwap;
                break;

            case CustomizationType.ObjectSwap:

                if (item.prefabToSwap == null)
                {
                    Debug.Log("Swapped Object, Prefab is Not Assigned");
                    return;
                }

                foreach (Transform child in slot.slotTransform) Destroy(child.gameObject);
                Instantiate(item.prefabToSwap, slot.slotTransform);
                break;

            case CustomizationType.MaterialColor:

                Debug.Log("Colore Applied!");
                
                foreach (var mat in slot.materials)
                    mat.color = item.colorToApply;
                break;

            case CustomizationType.TextureSwap:

                if (item.materialToApply == null)
                {
                    Debug.Log("Applied Material, But Material is Not Assigned");
                    return;
                }

                foreach (var mat in slot.materials)
                    mat.mainTexture = item.materialToApply.mainTexture;
                break;
        }

        MarkDirty();
    }

    #endregion Main

    #region SAVE/LOAD

    public override CharacterCustomizeState Capture()
    {
        characterCustomizeState = new CharacterCustomizeState();

        foreach (CustomizationSlot customizationSlot in customizationSlots)
        {
            characterCustomizeState.assignedItemID.Add(customizationSlot.selectedItem.itemID);
        }

        return characterCustomizeState;
    }

    public override void Restore(CharacterCustomizeState state)
    {
        characterCustomizeState = state;
    }

    #endregion SAVE/LOAD
}
