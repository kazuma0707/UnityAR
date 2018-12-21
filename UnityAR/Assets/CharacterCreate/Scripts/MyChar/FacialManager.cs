using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialManager : MonoBehaviour
{
    // 定数宣言 //////////////////////////////////////////////////////////////////////////////////   
    private const string HEAD_OBJ = "face";                               // HeadObjのタグ
    private const string NORMALFACE_EYE_CLOSE = "NormalFace_EyeClose";    // 瞬きアニメーションの名前
    private const string NORMALFACE_DEFAULT = "Face_default";             // デフォルトアニメーションの名前
    private const string BLINK_FRAG = "blink";                            // Animatorのblinkフラグ
    private const int BLINK_NUM = 4;                                      // BlendShape(memoto.toziru)の番号
    private const int RANDOM_MIN = 0;                                     // ランダムの最小値
    private const int RANDOM_MAX = 100;                                   // ランダムの最大値
    private const int BLINK_FRAG_UP = 30;                                 // blinkフラグを上げる確率(30%)
    private const bool BLINK_ON = true;                                   // 瞬きをする
    private const bool BLINK_OFF = false;                                 // 瞬きをしない
    
    //////////////////////////////////////////////////////////////////////////////////////////////


    [SerializeField]
    private GameObject faceObj;                 // 顔オブジェクト
    [SerializeField]
    private bool blink;                         // 瞬きするかどうか
    [SerializeField]
    private float speed = 1.0f;                 // 瞬きの速さ

    private Animator animator;                  // 顔オブジェクトのAnimator
    private HeadLookController headLC;          // HeadLookControllerコンポーネント


    // Use this for initialization
    void Start ()
    {
        // HeadObjのタグが付いたオブジェクトを取得
        FindHeadObjTag();

        blink = BLINK_OFF;

        // HeadLookControllerコンポーネントを取得し非アクティブにする
        headLC = this.GetComponent<HeadLookController>();
        headLC.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // HeadObjのタグが付いたオブジェクトを取得
        FindHeadObjTag();

        // 瞬きアニメーション
        BlinkAnimation();

        // HeadLookControllerコンポーネントの設定
        OnHeadLookController();
    }


    // HeadObjのタグが付いたオブジェクトを取得
    private void FindHeadObjTag()
    {
        if (!faceObj)
        {
            faceObj = GameObject.Find(HEAD_OBJ);

            // HeadObjのタグが付いたオブジェクトからAnimatorを取得
            animator = faceObj.GetComponent<Animator>();
        }
    }

    // 瞬きアニメーション
    private void BlinkAnimation()
    {
        // フラグが立っていたら瞬きをする
        if (blink)
        {            
            animator.SetBool(BLINK_FRAG, BLINK_ON);
            blink = BLINK_OFF;
        }
        // フラグが下がり且つデフォルトアニメーションの時、抽選に入る
        else if (!blink && animator.GetCurrentAnimatorStateInfo(0).IsName(NORMALFACE_DEFAULT))
        {
            animator.SetBool(BLINK_FRAG, BLINK_OFF);
            // 抽選
            int value = Random.Range(RANDOM_MIN, RANDOM_MAX);
            // 抽選に当たれば瞬きをする
            blink = (value > BLINK_FRAG_UP) ? BLINK_ON : BLINK_OFF;            
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    }

    // HeadLookControllerコンポーネントの設定
    private void OnHeadLookController()
    {
        // フェイズがキャラクリであれば
        if (MyCharDataManager.Instance.phase == Phase.CHARA_CREATE)
        {
            // アクティブにする
            headLC.enabled = true;
        }
        else
        {
            // 非アクティブにする
            headLC.enabled = false;
        }
    }
}