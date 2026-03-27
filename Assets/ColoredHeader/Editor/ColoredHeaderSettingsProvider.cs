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
            EditorGUILayout.LabelField("Colored Header Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // AutoStatic
            ColoredHeaderSettings.AutoStatic = EditorGUILayout.Toggle("Auto Static", ColoredHeaderSettings.AutoStatic);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Categories", EditorStyles.boldLabel);

            var categories = ColoredHeaderSettings.Categories;
            for (int i = 0; i < categories.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                categories[i] = new ColoredHeaderSettings.CategoryInfo
                {
                    Name = EditorGUILayout.TextField(categories[i].Name),
                    Color = EditorGUILayout.ColorField(categories[i].Color)
                };
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

            // Save changes
            if (GUI.changed)
            {
                ColoredHeaderSettings.Categories = categories;
            }
        }
    }
}
