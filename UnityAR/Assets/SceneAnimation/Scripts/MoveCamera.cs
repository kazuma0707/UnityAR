using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using ConstantName;

public class MoveCamera : MonoBehaviour
{
    // メインカメラのチェックポイントの登録番号
    enum CheckPointNum
    {
        CHARACRE,
        SELECT
    }

    // 定数宣言 //////////////////////////////////////////////////////////////////////////////////
    private const double RE_FADE_OUT = 3;            // セレクトからキャラクリにフェードインする時間
    private const double FADE_OUT = 6;               // タイトルからキャラクリにフェードアウトする時間
    private const float CHARA_CRE_POS_X = -0.1f;     // キャラクリする時のポジションX
    private const float CHARA_CRE_POS_Y = 0.2f;      // キャラクリする時のポジションY
    private const float CHARA_CRE_POS_Z = 2.5f;      // キャラクリする時のポジションZ
    private const int TITLE_FADE = 0;                // タイトルからキャラクリに移動する時のフェード
    private const int SELECT_FADE = 1;               // セレクトからキャラクリに移動する時のフェード

    //////////////////////////////////////////////////////////////////////////////////////////////

    // ターゲット
    private GameObject Target;

    // メインカメラのチェックポイント
    //[SerializeField]
    //private GameObject[] cameraCheckPoint = new GameObject[2];

    public ButtonScript _buttonScript;
    // スタートボタン
    [SerializeField]
    private GameObject StartButton;
    // キャラクタークリエイトボタン
    [SerializeField]
    private GameObject CharacterCreateButton;
    // キャラクターの部位を変更するボタン(ResetButtonとTypeViewArea)
    [SerializeField]
    private GameObject[] ChangeButtons;
    // キャラクタークリエイトを完了するボタン
    [SerializeField]
    private GameObject CharacterCreateEndButton;
    // 確認パネル
    [SerializeField]
    private GameObject Panel;
    // はいボタン
    [SerializeField]
    private GameObject YesButton;
    // いいえボタン
    [SerializeField]
    private GameObject NoButton;
    // 学校紹介ボタン
    [SerializeField]
    private GameObject SchoolIntroductionButton;
    // ゲームボタン
    [SerializeField]
    private GameObject GameButton;
    // 鑑賞ボタン
    [SerializeField]
    private GameObject AppreciationButton;

    // キャラクタークリエイトをし直すボタン
    [SerializeField]
    private GameObject ReCharacterCreateButton;
    // タイトル画像
    [SerializeField]
    private Image titleImage;

    /////////////// キャラクタークリエイトし直す ////////////////
    // パネル
    [SerializeField]
    private GameObject RePanel;
    // Yes
    [SerializeField]
    private GameObject ReYesButton;
    // No
    [SerializeField]
    private GameObject ReNoButton;
       
    // キャラクリ用のカメラ
    [SerializeField]
    private GameObject CCCamera;
    private Vector3 CCCdefaultPos;      // キャラクリ用のカメラの初期位置
    private Vector3 CCCdefaultRot;      // キャラクリ用のカメラの初期角度

    // 音関連
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip YesSound;
    [SerializeField]
    private AudioClip NoSound;

    [SerializeField]
    private GameObject MyC;             // マイキャラ

    [SerializeField]
    private GameObject welcomeText;     // Welcomeのテキスト

    public PlayableDirector playableDirector1;
    public PlayableDirector playableDirector2;
    public PlayableDirector playableDirector3;

    [SerializeField]
    private GameObject Camera1;
    [SerializeField]
    private GameObject Camera2;
    [SerializeField]
    private GameObject Camera3;

    [SerializeField]
    private GameObject cmCamera5;
    [SerializeField]
    private GameObject cmCamera6;
    [SerializeField]
    private GameObject cmCamera7;
    [SerializeField]
    private GameObject cmCamera8;
    [SerializeField]
    private GameObject cmCamera9;
    [SerializeField]
    private GameObject cmCamera10;

