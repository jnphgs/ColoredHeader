using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColoredHeader.Runtime
{
    public class ColoredHeaderSettings : ScriptableObject
    {
        [Serializable]
        public struct CategoryInfo
        {
            public string Name;
            public Color Color;
        }

        public bool AutoStatic = true;

        public List<CategoryInfo> Categories = new List<CategoryInfo>
        {
            new CategoryInfo { Name = "UI", Color = new Color(0f, 0.47f, 0.83f, 1f) },
            new CategoryInfo { Name = "System", Color = new Color(0.29f, 0.29f, 0.29f, 1f) },
            new CategoryInfo { Name = "Lighting", Color = new Color(1f, 0.84f, 0f, 1f) },
            new CategoryInfo { Name = "Audio", Color = new Color(0.06f, 0.49f, 0.06f, 1f) },
            new CategoryInfo { Name = "Timeline", Color = new Color(0.36f, 0.18f, 0.57f, 1f) }
        };

        private static ColoredHeaderSettings _instance;
        public static ColoredHeaderSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ColoredHeaderSettings>("ColoredHeaderSettings");
#if UNITY_EDITOR
                    if (_instance == null)
                    {
                        var assets = UnityEditor.AssetDatabase.FindAssets("t:ColoredHeaderSettings");
                        if (assets.Length > 0)
                        {
                            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(assets[0]);
                            _instance = UnityEditor.AssetDatabase.LoadAssetAtPath<ColoredHeaderSettings>(path);
                        }
                    }
#endif
                }
                return _instance;
            }
        }
    }
}
