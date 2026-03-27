using System.Linq;
using UnityEngine;

namespace ColoredHeader.Runtime
{
    [DisallowMultipleComponent]
    public class ColorInHierarchy : MonoBehaviour
    {
        public string CategoryName;
        public Color Color = Color.white;


        private void OnValidate()
        {
#if UNITY_EDITOR
            if (ColoredHeaderSettings.AutoStatic)
            {
                gameObject.isStatic = true;
            }

            if (string.IsNullOrEmpty(CategoryName)) return;

            var category = ColoredHeaderSettings.Categories.FirstOrDefault(c => c.Name == CategoryName);
            if (!string.IsNullOrEmpty(category.Name))
            {
                Color = category.Color;
            }
#endif
        }
    }
}
