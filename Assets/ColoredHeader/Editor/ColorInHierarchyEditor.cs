using System.Linq;
using ColoredHeader.Runtime;
using UnityEditor;

namespace ColoredHeader.Editor
{
    [CustomEditor(typeof(ColorInHierarchy))]
    public class ColorInHierarchyEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var script = (ColorInHierarchy)target;
            var settings = ColoredHeaderSettings.Instance;

            serializedObject.Update();

            if (settings != null && settings.Categories.Count > 0)
            {
                var names = settings.Categories.Select(c => c.Name).ToList();
                names.Insert(0, "None");

                int currentIndex = names.IndexOf(script.CategoryName);
                if (currentIndex < 0) currentIndex = 0;

                int newIndex = EditorGUILayout.Popup("Category", currentIndex, names.ToArray());
                if (newIndex != currentIndex)
                {
                    script.CategoryName = newIndex == 0 ? "" : names[newIndex];
                    EditorUtility.SetDirty(script);
                    // Force OnValidate to update the color
                    var onValidate = script.GetType().GetMethod("OnValidate", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    onValidate?.Invoke(script, null);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("ColoredHeaderSettings not found or empty. Please set it up in Project Settings.", MessageType.Warning);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CategoryName"));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Color"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
