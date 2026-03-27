using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ColoredHeader.Runtime;

namespace ColoredHeader.Editor
{
    public static class ColoredHeaderSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("Project/Colored Header", SettingsScope.Project)
            {
                label = "Colored Header",
                guiHandler = DrawSettingsGUI,
                keywords = new HashSet<string>(new[] { "Colored", "Header", "Hierarchy", "Color" })
            };

            return provider;
        }

        private static void DrawSettingsGUI(string searchContext)
        {
            var settings = ColoredHeaderSettings.Instance;
            if (settings == null)
            {
                EditorGUILayout.HelpBox("Could not load ColoredHeaderSettings.", MessageType.Error);
                return;
            }

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Colored Header Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // AutoStatic
            settings.AutoStatic = EditorGUILayout.Toggle("Auto Static", settings.AutoStatic);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Categories", EditorStyles.boldLabel);

            var categories = settings.Categories;
            for (int i = 0; i < categories.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                var category = categories[i];
                category.Name = EditorGUILayout.TextField(category.Name);
                category.Color = EditorGUILayout.ColorField(category.Color);
                categories[i] = category;

                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    categories.RemoveAt(i);
                    i--;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Category"))
            {
                categories.Add(new ColoredHeaderSettings.CategoryInfo { Name = "New Category", Color = Color.white });
            }

            if (EditorGUI.EndChangeCheck())
            {
                settings.Save();
            }
        }
    }
}
