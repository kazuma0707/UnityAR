//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   TitleManager.cs
//!
//! @brief  タイトルで行う処理
//!
//! @date   2018/11/14
//!
//! @author 加藤　竜哉
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager: MonoBehaviour {

    //  警告表示の点滅時間(数字　大→点滅 早　小→点滅 遅)
    private const int FLASH_TIME = 2;

    // 各シーンに飛ぶボタンの登録番号
    private const int START_BUTTON = 0;
    private const int APLLECIATION_BUTTON = 1;
    private const int SCHOOL_INTRO_BUTTON = 2;
    private const int CHAR_CRE_BUTTON = 3;

    // 各シーンに飛ぶボタンの規定の大きさ
    private const float SCENE_BUTTON_SCALE = 0.9f;

    //  シーンのロードが複数行われるのの防止
    bool isLoad = false;
    //  FadeObject
    GameObject fadeObj;

    //  ボタン
    [SerializeField]
    Button startButton;

    //  テキスト
    [SerializeField]
    GameObject startText;

    //  メニューオブジェクト
    [SerializeField]
    GameObject scrollView;
    [SerializeField]
    GameObject[] items;
    [SerializeField]
    GameObject menuImage;

    //  チュートリアルを表示するかどうかのフラグ
    static public bool tutorialSkipFlag = false;

    [SerializeField]
    GameObject modeChangeBotton;
    bool modeFlag = false;          //  モード切替用フラグ(初期は通常モード)   

    //  モードボタンのimage切り替え
    [SerializeField]
    Sprite ARon;
    [SerializeField]
    Sprite ARoff;

    private void Start()
    {
        fadeObj = GameObject.FindGameObjectWithTag("FadeObj");
    }

    private void Update()
    {
        //  テキストのアクティブがtrueなら
        if (startText.activeSelf)
        {
            float flash = Mathf.Abs(Mathf.Sin(Time.time * FLASH_TIME));
            //  点滅を行う
            startText.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, flash);
        }

        // 左クリックされた瞬間
        if (Input.GetMouseButtonDown(0))
        {
            startText.SetActive(false);
            scrollView.SetActive(true);
            menuImage.SetActive(true);
            modeChangeBotton.SetActive(true);
        }
    }

    public void startGame()
    {
        // 真ん中にあるボタンを押していなければ何もしない
        if (!CheckHit()) return;

        if (!isLoad)
        {
            isLoad = true;

            //  シーン切り替え
            //SceneManager.LoadScene("Play");
            if (modeFlag == true)
            {
                FadeManager.Instance.LoadScene("PlayAR", 2.0f);
            }
            else
            {
                FadeManager.Instance.LoadScene("Play", 2.0f);
            }
        }

    }

    //  キャラクリシーンに遷移するための関数
    public void CharCreate()
    {
        // 真ん中にあるボタンを押していなければ何もしない
        if (!CheckHit()) return;


        if (!isLoad)
        {
            isLoad = true;

            //  シーン切り替え
            SceneManager.LoadScene("CharCreate");
            //FadeManager.Instance.LoadScene("CharCreate", 2.0f);
        }

    }

    //  学校紹介に遷移するための関数
    public void SchoolIntroduction()
    {
        // 真ん中にあるボタンを押していなければ何もしない
        if (!CheckHit()) return;

        if (!isLoad)
        {
            isLoad = true;

            //  シーン切り替え
            //SceneManager.LoadScene("");
            FadeManager.Instance.LoadScene("ARScene", 2.0f);
        }

    }

    public void Appreciation()
    {
        // 真ん中にあるボタンを押していなければ何もしない
        if (!CheckHit()) return;


        if (!isLoad)
        {
            isLoad = true;

            //  シーン切り替え
            SceneManager.LoadScene("Appreciation");
            //FadeManager.Instance.LoadScene("Appreciation", 2.0f);
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | ミニゲームのモード切替
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    //  
    public void OnClickMode()
    {


       if(modeFlag == true)
       {
           modeFlag = false;
           modeChangeBotton.GetComponent<Image>().sprite = ARoff;
       }
       else
       {
            modeFlag = true;
            modeChangeBotton.GetComponent<Image>().sprite = ARon;
       }

    }


    private bool CheckHit()
    {
        bool check = false;

        // マウスクリック座標を取得
        if (!Input.GetMouseButtonUp(0)) return check;

        Vector3 touchPos = Input.mousePosition;  // スクリーン座標
        // スマホではこっちを使用
        //Vector3 touchPos = Input.GetTouch( 0 ).position;

        // カメラを原点としたスクリーン座標へのレイ
        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        //レイヤーマスク作成
        int layerMask = LayerMask.GetMask(new string[] { "UI" });

        // レイとのコライダーの当たり判定
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(touchPos.x, touchPos.y), (Vector2)ray.direction, 1000.0f, layerMask);
               

        // Rayと当たったオブジェクトを調べる
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].transform.name);
            // MenuImageと当たっているか
            if (hits[i].transform.name == "MenuImage")
            {
                check = true;
                break;
            }
        }
        

        return check;
    }
}
