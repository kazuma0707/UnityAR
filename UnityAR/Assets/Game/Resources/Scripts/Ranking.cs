using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Ranking : MonoBehaviour
{
    //=============  定数　===============//
    private string RANKING_PREF_KEY = "ranking";
    private int RANKING_NUM = 5;
    private float[] ranking;
    Array _array;

    [SerializeField, Range(0, 1000)]
    int size;
    public GUIStyleState stylestate;
    private GUIStyle style;
    public Color color;
    [SerializeField]
    Vector2 label_ranking;
    [SerializeField]
    Vector2 label_score;

    [SerializeField]
    GameObject rankImage;
    //  テキスト
    [SerializeField]
    Text rankText;
    [SerializeField]
    Text scoreText;

    bool rankInFlag = false;
    public GameObject fireworksParticle;
    GameObject obj;
    public bool modeFlag = false;

    //  シーンのロードが複数行われるのの防止
    bool isLoad = false;

    //  FadeObject
    GameObject fadeObj;

    //  ボタン
    [SerializeField]
    Button returnTitleButton;

    //  パスワード以外のUI
    [SerializeField]
    GameObject[] uiObject;
    //  パスワード入力オブジェクト
    [SerializeField]
    GameObject[] inputField;
    ////  モード変更ボタン
    //[SerializeField]
    //GameObject ModeBotton;
    [SerializeField]
    GameObject missText;
    [SerializeField]
    GameObject resetButton;


    //  シーン開始時に行う処理
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    // Use this for initialization
    void Start()
    {
        style = new GUIStyle();
        ranking = new float[RANKING_NUM];

        getRanking();
        //saveRanking(10);
        //saveRanking(100);
        //saveRanking(122);
        saveRanking(GameManager.gameScore);

        stylestate.textColor = color;
        style.normal = stylestate;

        displayScore();

        //GUI.Label(rect_ranking, ranking_string, style);

        //GUI.Label(rect_score, score_string, style);

        fadeObj = GameObject.FindGameObjectWithTag("FadeObj");
    }

    // Update is called once per frame
    void Update()
    {
        style.fontSize = size;

        Render();

        if (rankInFlag && !obj)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(1, 4), 0.0f);
            obj = Instantiate(fireworksParticle, pos, Quaternion.identity);
            if (!obj.GetComponent<ParticleSystem>().isPlaying)
            {
                obj = null;
            }
        }

        //  fade中はボタンを非アクティブ化する
        ButtonActive(); 
    }

    /****************************************************************
    *|　機能　ボタンのアクティブ状態を変える
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    private void ButtonActive()
    {
        if (fadeObj.GetComponent<FadeManager>().GetFadeFlag() == true)
        {
            returnTitleButton.interactable = false;
        }
        else
        {
            returnTitleButton.interactable = true;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | ランキングの取得
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void getRanking()
    {
        var _ranking = PlayerPrefs.GetString(RANKING_PREF_KEY);
        if (_ranking.Length > 0)
        {
            var _score = _ranking.Split(","[0]);
            ranking = new float[RANKING_NUM];
            for (var i = 0; i < _score.Length && i < RANKING_NUM; i++)
            {
                ranking[i] = float.Parse(_score[i]);
            }
        }

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | ランキングの記録
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void saveRanking(float new_score)
    {
        string rankingString = "";
        if (ranking.Length != 0)
        {
            float _tmp = 0.0f;
            for (int i = 0; i < ranking.Length; i++)
            {
                if (ranking[i] < new_score)
                {
                    _tmp = ranking[i];
                    ranking[i] = new_score;
                    new_score = _tmp;
                    rankInFlag = true;
                }
            }
        }
        else
        {
            ranking[0] = new_score;
        }
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingString += ranking[i].ToString();
            if (i + 1 < ranking.Length)
            {
                rankingString += ",";
            }
        }
        PlayerPrefs.SetString(RANKING_PREF_KEY, rankingString);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | ランキングの削除
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void deleteRanking()
    {
        PlayerPrefs.DeleteKey(RANKING_PREF_KEY);
        for (int i = 0; i < ranking.Length; i++)
        {
            ranking[i] = 0;
        }
        displayScore();

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | タイトルへ戻る
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void ReturnTitle()
    {
        if (!isLoad)
        {
            //SceneManager.LoadScene("Title");
            FadeManager.Instance.LoadScene("Title", 2.0f);
        }
        isLoad = true;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | スコアの表示
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void displayScore()
    {
        //Rect rect_ranking = new Rect(new Vector2(Screen.width, Screen.height), label_ranking);
        //Rect rect_score = new Rect(new Vector2(label_score.x, label_score.y), label_score);
        string score_string = "";
        //string ranking_string = "";
        for (int i = 0; i < ranking.Length; i++)
        {
            //ranking_string = ranking_string + (i + 1) + "位" + "  " + ranking[i] + "秒\n";
            //ranking_string = ranking_string + (i + 1) + "位\n";
            score_string = score_string + ranking[i] + "\n";
        }
        //ranking_string = ranking_string + "1st\n";
        //ranking_string = ranking_string + "2nd\n";
        //ranking_string = ranking_string + "3rd\n";
        //ranking_string = ranking_string + "4th\n";
        //ranking_string = ranking_string + "5th\n";

        //  順位の表示オブジェクト
        rankImage.SetActive(true);
        style.alignment = TextAnchor.UpperRight;
        //rankText.text = ranking_string.ToString();
        scoreText.text = score_string.ToString();
    }

    private void Render()
    {

    }
    private void OnGUI()
    {
        //Vector2 label_ranking = new Vector2(Screen.width, Screen.height);


    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | パスワード画面の表示
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void displayPassword()
    {
        if (modeFlag == false)
        {
            for (int i = 0; i < inputField.Length; i++)
            {
                inputField[i].SetActive(true);
            }
            for (int i = 0; i < uiObject.Length; i++)
            {
                uiObject[i].SetActive(false);
            }

            modeFlag = true;
        }
        else if (modeFlag == true)
        {
            for (int i = 0; i < inputField.Length; i++)
            {
                inputField[i].SetActive(false);
            }
            for (int i = 0; i < uiObject.Length; i++)
            {
                uiObject[i].SetActive(true);
            }
            modeFlag = false;
            inputField[0].GetComponent<InputManager>().MissTextReset();
            missText.SetActive(false);
            resetButton.SetActive(false);
        }
    }
}
