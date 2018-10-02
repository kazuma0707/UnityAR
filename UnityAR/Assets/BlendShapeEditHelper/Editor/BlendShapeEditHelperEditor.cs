using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Mebiustos.BlendShapeEditHelper {
    [CustomEditor(typeof(BlendShapeEditHelper))]
    public class BlendShapeEditHelperEditor : Editor {
        const string MenuItemName = "CONTEXT/BlendShapeEditHelper/DispStyleChange - TAB,LIST";
        //const string HideButtonLabel = "Hide SomeGizmo & Wireframe";
        const string HideButtonLabel = "Hide Wireframe";

        [MenuItem(MenuItemName)]
        private static void DispStyleTab(MenuCommand command) {
            //if (!IsTabStyle())
            //    return;

            var helper = (BlendShapeEditHelper)command.context;
            if (helper.guiStyle == BlendShapeEditHelper.GUIStyle.Tab)
                helper.guiStyle = BlendShapeEditHelper.GUIStyle.List;
            else
                helper.guiStyle = BlendShapeEditHelper.GUIStyle.Tab;
        }

        Color defaultColor;
        Color defaultBackgroundColor;
        Color defaultContentColor;

        void OnEnable() {
            this.defaultColor = GUI.color;
            this.defaultBackgroundColor = GUI.backgroundColor;
            this.defaultContentColor = GUI.contentColor;

            //helper = (BlendShapeEditHelper)target;
            this.HideAllWireframe(BlendShapeEditHelper.hideSomeGizmoAndWireframe);
            this.HideCollider(BlendShapeEditHelper.hideSomeGizmoAndWireframe);
        }

        public override void OnInspectorGUI() {
            var helper = (BlendShapeEditHelper)target;

            helper.maxWeight = EditorGUILayout.FloatField("Max Weight", helper.maxWeight);
            if (helper.maxWeight <= 0)
                helper.maxWeight = 0.01f;
            helper.fullName = EditorGUILayout.Toggle("Disp Full Name", helper.fullName);

            EditorGUILayout.Separator();
            this.DrawButtons();
            EditorGUILayout.Separator();

            if (GUI.changed) {
                this.HideAllWireframe(BlendShapeEditHelper.hideSomeGizmoAndWireframe);
                this.HideCollider(BlendShapeEditHelper.hideSomeGizmoAndWireframe);
            }

            var smeshs = GetSkinnedMeshRenderers();
            if (smeshs.Length > 0) {
                switch (helper.guiStyle) {
                    case BlendShapeEditHelper.GUIStyle.Tab:
                        this.DrawTabStyle(smeshs);
                        break;
                    case BlendShapeEditHelper.GUIStyle.List:
                        this.DrawListStyle(smeshs);
                        break;
                }
            }

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
            }
        }

        void CreateAnimationClip(SkinnedMeshRenderer component) {
            var helper = (BlendShapeEditHelper)target;

            var animclip = new AnimationClip();

            var smeshs = this.GetSkinnedMeshRenderers();
            foreach (var mesh in smeshs) {
                var pathsb = new StringBuilder().Append(mesh.transform.name);
                var trans = mesh.transform;
                while (trans.parent != null && trans.parent != helper.transform) {
                    trans = trans.parent;
                    pathsb.Insert(0, "/").Insert(0, trans.name);
                }
                var path = pathsb.ToString();

                for (int i = 0; i < mesh.sharedMesh.blendShapeCount; i++) {
                    EditorCurveBinding curveBinding = new EditorCurveBinding();
                    curveBinding.type = typeof(SkinnedMeshRenderer);
                    curveBinding.path = path;
                    curveBinding.propertyName = "blendShape." + mesh.sharedMesh.GetBlendShapeName(i);

                    AnimationCurve curve = new AnimationCurve();
                    curve.AddKey(0f, mesh.GetBlendShapeWeight(i));
                    //Debug.Log("path: " + curveBinding.path + "\r\nname: " + curveBinding.propertyName);
                    AnimationUtility.SetEditorCurve(animclip, curveBinding, curve);
                }
            }

            AssetDatabase.CreateAsset(animclip, AssetDatabase.GenerateUniqueAssetPath("Assets/" + helper.gameObject.name + " Clip.anim"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void DrawButtons() {
            var helper = (BlendShapeEditHelper)target;

            GUILayout.BeginHorizontal();
            GUI.color = Color.green;
            if (GUILayout.Button("Create Clip", GUILayout.ExpandWidth(false))) {
                CreateAnimationClip(helper.GetComponentInChildren<SkinnedMeshRenderer>());
            }
            GUI.color = this.defaultColor;

            GUILayout.FlexibleSpace();
            string hideButtonLabel;
            //Color hideButtonColor;
            Color hideButtonBgColor;
            Color hideButtonCtColor;

            // collider gizmo
            if (BlendShapeEditHelper.hideSomeGizmoAndWireframe) {
                //hideButtonColor = Color.yellow;
                hideButtonBgColor = Color.gray;
                hideButtonCtColor = Color.yellow;
                hideButtonLabel = BlendShapeEditHelperEditor.HideButtonLabel;
            } else {
                //hideButtonColor = this.defaultColor;
                hideButtonBgColor = this.defaultBackgroundColor;
                hideButtonCtColor = this.defaultContentColor;
                hideButtonLabel = BlendShapeEditHelperEditor.HideButtonLabel;
            }
            //GUI.color = hideButtonColor;
            GUI.backgroundColor = hideButtonBgColor;
            GUI.contentColor = hideButtonCtColor;
            if (GUILayout.Button(hideButtonLabel, GUILayout.ExpandWidth(false)))
                BlendShapeEditHelper.hideSomeGizmoAndWireframe = !BlendShapeEditHelper.hideSomeGizmoAndWireframe;
            //GUI.color = this.defaultColor;
            GUI.backgroundColor = this.defaultBackgroundColor;
            GUI.contentColor = this.defaultContentColor;

            GUILayout.EndHorizontal();
        }

        void HideAllWireframe(bool hidden) {
            var helper = (BlendShapeEditHelper)target;
            if (!helper) return;
            var rends = helper.GetComponentsInChildren<Renderer>();
            for (int i=0; i<rends.Length; i++) {
                EditorUtility.SetSelectedWireframeHidden(rends[i], hidden);
            }
        }

        void HideCollider(bool hidden) {
            ////for (int testloop = 0; testloop < 100; testloop++) {
            //    //Debug.Log("testloop: " + testloop);
                
            //    // Annotation
            //    var annotationType = System.Type.GetType("UnityEditor.Annotation, UnityEditor");
            //    var classIdField = annotationType.GetField("classID");
            //    var scriptClassField = annotationType.GetField("scriptClass");
            //    //var gizmoEnabledField = annotationType.GetField("gizmoEnabled");

            //    // AnnotationUtility
            //    var annotationUtilityType = System.Type.GetType("UnityEditor.AnnotationUtility, UnityEditor");
            //    var getAnnotationsMethod = annotationUtilityType.GetMethod("GetAnnotations", BindingFlags.NonPublic | BindingFlags.Static);
            //    var setGizmoEnabledMethod = annotationUtilityType.GetMethod("SetGizmoEnabled", BindingFlags.NonPublic | BindingFlags.Static);

            //    // BaseObjectTools
            //    var baseObjectToolsType = System.Type.GetType("UnityEditorInternal.BaseObjectTools, UnityEditor");
            //    var ClassIDToStringMethod = baseObjectToolsType.GetMethod("ClassIDToString", BindingFlags.Public | BindingFlags.Static);

            //    //
            //    var parameters = new object[] {
            //    };
            //    var annotations = getAnnotationsMethod.Invoke(null, null) as System.Array;

            //    //Debug.Log("annotations length: " + annotations.Length);

            //    foreach (var n in annotations) {
            //        int classid = (int)classIdField.GetValue(n);

            //        // getClassName
            //        var parameters2 = new object[] {
            //            classid
            //        };
            //        var className = (string)ClassIDToStringMethod.Invoke(null, parameters2);

            //        //Debug.LogFormat("className: {0} / {1}", className, (string)scriptClassField.GetValue(n));

            //        if (className.EndsWith("Collider") || className.EndsWith("Joint")) {
            //            // getGizmoEnable
            //            //var gizmoEnabled = (int)gizmoEnabledField.GetValue(n);

            //            // toggle
            //            var parameters3 = new object[] {
            //                classid,
            //                (string)scriptClassField.GetValue(n),
            //                //"",
            //                hidden ? 0 : 1
            //                //0
            //            };
            //            setGizmoEnabledMethod.Invoke(null, parameters3);
            //        }
            //    }
            //    SceneView.RepaintAll();
            //    //System.Threading.Thread.Sleep(1);
            ////}
        }

        SkinnedMeshRenderer[] GetSkinnedMeshRenderers() {
            var helper = (BlendShapeEditHelper)target;
            var renderers = helper.GetComponentsInChildren<SkinnedMeshRenderer>();
            List<SkinnedMeshRenderer> smeshList = new List<SkinnedMeshRenderer>();
            for (int i = 0; i < renderers.Length; i++) {
                var rend = renderers[i];
                var cnt = rend.sharedMesh.blendShapeCount;
                if (cnt > 0) {
                    smeshList.Add(rend);
                }
            }
            return smeshList.ToArray();
        }

        void DrawTabStyle(SkinnedMeshRenderer[] smeshs) {
            var helper = (BlendShapeEditHelper)target;

            string[] tabItems = new string[smeshs.Length];
            for (int i = 0; i < smeshs.Length; i++) {
                tabItems[i] = smeshs[i].gameObject.name;
            }

            helper.tabIndex = GUILayout.Toolbar(helper.tabIndex, tabItems);
            if (!(helper.tabIndex < smeshs.Length))
                helper.tabIndex = 0;

            EditorGUILayout.HelpBox(GetGameObjectPath(helper.transform, smeshs[helper.tabIndex].transform), MessageType.None);

            var targetRenderer = smeshs[helper.tabIndex];
            var sharedMesh = targetRenderer.sharedMesh;
            for (int i = 0; i < targetRenderer.sharedMesh.blendShapeCount; i++) {
                var label = sharedMesh.GetBlendShapeName(i);
                if (!helper.fullName) {
                    var lidx = label.IndexOf('.');
                    if (lidx > -1 && lidx + 1 < label.Length) {
                        label = label.Substring(label.IndexOf('.') + 1);
                    }
                }
                //targetRenderer.SetBlendShapeWeight(i, EditorGUILayout.Slider(label, targetRenderer.GetBlendShapeWeight(i) / helper.maxWeight, 0f, 1f) * helper.maxWeight);
                targetRenderer.SetBlendShapeWeight(i, EditorGUILayout.Slider(label, targetRenderer.GetBlendShapeWeight(i), 0f, helper.maxWeight));
            }
        }

        void DrawListStyle(SkinnedMeshRenderer[] smeshs) {
            var helper = (BlendShapeEditHelper)target;

            for (int i=0; i<smeshs.Length; i++) {
                if (i>0) EditorGUILayout.Separator();
                var targetRenderer = smeshs[i];
                GUI.color = Color.cyan;
                GUILayout.Label(targetRenderer.gameObject.name, EditorStyles.boldLabel);
                GUI.color = this.defaultColor;
                EditorGUILayout.HelpBox(GetGameObjectPath(helper.transform, targetRenderer.transform), MessageType.None);

                var sharedMesh = targetRenderer.sharedMesh;
                for (int j = 0; j < targetRenderer.sharedMesh.blendShapeCount; j++) {
                    var label = sharedMesh.GetBlendShapeName(j);
                    if (!helper.fullName) {
                        var lidx = label.IndexOf('.');
                        if (lidx > -1 && lidx + 1 < label.Length) {
                            label = label.Substring(label.IndexOf('.') + 1);
                        }
                    }
                    //targetRenderer.SetBlendShapeWeight(j, EditorGUILayout.Slider(label, targetRenderer.GetBlendShapeWeight(j) / helper.maxWeight, 0f, 1f) * helper.maxWeight);
                    targetRenderer.SetBlendShapeWeight(j, EditorGUILayout.Slider(label, targetRenderer.GetBlendShapeWeight(j), 0f, helper.maxWeight));
                }
            }
        }

        static string GetGameObjectPath(Transform root, Transform transform) {
            StringBuilder path = new StringBuilder().Append(transform.name);
            while (transform.parent != null && transform.parent != root) {
                transform = transform.parent;
                path.Insert(0, "/").Insert(0,transform.name);
            }
            path.Insert(0, "/").Insert(0, "[this]");
            return path.ToString();
        }
    }
}
