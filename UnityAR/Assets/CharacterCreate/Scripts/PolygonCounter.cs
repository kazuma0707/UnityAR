using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PolygonCounter : MonoBehaviour {

	int vertices;
    int polygons;

    [SerializeField]
    int minFps = 60;

    int frameCount = 0;
    float nextTime = 0.0f;

    void Start () {
        nextTime = Time.time + 1;
    }

    void Update () {

        frameCount++;

        // 1秒ごとにFPS検証
        if (Time.time >= nextTime) {
            // Debug.LogFormat ("{0}fps", frameCount);
            if (frameCount < minFps) PolygonCount (frameCount);

            frameCount = 0;
            nextTime += 1f;
        }

    }

    [ContextMenu("CountStart")]
    void PolygonCount(int fps = -1){

        vertices = 0;
        polygons = 0;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject))) {

            if (obj.activeInHierarchy) {

                SkinnedMeshRenderer skin = obj.GetComponent<SkinnedMeshRenderer> ();

                if (skin != null) {
                    int vert = skin.sharedMesh.vertices.Length;
                    vertices += vert;

                    int polygon = skin.sharedMesh.triangles.Length / 3;
                    polygons += polygon;
                }

                MeshFilter mesh = obj.GetComponent<MeshFilter> ();

                if (mesh != null) {
                    int vert = mesh.sharedMesh.vertices.Length;
                    vertices += vert;

                    int polygon = mesh.sharedMesh.triangles.Length / 3;
                    polygons += polygon;
                }

            }
        }
        Debug.LogFormat ("Vertices(verts) : {0} , Polygons(Tris) : {1} , FPS : {2} " ,
            vertices , polygons, fps);  

    }

#if UNITY_EDITOR

    [CustomEditor(typeof(PolygonCounter))]
    public class CountStartEditor : Editor {

        PolygonCounter polygonCounter;

        void OnEnable(){
            polygonCounter = target as PolygonCounter;
        }

        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();

            if (GUILayout.Button("カウント開始")){
                polygonCounter.PolygonCount ();
            }
        }

    }

#endif

}
