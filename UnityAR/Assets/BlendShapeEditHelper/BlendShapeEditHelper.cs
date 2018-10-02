using UnityEngine;
using System.Collections;

namespace Mebiustos.BlendShapeEditHelper {
    public class BlendShapeEditHelper : MonoBehaviour {
#if UNITY_EDITOR
        public float maxWeight = 100f;
        public bool fullName = false;
        public static bool hideSomeGizmoAndWireframe = false;
        public enum GUIStyle {
            Tab,
            List
        }
        public GUIStyle guiStyle = GUIStyle.List;
        [HideInInspector]
        public int tabIndex;
#endif
    }
}
