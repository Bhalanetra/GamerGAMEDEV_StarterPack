using UnityEditor;
using UnityEngine;
using System.IO;
using System;
using UnityEditor.ProjectWindowCallback;

public class CreateDerivedScriptEditor
{
    [MenuItem("Assets/GamerGAMEDEV/Create/Derived Script", true)]
    private static bool ValidateCreateDerivedScript()
    {
        return Selection.activeObject is MonoScript selectedScript && selectedScript.GetClass() != null;
    }

    [MenuItem("Assets/GamerGAMEDEV/Create/Derived Script", false, 1)]
    private static void CreateDerivedScript()
    {
        MonoScript baseScript = Selection.activeObject as MonoScript;
        if (baseScript == null || baseScript.GetClass() == null)
        {
            Debug.LogError("Please select a valid script class.");
            return;
        }

        Type baseType = baseScript.GetClass();
        string baseClassName = baseType.Name;
        string defaultNewClassName = "New" + baseClassName;
        string namespaceName = baseType.Namespace;

        string folderPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(baseScript));
        string tempFilePath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(folderPath, defaultNewClassName + ".cs"));

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            EndNameEditActionInstance.CreateInstance(baseClassName, namespaceName),
            tempFilePath,
            EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
            null
        );
    }

    private class EndNameEditActionInstance : EndNameEditAction
    {
        private string baseClassName;
        private string namespaceName;

        public static EndNameEditActionInstance CreateInstance(string baseClassName, string namespaceName)
        {
            var instance = CreateInstance<EndNameEditActionInstance>();
            instance.baseClassName = baseClassName;
            instance.namespaceName = namespaceName;
            return instance;
        }

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            string className = Path.GetFileNameWithoutExtension(pathName);
            className = SanitizeClassName(className);

            //Choose the template dynamically based on base class
            string templateFile = baseClassName == "ScriptableObject" ? "GamerGAMEDEV_DerivedClass_ScriptableObject_Template.txt" : "GamerGAMEDEV_DerivedClass_Mono_Template.txt";
            string templatePath = $"Assets/GamerGAMEDEV/CustomScripts/Templates/{templateFile}";

            if (!File.Exists(templatePath))
            {
                Debug.LogError("Template file not found at: " + templatePath);
                return;
            }

            string template = File.ReadAllText(templatePath);

            // Prepare namespace line
            string namespaceLine = string.IsNullOrEmpty(namespaceName) ? "" : $"using {namespaceName};";

            // Replace placeholders
            string finalScript = template
                .Replace("#SCRIPTNAME#", className)
                .Replace("#BASECLASS#", baseClassName)
                .Replace("#NAMESPACE#", namespaceLine);

            File.WriteAllText(pathName, finalScript);
            AssetDatabase.ImportAsset(pathName);
            AssetDatabase.Refresh();

            MonoScript newScript = AssetDatabase.LoadAssetAtPath<MonoScript>(pathName);
            Selection.activeObject = newScript;
        }

        private string SanitizeClassName(string input)
        {
            // Remove invalid characters
            input = input.Replace(" ", "").Replace("-", "").Replace(".", "");
            if (string.IsNullOrEmpty(input))
                input = "NewClass";
            if (!char.IsLetter(input[0]) && input[0] != '_')
                input = "_" + input;
            return input;
        }
    }
}
