using System;
using GamerGAMEDEV.Interfaces;
using UnityEngine;

public abstract class SavableMonoBehaviour<TState> : MonoBehaviour, ISavable
{
    [SerializeField] private string saveId = Guid.NewGuid().ToString();
    public string SaveId => saveId;

    [NonSerialized] public bool IsDirty = true;
    bool ISavable.IsDirty => IsDirty;

    protected void MarkDirty() => IsDirty = true;

    public abstract TState Capture();

    public abstract void Restore(TState state);

    #region ISaveable
    object ISavable.CaptureState() => Capture();
    void ISavable.RestoreState(object state) => Restore((TState)state);
    void ISavable.ClearDirtyFlag() => IsDirty = false;
    #endregion
}
