#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class EnumEditorWindow : EditorWindow
{
    private List<string> enumNames = new List<string>();
    private Dictionary<string, string> enumNameToPath = new Dictionary<string, string>();

    private string searchFilter = "";
    private int selectedEnumIndex = 0;
    private string selectedEnum => filteredEnumNames.Count > 0 ? filteredEnumNames[selectedEnumIndex] : "";
    private List<string> filteredEnumNames => enumNames.Where(n => string.IsNullOrEmpty(searchFilter) || n.ToLower().Contains(searchFilter.ToLower())).ToList();

    private List<string> newEnumValues = new List<string>() { "" };
    private List<bool> selectedValuesToRemove = new List<bool>();

    private string renameOldValue = "";
    private string renameNewValue = "";

    private string newEnumName = "";
    private string newEnumNamespace = "Game.Enums";
    private MonoScript targetScriptForNewEnum;

    private string renameEnumName = "";
    private Vector2 scroll;

    [MenuItem("GamerGAMEDEV/Tools/Enum Editor")]
    public static void ShowWindow() => GetWindow<EnumEditorWindow>("Enum Editor");

    private void OnEnable() => RefreshEnums();

    void RefreshEnums()
    {
        enumNames.Clear();
        enumNameToPath.Clear();
        selectedValuesToRemove.Clear();

        string[] files = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            string content = File.ReadAllText(file);
            MatchCollection matches = Regex.Matches(content, @"enum\s+(\w+)");
            foreach (Match match in matches)
            {
                string name = match.Groups[1].Value;
                if (!enumNames.Contains(name))
                {
                    string relativePath = "Assets" + file.Replace(Application.dataPath, "").Replace('\\', '/');
                    enumNames.Add(name);
                    enumNameToPath[name] = relativePath;
                }
            }
        }
    }

    void OnGUI()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll);
        GUILayout.Space(10);

        EditorGUILayout.LabelField("SEARCH ENUMS", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Filter", searchFilter);

        if (filteredEnumNames.Count == 0)
        {
            EditorGUILayout.HelpBox("No enums found in the project.", MessageType.Warning);
            EditorGUILayout.EndScrollView();
            return;
        }

        selectedEnumIndex = EditorGUILayout.Popup("Select Enum", selectedEnumIndex, filteredEnumNames.ToArray());
        if (!string.IsNullOrEmpty(selectedEnum))
        {
            EditorGUILayout.LabelField("Enum Path:", enumNameToPath[selectedEnum]);

            if (GUILayout.Button("Open Enum File"))
            {
                UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(enumNameToPath[selectedEnum], 1);
            }
        }

        GUILayout.Space(10);
        DrawAddValuesSection();
        GUILayout.Space(10);
        DrawRemoveValuesSection();
        GUILayout.Space(10);
        DrawRenameValueSection();
        GUILayout.Space(10);
        DrawRenameEnumSection();
        GUILayout.Space(10);
        DrawCreateEnumSection();
        GUILayout.Space(10);
        DrawDeleteEnumSection();

        EditorGUILayout.EndScrollView();
    }

    void DrawAddValuesSection()
    {
        EditorGUILayout.LabelField("ADD ENUM VALUES", EditorStyles.boldLabel);
        for (int i = 0; i < newEnumValues.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            newEnumValues[i] = EditorGUILayout.TextField($"Value {i + 1}", newEnumValues[i]);
            if (GUILayout.Button("-", GUILayout.Width(25)) && newEnumValues.Count > 1)
                newEnumValues.RemoveAt(i);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("+ Add Field"))
            newEnumValues.Add("");

        if (newEnumValues.Any(v => string.IsNullOrWhiteSpace(v)))
        {
            EditorGUILayout.HelpBox("All fields must be filled before adding.", MessageType.Warning);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Add Values") && !string.IsNullOrEmpty(selectedEnum))
        {
            foreach (var val in newEnumValues.Where(v => !EnumValueExists(selectedEnum, v)))
            {
                AddEnumValue(selectedEnum, val);
            }
            newEnumValues = new List<string>() { "" };
        }

        GUI.enabled = true;
    }

    void DrawRemoveValuesSection()
    {
        EditorGUILayout.LabelField("REMOVE ENUM VALUES", EditorStyles.boldLabel);
        string[] enumValues = GetEnumValues(selectedEnum);

        if (selectedValuesToRemove.Count != enumValues.Length)
            selectedValuesToRemove = new List<bool>(new bool[enumValues.Length]);

        for (int i = 0; i < enumValues.Length; i++)
        {
            selectedValuesToRemove[i] = EditorGUILayout.ToggleLeft(enumValues[i], selectedValuesToRemove[i]);
        }

        if (GUILayout.Button("Remove Selected Values"))
        {
            for (int i = enumValues.Length - 1; i >= 0; i--)
            {
                if (selectedValuesToRemove[i])
                    RemoveEnumValue(selectedEnum, enumValues[i]);
            }
            selectedValuesToRemove = new List<bool>(new bool[GetEnumValues(selectedEnum).Length]);
        }
    }

    void DrawRenameValueSection()
    {
        EditorGUILayout.LabelField("RENAME ENUM VALUE", EditorStyles.boldLabel);
        string[] enumValues = GetEnumValues(selectedEnum);
        int oldIndex = Array.IndexOf(enumValues, renameOldValue);
        oldIndex = EditorGUILayout.Popup("Old Value", oldIndex, enumValues);
        if (oldIndex >= 0 && oldIndex < enumValues.Length)
            renameOldValue = enumValues[oldIndex];

        renameNewValue = EditorGUILayout.TextField("New Value", renameNewValue);

        if (!string.IsNullOrEmpty(renameNewValue) && EnumValueExists(selectedEnum, renameNewValue))
        {
            EditorGUILayout.HelpBox("This value already exists.", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Rename"))
        {
            RenameEnumValue(selectedEnum, renameOldValue, renameNewValue);
            renameOldValue = renameNewValue = "";
        }

        GUI.enabled = true;
    }

    void DrawRenameEnumSection()
    {
        EditorGUILayout.LabelField("RENAME ENUM TYPE", EditorStyles.boldLabel);
        renameEnumName = EditorGUILayout.TextField("New Enum Name", renameEnumName);

        if (enumNames.Contains(renameEnumName))
        {
            EditorGUILayout.HelpBox("Enum name already exists.", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Rename Enum"))
        {
            RenameEnumType(selectedEnum, renameEnumName);
            renameEnumName = "";
        }

        GUI.enabled = true;
    }

    void DrawCreateEnumSection()
    {
        EditorGUILayout.LabelField("CREATE NEW ENUM", EditorStyles.boldLabel);
        newEnumName = EditorGUILayout.TextField("Enum Name", newEnumName);
        //newEnumNamespace = EditorGUILayout.TextField("Namespace", newEnumNamespace);
        newEnumNamespace = EditorGUILayout.TextField("Namespace (Optional)", newEnumNamespace);
        targetScriptForNewEnum = (MonoScript)EditorGUILayout.ObjectField("Target Script", targetScriptForNewEnum, typeof(MonoScript), false);

        if (enumNames.Contains(newEnumName))
        {
            EditorGUILayout.HelpBox("Enum already exists.", MessageType.Error);
            GUI.enabled = false;
        }

        if (GUILayout.Button("Create Enum"))
        {
            CreateNewEnum(newEnumName, newEnumNamespace, targetScriptForNewEnum);
            newEnumName = "";
        }

        GUI.enabled = true;
    }

    void DrawDeleteEnumSection()
    {
        EditorGUILayout.LabelField("DELETE ENUM", EditorStyles.boldLabel);
        if (GUILayout.Button("Delete Enum"))
        {
            if (EditorUtility.DisplayDialog("Confirm", $"Delete enum '{selectedEnum}' permanently?", "Yes", "Cancel"))
                DeleteEnum(selectedEnum);
        }
    }

    bool EnumValueExists(string enumName, string valueName)
    {
        string[] values = GetEnumValues(enumName);
        return values.Contains(valueName);
    }

    string[] GetEnumValues(string enumName)
    {
        if (string.IsNullOrEmpty(enumName) || !enumNameToPath.ContainsKey(enumName)) return new string[0];
        string content = File.ReadAllText(enumNameToPath[enumName]);
        Match match = Regex.Match(content, $@"enum\s+{enumName}\s*\{{([^}}]+)\}}");
        if (!match.Success) return new string[0];
        return match.Groups[1].Value.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
    }

    void AddEnumValue(string enumName, string valueName)
    {
        string path = enumNameToPath[enumName];
        string content = File.ReadAllText(path);

        content = Regex.Replace(content, $@"(enum\s+{enumName}\s*\{{)([^}}]*)(\}})", m =>
        {
            string inside = m.Groups[2].Value.TrimEnd();
            string updated = string.IsNullOrEmpty(inside) ? valueName : $"{inside},\n    {valueName}";
            return $"{m.Groups[1].Value}\n    {updated}\n{m.Groups[3].Value}";
        });

        File.WriteAllText(path, content);
        AssetDatabase.Refresh();
        RefreshEnums();
    }

    void RemoveEnumValue(string enumName, string valueName)
    {
        string path = enumNameToPath[enumName];
        string content = File.ReadAllText(path);

        content = Regex.Replace(content, $@"(enum\s+{enumName}\s*\{{)([^}}]*)(\}})", m =>
        {
            var parts = m.Groups[2].Value.Split(',').Select(p => p.Trim()).Where(p => p != valueName && !string.IsNullOrEmpty(p));
            return $"{m.Groups[1].Value}\n    {string.Join(",\n    ", parts)}\n{m.Groups[3].Value}";
        });

        File.WriteAllText(path, content);
        AssetDatabase.Refresh();
    }

    void RenameEnumValue(string enumName, string oldValue, string newValue)
    {
        if (EnumValueExists(enumName, newValue)) return;

        string path = enumNameToPath[enumName];
        string content = File.ReadAllText(path).Replace(oldValue, newValue);
        File.WriteAllText(path, content);
        AssetDatabase.Refresh();
    }

    void RenameEnumType(string oldName, string newName)
    {
        if (enumNameToPath.TryGetValue(oldName, out string path))
        {
            string content = File.ReadAllText(path).Replace($"enum {oldName}", $"enum {newName}");
            File.WriteAllText(path, content);
            AssetDatabase.Refresh();
            RefreshEnums();
        }
    }

    void CreateNewEnum(string enumName, string ns, MonoScript scriptTarget)
    {
        string targetPath = scriptTarget ? AssetDatabase.GetAssetPath(scriptTarget) : "Assets/GamerGAMEDEV/Scripts/MyEnums.cs";

        if (!File.Exists(targetPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            File.WriteAllText(targetPath, "// Auto-generated enum file\n");
        }

        //string template = $"\nnamespace {ns} {{\n    public enum {enumName} {{\n        Value1\n    }}\n}}\n";

        string template;
        if (!string.IsNullOrWhiteSpace(ns))
        {
            template = $"\nnamespace {ns} {{\n    public enum {enumName} {{\n        Value1\n    }}\n}}\n";
        }
        else
        {
            template = $"\npublic enum {enumName} {{\n    Value1\n}}\n";
        }

        File.AppendAllText(targetPath, template);
        AssetDatabase.Refresh();
        RefreshEnums();
    }

    void DeleteEnum(string enumName)
    {
        string path = enumNameToPath[enumName];
        string content = File.ReadAllText(path);
        content = Regex.Replace(content, $@"public\s+enum\s+{enumName}\s*\{{[^}}]*\}}", "", RegexOptions.Singleline);
        File.WriteAllText(path, content);
        AssetDatabase.Refresh();
        RefreshEnums();
    }
}
#endif
