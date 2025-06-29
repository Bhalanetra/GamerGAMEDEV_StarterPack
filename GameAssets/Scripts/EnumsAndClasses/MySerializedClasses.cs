using System;
using System.Collections.Generic;
using UnityEngine;

public class MySerializedClasses
{
    
}

namespace GamerGAMEDEV
{
    namespace Resources
    {
        [Serializable]
        public class CustomizationInventoryItem
        {
            public CustomizationItemSO item;

            public CustomizationInventoryItem(CustomizationItemSO item)
            {
                this.item = item;
            }
        }

        [Serializable]
        public class InventoryItem
        {
            public ItemSO item;
            public bool isEquipped = false;

            public InventoryItem(ItemSO item, bool isEquipped)
            {
                this.item = item;
                this.isEquipped = isEquipped;
            }
        }

        [Serializable]
        public class SpritesCollection
        {
            public Sprite _coin;
            public Sprite _gem;
        }

        [Serializable]
        public class ItemsCollection
        {
            public List<ItemSO_Base> items;
            public List<CustomizationItemSO> customizationItems;
        }
    }

    namespace Screens
    {
        [Serializable]
        public class ScreenCallback
        {
            
        }

        [Serializable]
        public class MapCategories
        {
            public MapCategorySO[] categories;
        }

        [Serializable]
        public class ShopItemCategories
        {
            public ShopCategorySO[] categories;
        }

        [Serializable]
        public class  InventoryItemCategories
        {
            public InventoryCategorySO[] categories;
        }
    }

    namespace SettingsProperties
    {
        [Serializable]
        public class ToggleAndSliderActionHolder
        {
            public Action<bool> OnToggleValueChanged;
            public Action<float> OnSliderValueChanged;
        }

    }

    namespace Interfaces
    {
        // ISavable.cs
        public interface ISavable
        {
            /// Unique key that never changes at runtime.
            string SaveId { get; }

            /// True if the in‑memory state deviates from last save.
            bool IsDirty { get; }

            /// Return a DTO (pure data struct / class) to be serialised.
            object CaptureState();

            /// Push persisted data back into the live object.
            void RestoreState(object state);

            /// Flip IsDirty off after a successful save.
            void ClearDirtyFlag();
        }

    }

    namespace States
    {
        [Serializable]
        public class CategorySelectionButtonState
        {
            public string id;
        }

        [Serializable]
        public class InventoryState
        {
            public List<ItemState> inventoryItemsState = new List<ItemState>();

            public List<string> skinInventoryState = new List<string>();
        }

        [Serializable]
        public class ItemState
        {
            public string id;
            public bool equipped = false;

            public ItemState(string id, bool equipped)
            {
                this.id = id;
                this.equipped = equipped;
            }
        }

        [Serializable]
        public class GameState
        {
            public int coin = 150;
            public int gems = 15;
            public int highscore = 0;
            public PlayerState playerState;
        }


        [Serializable]
        public class PlayerState
        {
            public int playerLevel = 1;
            public int playerExp = 0;
        }
    }

    namespace Customization
    {
        [Serializable]
        public class CustomizationSlot
        {
            public CustomizationCategory category;
            public Transform slotTransform; // For ObjectSwaps
            public SkinnedMeshRenderer skinnedMeshRenderer; // For Gear Mesh
            public MeshFilter meshFilter; // For static mesh objects
            public Material[] materials; // Optional override
            public CustomizationItemSO selectedItem;
        }

        [Serializable]
        public class CharacterCustomizeState
        {
            public List<string> assignedItemID = new List<string>();
        }
    }
}


