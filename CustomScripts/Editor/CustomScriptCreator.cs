using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.ProjectWindowCallback;

public class CustomScriptCreator
{
    private const string TEMPLATE_FOLDER_PATH = "Assets/GamerGAMEDEV/CustomScripts/Templates/";
    private const string DEFAULT_TEMPLATE = "GamerGAMEDEV_Custom_Mono_Template.txt";

    [MenuItem("Assets/GamerGAMEDEV/Create/Custom C# Script", false)]
    public static void CreateScript()
    {
        string templatePath = Path.Combine(TEMPLATE_FOLDER_PATH, DEFAULT_TEMPLATE);
        if (!File.Exists(templatePath))
        {
            Debug.LogError($"Template file not found at path: {templatePath}");
            return;
        }

        string folderPath = GetSelectedPathOrFallback();
        string defaultFileName = "NewCustomScript.cs";
        string uniquePath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(folderPath, defaultFileName));

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            EndNameEditActionCustom.CreateInstance(templatePath),
            uniquePath,
            EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
            null
        );
    }

    private static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        UnityEngine.Object obj = Selection.activeObject;
        if (obj != null)
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (File.Exists(path))
                path = Path.GetDirectoryName(path);
        }
        return path;
    }

    private class EndNameEditActionCustom : EndNameEditAction
    {
        private string templatePath;

        public static EndNameEditActionCustom CreateInstance(string templatePath)
        {
            var instance = CreateInstance<EndNameEditActionCustom>();
            instance.templatePath = templatePath;
            return instance;
        }

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            string className = Path.GetFileNameWithoutExtension(pathName);
            className = SanitizeClassName(className);

            if (!File.Exists(templatePath))
            {
                Debug.LogError($"Template file not found at: {templatePath}");
                return;
            }

            string template = File.ReadAllText(templatePath);

            // Replace placeholders
            string finalScript = template
                .Replace("#SCRIPTNAME#", className)
                .Replace("#NAMESPACE#", ""); // You can customize namespace handling

            File.WriteAllText(pathName, finalScript);
            AssetDatabase.ImportAsset(pathName);
            AssetDatabase.Refresh();

            MonoScript newScript = AssetDatabase.LoadAssetAtPath<MonoScript>(pathName);
            Selection.activeObject = newScript;
        }

        private string SanitizeClassName(string input)
        {
            input = input.Replace(" ", "").Replace("-", "").Replace(".", "");
            if (string.IsNullOrEmpty(input))
                input = "NewClass";
            if (!char.IsLetter(input[0]) && input[0] != '_')
                input = "_" + input;
            return input;
        }
    }
}