    [SerializeField]
    private GameObject[] fades;                 // フェード用の画像

    //アニメーター
    private Animator animator;
    // 素体となるモデル
    private GameObject sotai;

    // Use this for initialization
    void Start ()
    {

        // 素体となるモデルを取得
        sotai = MyC.transform.Find("skin").gameObject;

        //アニメーターの取得
        animator = sotai.GetComponent<Animator>();

        CCCdefaultPos = CCCamera.transform.position;
        CCCdefaultRot = CCCamera.transform.localEulerAngles;
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        //UI非表示
       
        
        CharacterCreateButton.SetActive(false);
        CharacterCreateEndButton.SetActive(false);
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
        SchoolIntroductionButton.SetActive(false);
        GameButton.SetActive(false);
        AppreciationButton.SetActive(false);
        ReCharacterCreateButton.SetActive(false);
        RePanel.SetActive(false);
        ReYesButton.SetActive(false);
        ReNoButton.SetActive(false);
        welcomeText.SetActive(false);

        Camera2.GetComponent<Camera>().enabled = false;
        Camera3.GetComponent<Camera>().enabled = false;
        cmCamera5.SetActive(false);
        cmCamera6.SetActive(false);
        cmCamera7.SetActive(false);
        cmCamera8.SetActive(false);
        cmCamera9.SetActive(false);
        cmCamera10.SetActive(false);

        // キャラクリ用のカメラを停止
        CCCamera.SetActive(false);
        // メインカメラを停止
        StopTimeline1();
        StopTimeline2();
        StopTimeline3();

        // タイトルシーンの読み込みが2回目以降であればキャラクリから始める
        if (MyCharDataManager.Instance.SceneLoadOnce) StartCharacterCreateMode();

    }

    // Update is called once per frame
    void Update ()
    {       
        // キャラクリに行くときのフェードの管理
        if (playableDirector1.enabled) FadeInOut(playableDirector1, fades[TITLE_FADE], FADE_OUT);
        if (playableDirector3.enabled) FadeInOut(playableDirector3, fades[SELECT_FADE], RE_FADE_OUT);
    }

    //スタートボタンをクリックしたら
    public void OnStartButton()
    {
        titleImage.enabled = false;
        StartButton.SetActive(false);
        CharacterCreateButton.SetActive(true);

        audioSource.PlayOneShot(YesSound);
    }

    //キャラクタークリエイトボタンを押したら
    public void OnCharacterCreate()
    {     

        CharacterCreateButton.SetActive(false);        

        audioSource.PlayOneShot(YesSound);

        animator.SetBool("Walk", true);

        iTween.RotateTo(MyC, iTween.Hash("y", 150f,"time", 2f));

        iTween.MoveTo(MyC, iTween.Hash("x", CHARA_CRE_POS_X, "y", CHARA_CRE_POS_Y, "z", CHARA_CRE_POS_Z, 
            "speed", 0.5f, 
            "EaseType", iTween.EaseType.linear,
            "oncomplete","WalkStop",
            "oncompletetarget", this.gameObject));

        welcomeText.SetActive(true);
        PlayTimeline1();
    }

    // キャラクリから始めるときの処理
    private void StartCharacterCreateMode()
    {        
        // キャラのデータを適用
        MyCharDataManager.Instance.ReCreate(MyC.transform.Find(MyCharDataManager.SOTAI_MODEL).gameObject);
        // タイトルのUIを非表示にする
        welcomeText.SetActive(false);
        titleImage.enabled = false;
        StartButton.SetActive(false);
        CharacterCreateButton.SetActive(false);
        // キャラの座標を設定
        MyC.transform.localPosition = new Vector3(CHARA_CRE_POS_X, CHARA_CRE_POS_Y, CHARA_CRE_POS_Z);
        // タイトルフェードをフェードインが終わった状態にしてキャラクリ用のUIを表示
        fades[TITLE_FADE].GetComponent<Fade>().IsFade = Fade.FADE_IN;
        MoveToCharaCreEnd(fades[TITLE_FADE]);
    }

