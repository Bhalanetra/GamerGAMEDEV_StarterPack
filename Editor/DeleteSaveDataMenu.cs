// File: Assets/Editor/DeleteSaveDataMenu.cs
using UnityEditor;
using UnityEngine;
using System.IO;

public static class DeleteSaveDataMenu
{
    private const string fileName = "savegame.json";

    [MenuItem("GamerGAMEDEV/Saved Data/Delete Json Data")]
    public static void DeleteSaveFile()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"[DeleteSaveData] Deleted save file at: {path}");
            EditorUtility.DisplayDialog("Delete Save", "Save data deleted successfully!", "OK");
        }
        else
        {
            Debug.LogWarning("[DeleteSaveData] No save file found to delete.");
            EditorUtility.DisplayDialog("Delete Save", "No save file found to delete.", "OK");
        }
    }
}
