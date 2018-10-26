using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{
    // メインカメラのチェックポイントの登録番号
    enum CheckPointNum
    {
        CHARACRE,
        SELECT
    }
    
    // ターゲット
    private GameObject Target;

    // メインカメラのチェックポイント
    [SerializeField]
    private GameObject[] cameraCheckPoint = new GameObject[2];

    // スタートボタン
    [SerializeField]
    private GameObject StartButton;
    // キャラクタークリエイトボタン
    [SerializeField]
    private GameObject CharacterCreateButton;
    // キャラクターの部位を変更するボタン
    [SerializeField]
    private GameObject[] ChangeButtons;
    // キャラクタークリエイトを完了するボタン
    [SerializeField]
    private GameObject CharacterCreateEndButton;
    // 確認テキスト
    [SerializeField]
    private GameObject Text;
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
    //[SerializeField]
    //private GameObject SchoolIntroductionButton;
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
    // テキスト
    [SerializeField]
    private GameObject ReText;
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

    // 音関連
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip YesSound;
    [SerializeField]
    private AudioClip NoSound;
    
    // Use this for initialization
    void Start ()
    {
        // 開発している画面を元に縦横比取得 (縦画面) iPhone6, 6sサイズ
        float developAspect = 1440.0f / 2990.0f;

        // 実機のサイズを取得して、縦横比取得
        float deviceAspect = (float)Screen.width / (float)Screen.height;

        // 実機と開発画面との対比
        float scale = deviceAspect / developAspect;

        //Camera mainCamera = Camera.main;
        Camera mainCamera = CCCamera.GetComponent<Camera>();

        // カメラに設定していたorthographicSizeを実機との対比でスケール
        float deviceSize = mainCamera.orthographicSize;
        // scaleの逆数
        float deviceScale = 1.0f / scale;
        // orthographicSizeを計算し直す
        mainCamera.orthographicSize = deviceSize * deviceScale;

        CCCdefaultPos = CCCamera.transform.position;
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        //UI非表示
        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(false);
        }
        CharacterCreateButton.SetActive(false);
        CharacterCreateEndButton.SetActive(false);
        Text.SetActive(false);
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
        //SchoolIntroductionButton.SetActive(false);
        GameButton.SetActive(false);
        AppreciationButton.SetActive(false);
        ReCharacterCreateButton.SetActive(false);
        ReText.SetActive(false);
        RePanel.SetActive(false);
        ReYesButton.SetActive(false);
        ReNoButton.SetActive(false);

        CCCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        
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
        //Invoke("Rotate1", 0.5f);
        
        // デバッグ用 //////////
        MoveToCharaCreEnd();
        ////////////////////////

        CharacterCreateButton.SetActive(false);        

        audioSource.PlayOneShot(YesSound);
    }

    //完了ボタンを押したら
    public void OnCharacterCreateEndButton()
    {
        CharacterCreateEndButton.SetActive(false);

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            if (ChangeButtons[i].GetComponent<ButtonScript>())
                ChangeButtons[i].GetComponent<ButtonScript>().ActiveFalse();
            ChangeButtons[i].SetActive(false);
        }
        CCCamera.GetComponent<CharaCreCameraCtrl>().MoveFlag = false;

        Text.SetActive(true);
        Panel.SetActive(true);
        YesButton.SetActive(true);
        NoButton.SetActive(true);
    }

    //はいを押したら
    public void OnYesButton()
    {       
        Invoke("Rotate2", 0.5f);

        //SchoolIntroductionButton.SetActive(true);
        GameButton.SetActive(true);
        AppreciationButton.SetActive(true);
        ReCharacterCreateButton.SetActive(true);
        Text.SetActive(false);
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        audioSource.PlayOneShot(YesSound);
    }

    //いいえを押したら
    public void OnNoButton()
    {
        CCCamera.GetComponent<CharaCreCameraCtrl>().MoveFlag = true;

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(true);
        }
        CharacterCreateEndButton.SetActive(true);
        Text.SetActive(false);
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        audioSource.PlayOneShot(NoSound);
    }

    //学校紹介ボタンを押したら
    public void OnSchoolIntroduction()
    {
        //SceneManager.LoadScene("SchoolIntroduction");
    }

    //ゲームボタンを押したら
    public void OnGame()
    {
        SceneManager.LoadScene("Title");
    }

    //鑑賞ボタンを押したら
    public void OnAppreciation()
    {
        SceneManager.LoadScene("Appreciation");
    }

    //キャラクタークリエイトをし直す
    public void OnReCharacterCreate()
    {        
        ReText.SetActive(true);
        RePanel.SetActive(true);
        ReYesButton.SetActive(true);
        ReNoButton.SetActive(true);

        Panel.SetActive(false);
        //SchoolIntroductionButton.SetActive(false);
        GameButton.SetActive(false);
        AppreciationButton.SetActive(false);
        ReCharacterCreateButton.SetActive(false);
    }

    //確認用ボタン
    //はいを押したら
    public void OnReConfirmationYes()
    {
        Invoke("Rotate3", 0.5f);       

        ReText.SetActive(false);
        RePanel.SetActive(false);
        ReYesButton.SetActive(false);
        ReNoButton.SetActive(false);

        audioSource.PlayOneShot(YesSound);
    }

    //いいえが押されたら
    public void OnReConfirmationNo()
    {
        ReText.SetActive(false);
        RePanel.SetActive(false);
        ReYesButton.SetActive(false);
        ReNoButton.SetActive(false);

        //SchoolIntroductionButton.SetActive(true);
        GameButton.SetActive(true);
        AppreciationButton.SetActive(true);
        ReCharacterCreateButton.SetActive(true);

        audioSource.PlayOneShot(NoSound);
    }

    //キャラクタークリエイトButtonを押したとき
    private void Rotate1()
    {
        // キャラクリ用のチェックポイントまで移動
        Vector3 pos = cameraCheckPoint[(int)CheckPointNum.CHARACRE].transform.position;
        iTween.MoveTo(this.gameObject, iTween.Hash("position", pos, "time", 6.0f,
                                                   "oncomplete", "MoveToCharaCreEnd",
                                                   "oncompletetarget", this.gameObject));
    }
    //完了Buttonを押したとき
    private void Rotate2()
    {
        // カメラを切り替える
        this.gameObject.GetComponent<Camera>().enabled = true;
        CCCamera.transform.position = CCCdefaultPos;
        CCCamera.SetActive(false);

        // セレクト用のチェックポイントまで移動
        Vector3 pos = cameraCheckPoint[(int)CheckPointNum.SELECT].transform.position;
        float rot = cameraCheckPoint[(int)CheckPointNum.SELECT].transform.eulerAngles.y;       
        iTween.MoveTo(this.gameObject, iTween.Hash("position", pos, "time", 7.0f));
        iTween.RotateTo(this.gameObject, iTween.Hash("y", rot, "time", 9.0f));
    }

    //キャラクタークリエイトをし直す
    private void Rotate3()
    {
        // キャラクリ用のチェックポイントまで移動
        Vector3 pos = cameraCheckPoint[(int)CheckPointNum.CHARACRE].transform.position;
        float rot = cameraCheckPoint[(int)CheckPointNum.CHARACRE].transform.eulerAngles.y;
        iTween.MoveTo(this.gameObject, iTween.Hash("position", pos, "time", 7.0f,
                                                   "oncomplete", "MoveToCharaCreEnd",
                                                   "oncompletetarget", this.gameObject));
        iTween.RotateTo(this.gameObject, iTween.Hash("y", rot, "time", 8.0f));
    }

    // キャラクリまでの移動が終えた時に呼び出される関数
    private void MoveToCharaCreEnd()
    {
        // カメラを切り替える
        this.gameObject.GetComponent<Camera>().enabled = false;
        CCCamera.SetActive(true);
        CCCamera.GetComponent<CharaCreCameraCtrl>().MoveFlag = true;

        // 部位を変更するボタンと完了ボタンを表示
        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(true);
        }
        CharacterCreateEndButton.SetActive(true);
    }
    
}
