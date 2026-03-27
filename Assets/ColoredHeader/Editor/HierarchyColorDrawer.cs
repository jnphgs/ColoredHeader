using UnityEditor;
using UnityEngine;
using ColoredHeader.Runtime;

namespace ColoredHeader.Editor
{
    [InitializeOnLoad]
    public static class HierarchyColorDrawer
    {
        static HierarchyColorDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            // TODO: Change to use EditorUtility.EntityIdToObject if Unity LTS fully migrates to it.
            // var obj = EditorUtility.EntityIdToObject(instanceID) as GameObject;
#pragma warning disable CS0618
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
#pragma warning restore CS0618
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
                var backColor = colorComp != null ? colorComp.color : (EditorGUIUtility.isProSkin 
                    ? new Color(0.22f, 0.22f, 0.22f, 1f) 
                    : new Color(0.7f, 0.7f, 0.7f, 1f));
                
                // Draw background only for the label area
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
            if (colorComp)
            {
                // Draw custom background color
                EditorGUI.DrawRect(labelRect, colorComp.color);

                // Determine text color based on background brightness
                var luminance = 0.2126f * colorComp.color.r + 0.7152f * colorComp.color.g + 0.0722f * colorComp.color.b;
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
