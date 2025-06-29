using UnityEngine;


public class ItemSO_Base : ScriptableObject
{
    [ReadOnly]
    public string itemID;

    private void OnEnable()
    {
        itemID = this.name;
    }
}
