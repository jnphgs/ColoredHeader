using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace ColoredHeader.Runtime
{
    [DisallowMultipleComponent]
    public class ColorInHierarchy : MonoBehaviour
    {
        public string categoryName;
        public Color color = Color.white;


        private void OnValidate()
        {
#if UNITY_EDITOR
            var settings = ColoredHeaderSettings.Instance;
            if (settings == null) return;

            if (settings.autoStatic)
            {
                gameObject.isStatic = true;
            }

            if (string.IsNullOrEmpty(categoryName)) return;

            var category = settings.categories.FirstOrDefault(c => c.name == categoryName);
            if (category != null && !string.IsNullOrEmpty(category.name))
            {
                color = category.color;
            }
#endif
        }
    }
}
