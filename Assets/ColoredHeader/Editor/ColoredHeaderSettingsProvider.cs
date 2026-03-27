using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using ColoredHeader.Runtime;

namespace ColoredHeader.Editor
{
    public static class ColoredHeaderSettingsProvider
    {
        private const string SettingsPath = "Assets/ColoredHeader/Resources/ColoredHeaderSettings.asset";

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("Project/Colored Header", SettingsScope.Project)
            {
                label = "Colored Header",
                guiHandler = (searchContext) =>
                {
                    var settings = GetOrCreateSettings();
                    var editor = UnityEditor.Editor.CreateEditor(settings);
                    editor.OnInspectorGUI();
                },
                keywords = new HashSet<string>(new[] { "Colored", "Header", "Hierarchy", "Color" })
            };

            return provider;
        }

        private static ColoredHeaderSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<ColoredHeaderSettings>("ColoredHeaderSettings");
            if (settings != null) return settings;

            // Search for existing settings
            var assets = AssetDatabase.FindAssets("t:ColoredHeaderSettings");
            if (assets.Length > 0)
            {
                var path = AssetDatabase.GUIDToAssetPath(assets[0]);
                return AssetDatabase.LoadAssetAtPath<ColoredHeaderSettings>(path);
            }

            // Create new settings if not found
            var directory = Path.GetDirectoryName(SettingsPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            settings = ScriptableObject.CreateInstance<ColoredHeaderSettings>();
            AssetDatabase.CreateAsset(settings, SettingsPath);
            AssetDatabase.SaveAssets();
            return settings;
        }
    }
}
