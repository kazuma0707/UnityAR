using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialManager : MonoBehaviour
{
    // 定数宣言 //////////////////////////////////////////////////////////////////////////////////   
    private const string HEAD_OBJ = "face";                               // HeadObjのタグ
    private const string NORMALFACE_EYE_CLOSE = "NormalFace_EyeClose";    // 瞬きアニメーションの名前
    private const string NORMALFACE_DEFAULT = "Face_default";             // デフォルトアニメーションの名前
    private const string NORMALFACE_SMILE = "NormalFace_Smile";           // 笑顔アニメーションの名前
    private const string BLINK_FRAG = "blink";                            // Animatorのblinkフラグ
    private const string SMILE_FRAG = "smile";                            // Animatorのsmileフラグ
    private const int RANDOM_MIN = 0;                                     // ランダムの最小値
    private const int RANDOM_MAX = 100;                                   // ランダムの最大値
    private const int BLINK_FRAG_UP = 1;                                 // blinkフラグを上げる確率(30%)
    private const bool BLINK_ON = true;                                   // 瞬きをする
    private const bool BLINK_OFF = false;                                 // 瞬きをしない
    private const bool SMILE_ON = true;                                   // 笑顔になる
    private const bool SMILE_OFF = false;                                 // 笑顔にならない

    //////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField]
    private GameObject faceObj;                 // 顔オブジェクト
    [SerializeField]
    private bool blink;                         // 瞬きするかどうか
   
    [SerializeField]
    private bool smile;                         // 笑顔にするかどうか

    private Animator animator;                  // 顔オブジェクトのAnimator
    private HeadLookController headLC;          // HeadLookControllerコンポーネント


    // Use this for initialization
    void Start ()
    {
        // HeadObjのタグが付いたオブジェクトを取得
        FindHeadObjTag();

        blink = BLINK_OFF;
        smile = SMILE_OFF;

        // HeadLookControllerコンポーネントを取得し非アクティブにする
        headLC = this.GetComponent<HeadLookController>();
        headLC.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // HeadObjのタグが付いたオブジェクトを取得
        if (!FindHeadObjTag()) return;


        // 瞬きアニメーション
        BlinkAnimation();

        // 笑顔アニメーション
        //SmileAnimation();

        // HeadLookControllerコンポーネントの設定
        OnHeadLookController();
    }


    // HeadObjのタグが付いたオブジェクトを取得
    private bool FindHeadObjTag()
    {
        if (!faceObj)
        {
            faceObj = GameObject.Find(HEAD_OBJ);

            // HeadObjのタグが付いたオブジェクトからAnimatorを取得
            animator = faceObj.GetComponent<Animator>();
        }

        if (animator) return true;

        return false;
    }

    // 瞬きアニメーション
    private void BlinkAnimation()
    {
        // 笑顔の状態であれば何もしない
        if (smile) return;

        // フラグが立っていたら瞬きをする
        if (blink)
        {
            //animator.SetBool(BLINK_FRAG, BLINK_ON);
            animator.Play(NORMALFACE_EYE_CLOSE, 0);
            blink = BLINK_OFF;
        }
        // デフォルトアニメーションの時、抽選に入る
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(NORMALFACE_DEFAULT))
        {
            //animator.SetBool(BLINK_FRAG, BLINK_OFF);
            // 抽選
            int value = Random.Range(RANDOM_MIN, RANDOM_MAX);
            // 抽選に当たれば瞬きをする

            if (value < BLINK_FRAG_UP) blink = BLINK_ON;
        }

        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    }

    // 笑顔アニメーション
    public void SmileAnimation()
    {        
        // フラグが上がっていたら笑顔になる
        //if (smile)
        {
            //animator.SetBool(BLINK_FRAG, BLINK_OFF);
            blink = BLINK_OFF;

            animator.Play(NORMALFACE_SMILE, 0);
            //animator.SetBool(SMILE_FRAG, SMILE_ON);
            smile = SMILE_ON;
        }

        Invoke("SettingSmileAnim", 2.0f);

        // デフォルトアニメーションの時、フラグを下げる
        //else if (animator.GetCurrentAnimatorStateInfo(0).IsName(NORMALFACE_DEFAULT))
        //{
        //    animator.SetBool(SMILE_FRAG, SMILE_OFF);
        //}
    }

    private void SettingSmileAnim()
    {
        smile = SMILE_OFF;
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

    // smileフラグのアクセッサ
    public bool Smile
    {
        get { return smile; }
        set { smile = value; }
    }

}