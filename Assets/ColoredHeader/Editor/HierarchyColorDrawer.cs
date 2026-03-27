using UnityEditor;
using UnityEngine;
using ColoredHeader.Runtime;

namespace ColoredHeader.Editor
{
    [InitializeOnLoad]
    public static class HierarchyColorDrawer
    {
        private static readonly Color DefaultBackgroundColor = new Color(0.19f, 0.19f, 0.19f);

        static HierarchyColorDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
#if UNITY_2023_1_OR_NEWER
            var obj = EditorUtility.EntityIdToObject(instanceID) as GameObject;
#else
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
#endif
            if (obj == null) return;

            // Offset for the default icon (usually 16x16)
            var iconOffset = 18f;
            var labelRect = new Rect(selectionRect);
            labelRect.xMin += iconOffset;

            var colorComp = obj.GetComponent<ColorInHierarchy>();

            // --- Header Formatting ---
            if (obj.name.StartsWith("#"))
            {
                // Background color
                var backColor = colorComp != null ? colorComp.Color : (EditorGUIUtility.isProSkin 
                    ? new Color(0.22f, 0.22f, 0.22f, 1f) 
                    : new Color(0.7f, 0.7f, 0.7f, 1f));
                
                // Draw background only for label area
                EditorGUI.DrawRect(labelRect, backColor);

                // Determine text color based on background brightness
                var luminance = 0.2126f * backColor.r + 0.7152f * backColor.g + 0.0722f * backColor.b;
                var textColor = luminance > 0.5f ? Color.black : Color.white;

                var style = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = textColor }
                };

                var headerText = obj.name.TrimStart('#').Trim();
                EditorGUI.LabelField(labelRect, headerText, style);
                return;
            }

            // --- ColorInHierarchy ---
            if (colorComp != null)
            {
                // Draw custom background color
                EditorGUI.DrawRect(labelRect, colorComp.Color);

                // Determine text color based on background brightness
                var luminance = 0.2126f * colorComp.Color.r + 0.7152f * colorComp.Color.g + 0.0722f * colorComp.Color.b;
                var textColor = luminance > 0.5f ? Color.black : Color.white;

                var style = new GUIStyle(EditorStyles.label)
                {
                    normal = { textColor = textColor }
                };

                EditorGUI.LabelField(labelRect, obj.name, style);
            }
        }
    }
}
