using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialManager : MonoBehaviour
{
    // 定数宣言 //////////////////////////////////////////////////////////////////////////////////   
    private const string HEAD_OBJ = "face";                // HeadObjのタグ
    private const int BLINK_NUM = 4;                          // BlendShape(memoto.toziru)の番号

    //////////////////////////////////////////////////////////////////////////////////////////////


    [SerializeField]
    private GameObject faceObj;                 // 顔オブジェクト
    [SerializeField]
    private bool blink = false;                 // 瞬きするかどうか
    [SerializeField]
    private float speed = 1.0f;                 // 瞬きの速さ

    private SkinnedMeshRenderer smr;
    private Mesh mesh;
    private float eyeCloseValue = 0.0f;


    // Use this for initialization
    void Start ()
    {
        // HeadObjのタグが付いたオブジェクトを取得
        FindHeadObjTag();

        // HeadObjのタグが付いたオブジェクトからSkinnedMeshRendererを取得
        smr = faceObj.GetComponent<SkinnedMeshRenderer>();
        // SkinnedMeshRendererからメッシュを取得
        mesh = smr.sharedMesh;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        // HeadObjのタグが付いたオブジェクトを取得
        FindHeadObjTag();

        // 瞬きアニメーション
        BlinkAnimation();

        smr.SetBlendShapeWeight(BLINK_NUM, eyeCloseValue);
    }


    // HeadObjのタグが付いたオブジェクトを取得
    private void FindHeadObjTag()
    {
        if (!faceObj)
        {
            faceObj = GameObject.Find(HEAD_OBJ);

            // HeadObjのタグが付いたオブジェクトからSkinnedMeshRendererを取得
            smr = faceObj.GetComponent<SkinnedMeshRenderer>();
            // SkinnedMeshRendererからメッシュを取得
            mesh = smr.sharedMesh;
        }
    }

    // 瞬きアニメーション
    private void BlinkAnimation()
    {
        eyeCloseValue = Mathf.Lerp(0, 100, 1.0f);
    }
}
