// SaveManager.cs
using System.Collections.Generic;
using System.IO;
using GamerGAMEDEV.Interfaces;
//using System.Xml;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    private const string FILE = "savegame.json";
    public static SaveManager Instance { get; private set; }

    private readonly Dictionary<string, ISavable> registry = new();

    public static Action OnSaveLoadComplete;

    public static Action OnLoadindComplete;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region Registration
    public static void Register(ISavable s)
    {
        Debug.Log($"{Instance.gameObject.name} && {s.SaveId} && {s}");
        if (!Instance.registry.ContainsKey(s.SaveId))
            Instance.registry.Add(s.SaveId, s);
    }
    public static void Unregister(ISavable s)
    {
        if (Instance != null) Instance.registry.Remove(s.SaveId);
    }
    #endregion

    #region Public API
    public static void SaveAll(Action OnSaveComplete = null) => Instance.InternalSave(onlyDirty: false, OnSaveComplete);
    public static void Load(Action OnLoadComplete = null) => Instance.InternalLoad(OnLoadComplete);
    #endregion
    //public static void SaveModified(Action OnSaveComplete = null) => Instance.InternalSave(onlyDirty: true, OnSaveComplete);

    public static void SaveModified(Action OnSaveComplete = null) =>
    SaveModifiedPreservingOthers(OnSaveComplete);

    #region Implementation
    private class Packet
    {
        public string SaveId;
        public string Type;   // assembly‑qualified name
        public object Data;
    }

    private void InternalSave(bool onlyDirty, Action OnSaveComplete = null)
    {
        var packets = new List<Packet>();

        foreach (var s in registry.Values)
        {
            if (onlyDirty && !s.IsDirty) continue;
            packets.Add(new Packet
            {
                SaveId = s.SaveId,
                Type = s.CaptureState().GetType().AssemblyQualifiedName,
                Data = s.CaptureState()
            });
            s.ClearDirtyFlag();
        }

        var json = JsonConvert.SerializeObject(packets, Formatting.Indented);
        var path = Path.Combine(Application.persistentDataPath, FILE);
        File.WriteAllText(path, json);
#if UNITY_EDITOR
        Debug.Log($"[SaveManager] Wrote {packets.Count} packets to {path}");
#endif

        OnSaveComplete?.Invoke();
        OnSaveLoadComplete?.Invoke();
    }

    private void InternalLoad(Action OnLoadingComplete = null)
    {
        var path = Path.Combine(Application.persistentDataPath, FILE);
        if (!File.Exists(path))
        {
            Debug.LogWarning("[SaveManager] No save file found.");
            OnLoadingComplete?.Invoke();
            return;
        }

        var json = File.ReadAllText(path);
        var packets = JsonConvert.DeserializeObject<List<Packet>>(json);

        // 1. Build a lookup so restoration order doesn't matter
        var byId = new Dictionary<string, Packet>();
        foreach (var p in packets) byId[p.SaveId] = p;

        // 2. Feed data back into live objects
        foreach (var s in registry.Values)
        {
            if (byId.TryGetValue(s.SaveId, out var p))
            {
                var concreteType = System.Type.GetType(p.Type);
                var stateObj = JsonConvert.DeserializeObject(
                                        p.Data.ToString(), concreteType);
                s.RestoreState(stateObj);
                s.ClearDirtyFlag();
            }
        }

        OnLoadingComplete?.Invoke();
        OnSaveLoadComplete?.Invoke();
        OnLoadindComplete?.Invoke();
    }

    public static void LoadSingle(ISavable s, Action OnLoadComplete = null)
    {
        string path = Path.Combine(Application.persistentDataPath, FILE);
        if (!File.Exists(path))
        {
            Debug.LogWarning($"[SaveManager] No save file found to load single object ({s.SaveId})");
            OnLoadComplete?.Invoke();
            return;
        }

        try
        {
            string json = File.ReadAllText(path);
            var packets = JsonConvert.DeserializeObject<List<Packet>>(json);

            if (packets == null || packets.Count == 0)
            {
                Debug.LogWarning($"[SaveManager] Save file empty or corrupt while loading {s.SaveId}");
                return;
            }

            var packet = packets.FirstOrDefault(p => p.SaveId == s.SaveId);

            if (packet != null)
            {
                var concreteType = Type.GetType(packet.Type);
                var stateObj = JsonConvert.DeserializeObject(packet.Data.ToString(), concreteType);

                s.RestoreState(stateObj);
                s.ClearDirtyFlag();

#if UNITY_EDITOR
                Debug.Log($"[SaveManager] Loaded data for: {s.SaveId}");
#endif
                OnLoadComplete?.Invoke();
            }
            else
            {
                Debug.LogWarning($"[SaveManager] No saved data found for: {s.SaveId}");
                OnLoadComplete?.Invoke();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SaveManager] Failed to load single savable ({s.SaveId}): {ex.Message}");
            OnLoadComplete?.Invoke();
        }
    }

    private static void SaveModifiedPreservingOthers(Action OnSaveComplete = null)
    {
        string path = Path.Combine(Application.persistentDataPath, FILE);

        List<Packet> existingPackets = new();

        if (File.Exists(path))
        {
            try
            {
                string existingJson = File.ReadAllText(path);
                existingPackets = JsonConvert.DeserializeObject<List<Packet>>(existingJson)
                                  ?? new List<Packet>();
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveManager] Failed to read existing save file: {e}");
            }
        }

        // Dictionary to merge new data
        Dictionary<string, Packet> packetDict = existingPackets
            .ToDictionary(p => p.SaveId, p => p);

        int modifiedCount = 0;

        foreach (var s in Instance.registry.Values)
        {
            if (!s.IsDirty)
                continue;

            var state = s.CaptureState();
            var packet = new Packet
            {
                SaveId = s.SaveId,
                Type = state.GetType().AssemblyQualifiedName,
                Data = state
            };

            packetDict[s.SaveId] = packet;
            s.ClearDirtyFlag();
            modifiedCount++;
        }

        var mergedList = packetDict.Values.ToList();
        string updatedJson = JsonConvert.SerializeObject(mergedList, Formatting.Indented);
        File.WriteAllText(path, updatedJson);

#if UNITY_EDITOR
        Debug.Log($"[SaveManager] Updated {modifiedCount}, {packetDict.Keys} modified savables (merged into {mergedList.Count} total objects).");
#endif

        OnSaveComplete?.Invoke();
        OnSaveLoadComplete?.Invoke();
    }

    #endregion
}