    //完了ボタンを押したら
    public void OnCharacterCreateEndButton()
    {
        CharacterCreateEndButton.SetActive(false);
        Panel.SetActive(true);
        YesButton.SetActive(true);
        NoButton.SetActive(true);
        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            if (ChangeButtons[i].GetComponent<ButtonScript>())
                ChangeButtons[i].GetComponent<ButtonScript>().ActiveFalse();
            ChangeButtons[i].transform.localScale = new Vector3(0, 0, 0);
            //ChangeButtons[i].SetActive(false);
        }
        CCCamera.GetComponent<CharaCreCameraCtrl>().MoveFlag = false;
        //_buttonScript.HideAnimation();

    }

    //はいを押したら
    public void OnYesButton()
    {       
        Rotate2();

        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        Camera1.GetComponent<Camera>().enabled = false;
        Camera2.GetComponent<Camera>().enabled = true;
        cmCamera5.SetActive(true);
        cmCamera6.SetActive(true);
        cmCamera7.SetActive(false);

        welcomeText.SetActive(false);

        animator.SetBool("Walk", true);

        iTween.RotateTo(MyC, iTween.Hash("y", 35f));
        iTween.MoveTo(MyC, iTween.Hash("x", -3.3f, "y", 0.2f, "z", -2.2f, "speed",0.75f,
                                       "EaseType", iTween.EaseType.linear, 
                                       "oncomplete", "MoveToSelectEnd",
                                       //"oncomplete", "WalkStop",
                                       "oncompletetarget", this.gameObject
                                       ));

        audioSource.PlayOneShot(YesSound);

        PlayTimeline2();
    }

    //いいえを押したら
    public void OnNoButton()
    {
        CCCamera.GetComponent<CharaCreCameraCtrl>().MoveFlag = true;

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].transform.localScale = new Vector3(1, 1, 1);

        }
        CharacterCreateEndButton.SetActive(true);
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        audioSource.PlayOneShot(NoSound);
    }

    //学校紹介ボタンを押したら
    public void OnSchoolIntroduction()
    {
        //SceneManager.LoadScene(SceneName.ARScene);
        FadeManager.Instance.LoadScene(SceneName.ARScene, 2.0f);
    }

    //ゲームボタンを押したら
    public void OnGame()
    {
        //SceneManager.LoadScene(SceneName.Title);
        FadeManager.Instance.LoadScene(SceneName.Title, 2.0f);
    }

    //鑑賞ボタンを押したら
    public void OnAppreciation()
    {
        //SceneManager.LoadScene(SceneName.Appreciation);
        FadeManager.Instance.LoadScene(SceneName.Appreciation, 2.0f);
    }

    public void OnAppreciationAR()
    {
        FadeManager.Instance.LoadScene(SceneName.AppreciationAR, 2.0f);

    }

    //キャラクタークリエイトをし直す
    public void OnReCharacterCreate()
    {        
        RePanel.SetActive(true);
        ReYesButton.SetActive(true);
        ReNoButton.SetActive(true);

        Panel.SetActive(false);
        SchoolIntroductionButton.SetActive(false);
        GameButton.SetActive(false);
        AppreciationButton.SetActive(false);
        ReCharacterCreateButton.SetActive(false);
    }

    //確認用ボタン
    //はいを押したら
    public void OnReConfirmationYes()
    {
        RePanel.SetActive(false);
        ReYesButton.SetActive(false);
        ReNoButton.SetActive(false);

        Camera2.GetComponent<Camera>().enabled = false;
        Camera3.GetComponent<Camera>().enabled = true;
        cmCamera8.SetActive(true);
        cmCamera9.SetActive(true);
        cmCamera10.SetActive(true);

        animator.SetBool("Walk", true);

        iTween.RotateTo(MyC, iTween.Hash("y", 210f));

        iTween.MoveTo(MyC, iTween.Hash("x", -0.1f, "y", 0.2f, "z", 2.5f, "speed", 0.75f, 
            "EaseType", iTween.EaseType.linear,
            "oncomplete", "WalkStop",
            "oncompletetarget", this.gameObject));

        audioSource.PlayOneShot(YesSound);

        PlayTimeline3();
    }

    //いいえが押されたら
    public void OnReConfirmationNo()
    {
        RePanel.SetActive(false);
        ReYesButton.SetActive(false);
        ReNoButton.SetActive(false);

        SchoolIntroductionButton.SetActive(true);
        GameButton.SetActive(true);
        AppreciationButton.SetActive(true);
        ReCharacterCreateButton.SetActive(true);

        audioSource.PlayOneShot(NoSound);
    }

    //完了Buttonを押したとき
    private void Rotate2()
    {
        // カメラを切り替える
        //this.gameObject.GetComponent<Camera>().enabled = true;
        CCCamera.transform.position = CCCdefaultPos;
        CCCamera.transform.localEulerAngles = CCCdefaultRot;
        CCCamera.SetActive(false);
        MyCharDataManager.Instance.phase = Phase.SELECT;
    }

    // キャラクリまでの移動が終えた時に呼び出される関数
    private void MoveToCharaCreEnd(GameObject fade)
    {
        // カメラを切り替える
        this.gameObject.GetComponent<Camera>().enabled = false;
        Camera3.GetComponent<Camera>().enabled = false;
        cmCamera8.SetActive(false);
        cmCamera9.SetActive(false);
        cmCamera10.SetActive(false);
        CCCamera.SetActive(true);
        welcomeText.SetActive(false);

        // フェードインがまだ終わっていなければ何もしない
        if (fade.GetComponent<Fade>().IsFade != Fade.FADE_IN) return;

        // キャラクリ用のカメラを動かせるようにする
        CCCamera.GetComponent<CharaCreCameraCtrl>().MoveFlag = true;

        // 部位を変更するボタンと完了ボタンを表示
        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(true);
        }
        GameObject.Find("TypeViewArea").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("CharaCreButtonViewArea").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("ResetButton").transform.localScale = new Vector3(1, 1, 1);
        CharacterCreateEndButton.SetActive(true);
    }

    // フェードイン・アウトの管理
    private void FadeInOut(PlayableDirector pd, GameObject fade, double time)
    {
        // 一定時間経過したらフェードアウトする
        if (pd.time >= time && pd.state == PlayState.Playing)
        {
            fade.GetComponent<Fade>().FadeOut();
        }
        // フェードアウトし終えたらフェードインに移行
        if (fade.GetComponent<Fade>().IsFade == Fade.FADE_OUT)
        {
            fade.GetComponent<Fade>().FadeIn();
            MoveToCharaCreEnd(fade);
        }
    }
    
    //private void SelectUIActive()
    private void MoveToSelectEnd()
    {
        WalkStop();
        SchoolIntroductionButton.SetActive(true);
        GameButton.SetActive(true);
        AppreciationButton.SetActive(true);
        ReCharacterCreateButton.SetActive(true);
        iTween.RotateTo(MyC, iTween.Hash("y", 50f));
    }

    //止まる
    private void WalkStop()
    {
        animator.SetBool("Walk", false);
        iTween.RotateTo(MyC, iTween.Hash("y", 0f));
    }

    //開始
    void PlayTimeline1()
    {
        playableDirector1.Play();
    }
    //停止
    void StopTimeline1()
    {
        playableDirector1.Stop();
    }
    //開始
    void PlayTimeline2()
    {
        playableDirector2.Play();
    }
    //停止
    void StopTimeline2()
    {
        playableDirector2.Stop();
    }
    //開始
    void PlayTimeline3()
    {
        playableDirector3.Play();
    }
    //停止
    void StopTimeline3()
    {
        playableDirector3.Stop();
    }
}
