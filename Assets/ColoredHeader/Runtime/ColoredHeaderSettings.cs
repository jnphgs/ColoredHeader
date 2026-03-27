using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace ColoredHeader.Runtime
{
    [Serializable]
    public class ColoredHeaderSettings
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
                if (_instance != null) return _instance;
                _instance = Load();
                return _instance;
            }
        }

        private const string SettingsPath = "ProjectSettings/ColoredHeaderSettings.json";

        public static ColoredHeaderSettings Load()
        {
#if UNITY_EDITOR
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                return JsonUtility.FromJson<ColoredHeaderSettings>(json);
            }
#endif
            return new ColoredHeaderSettings();
        }

        public void Save()
        {
#if UNITY_EDITOR
            var json = JsonUtility.ToJson(this, true);
            File.WriteAllText(SettingsPath, json);
#endif
        }
    }
}
