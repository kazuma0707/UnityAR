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
            startText.GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f, flash);
        }

        // 左クリックされた瞬間
        if (Input.GetMouseButtonDown(0))
        {
            startText.SetActive(false);
            scrollView.SetActive(true);
        }
    }

    public void startGame()
    {
        if (!isLoad)
        {
            //  シーン切り替え
            //SceneManager.LoadScene("Play");
            FadeManager.Instance.LoadScene("Play", 2.0f);
        }
        isLoad = true;
    }

    //  キャラクリシーンに遷移するための関数
    public void CharCreate()
    {
        if (!isLoad)
        {
            //  シーン切り替え
            SceneManager.LoadScene("CharCreate");
            //FadeManager.Instance.LoadScene("CharCreate", 2.0f);
        }
        isLoad = true;
    }

    //  学校紹介に遷移するための関数
    public void SchoolIntroduction()
    {
        if (!isLoad)
        {
            //  シーン切り替え
            //SceneManager.LoadScene("");
            //FadeManager.Instance.LoadScene("CharCreate", 2.0f);
        }
        isLoad = true;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | ランキングの削除
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    //  
    public void Appreciation()
    {
        if (!isLoad)
        {
            //  シーン切り替え
            SceneManager.LoadScene("Appreciation");
            //FadeManager.Instance.LoadScene("Appreciation", 2.0f);
        }
        isLoad = true;
    }
}
