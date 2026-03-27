using UnityEditor;
using ColoredHeader.Runtime;

namespace ColoredHeader.Editor
{
    [InitializeOnLoad]
    public static class HeaderStaticAutoSetter
    {
        static HeaderStaticAutoSetter()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private static void OnHierarchyChanged()
        {
            var settings = ColoredHeaderSettings.Instance;
            if (settings == null || !settings.autoStatic) return;

            // Simple check: iterate over selected or all root objects?
            // To be efficient, we might only check objects that were just changed, 
            // but hierarchyChanged doesn't tell us which ones.
            // As a compromise, we'll check the active object if it's new/changed.
            
            var obj = Selection.activeGameObject;
            if (obj && obj.name.StartsWith("#") && !obj.isStatic)
            {
                obj.isStatic = true;
                // No need to set dirty here as Unity handles isStatic change
            }
        }
    }
}
