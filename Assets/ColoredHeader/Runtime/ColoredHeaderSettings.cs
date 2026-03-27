using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ColoredHeader.Runtime
{
    public static class ColoredHeaderSettings
    {
        [Serializable]
        public struct CategoryInfo
        {
            public string Name;
            public Color Color;
        }

        private const string AutoStaticKey = "ColoredHeader_AutoStatic";
        private const string CategoriesKey = "ColoredHeader_Categories";

        public static bool AutoStatic
        {
            get
            {
#if UNITY_EDITOR
                return EditorPrefs.GetBool(AutoStaticKey, true);
#else
                return false;
#endif
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool(AutoStaticKey, value);
#endif
            }
        }

        public static List<CategoryInfo> Categories
        {
            get
            {
#if UNITY_EDITOR
                var json = EditorPrefs.GetString(CategoriesKey, "");
                if (string.IsNullOrEmpty(json))
                {
                    return GetDefaultCategories();
                }
                return JsonUtility.FromJson<CategoryList>(json).Categories;
#else
                return GetDefaultCategories();
#endif
            }
            set
            {
#if UNITY_EDITOR
                var categoryList = new CategoryList { Categories = value };
                var json = JsonUtility.ToJson(categoryList);
                EditorPrefs.SetString(CategoriesKey, json);
#endif
            }
        }

        [Serializable]
        private class CategoryList
        {
            public List<CategoryInfo> Categories;
        }

        private static List<CategoryInfo> GetDefaultCategories()
        {
            return new List<CategoryInfo>
            {
                new CategoryInfo { Name = "UI", Color = new Color(0f, 0.47f, 0.83f, 1f) },
                new CategoryInfo { Name = "System", Color = new Color(0.29f, 0.29f, 0.29f, 1f) },
                new CategoryInfo { Name = "Lighting", Color = new Color(1f, 0.84f, 0f, 1f) },
                new CategoryInfo { Name = "Audio", Color = new Color(0.06f, 0.49f, 0.06f, 1f) },
                new CategoryInfo { Name = "Timeline", Color = new Color(0.36f, 0.18f, 0.57f, 1f) }
            };
        }
    }
}
