using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{
    //ターゲット
    private GameObject Target;

    //現在の座標
    private int nowPoint = 0;

    //スタートボタン
    [SerializeField]
    private GameObject StartButton;
    //キャラクタークリエイトボタン
    [SerializeField]
    private GameObject CharacterCreateButton;
    //キャラクターの部位を変更するボタン
    [SerializeField]
    private GameObject[] ChangeButtons;
    //キャラクタークリエイトを完了するボタン
    [SerializeField]
    private GameObject CharacterCreateEndButton;
    //確認テキスト
    [SerializeField]
    private GameObject Text;
    //確認パネル
    [SerializeField]
    private GameObject Panel;
    //はいボタン
    [SerializeField]
    private GameObject YesButton;
    //いいえボタン
    [SerializeField]
    private GameObject NoButton;
    //学校紹介ボタン
    [SerializeField]
    private GameObject SchoolIntroductionButton;
    //ゲームボタン
    [SerializeField]
    private GameObject GameButton;
    //鑑賞ボタン
    [SerializeField]
    private GameObject AppreciationButton;
    //キャラクタークリエイトをし直すボタン
    [SerializeField]
    private GameObject ReCharacterCreateButton;
    
    ///////////////キャラクタークリエイトし直す////////////////
    //テキスト
    [SerializeField]
    private GameObject ReText;
    //パネル
    [SerializeField]
    private GameObject RePanel;
    //Yes
    [SerializeField]
    private GameObject ReYesButton;
    //No
    [SerializeField]
    private GameObject ReNoButton;
       
    // キャラクリ用のカメラ
    [SerializeField]
    private GameObject CCCamera;

    //音関連
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip YesSound;
    [SerializeField]
    private AudioClip NoSound;

    // Use this for initialization
    void Start ()
    {

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
        SchoolIntroductionButton.SetActive(false);
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
        StartButton.SetActive(false);
        CharacterCreateButton.SetActive(true);

        audioSource.PlayOneShot(YesSound);
    }

    //キャラクタークリエイトボタンを押したら
    public void OnCharacterCreate()
    {
        Invoke("Rotate1", 0.5f);

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(true);
        }

        CharacterCreateButton.SetActive(false);
        CharacterCreateEndButton.SetActive(true);

        audioSource.PlayOneShot(YesSound);
    }

    //完了ボタンを押したら
    public void OnCharacterCreateEndButton()
    {
        CharacterCreateEndButton.SetActive(false);

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
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

        SchoolIntroductionButton.SetActive(true);
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
        SceneManager.LoadScene("SchoolIntroduction");
    }

    //ゲームボタンを押したら
    public void OnGame()
    {
        SceneManager.LoadScene("Game");
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
        SchoolIntroductionButton.SetActive(false);
        GameButton.SetActive(false);
        AppreciationButton.SetActive(false);
        ReCharacterCreateButton.SetActive(false);
    }

    //確認用ボタン
    //はいを押したら
    public void OnReConfirmationYes()
    {
        Invoke("Rotate3", 0.5f);

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(true);
        }

        CharacterCreateEndButton.SetActive(true);

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

        SchoolIntroductionButton.SetActive(true);
        GameButton.SetActive(true);
        AppreciationButton.SetActive(true);
        ReCharacterCreateButton.SetActive(true);

        audioSource.PlayOneShot(NoSound);
    }

    //キャラクタークリエイトButtonを押したとき
    void Rotate1()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("z", -8.5f, "time", 6.0f,
        "oncomplete", "MoveToCharaCreEnd",
        "oncompletetarget", this.gameObject));
        iTween.RotateTo(this.gameObject, iTween.Hash("y", 160.0f, "time", 8.0f));
    }
    //完了Buttonを押したとき
    void Rotate2()
    {
        this.gameObject.GetComponent<Camera>().enabled = true;
        CCCamera.SetActive(false);

        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(4.0f, 0.8f, 3.5f), "time", 7.0f));
        iTween.RotateTo(this.gameObject, iTween.Hash("y", 100.0f, "time", 9.0f));
    }

    //キャラクタークリエイトをし直す
    void Rotate3()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(0.8f, 0.8f, -8.3f), "time", 7.0f,
        "oncomplete", "MoveToCharaCreEnd",
        "oncompletetarget", this.gameObject));
        iTween.RotateTo(this.gameObject, iTween.Hash("y", 180.0f, "time", 8.0f));
    }

    // セレクト画面からキャラクリに戻り終えた時に呼び出される関数
    void MoveToCharaCreEnd()
    {
        Debug.Log("aaa");
        this.gameObject.GetComponent<Camera>().enabled = false;
        CCCamera.SetActive(true);
        CCCamera.GetComponent<CharaCreCameraCtrl>().MoveFlag = true;
    }

}
