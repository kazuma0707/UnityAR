using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour {

    //ターゲット
    private GameObject Target;

    //Waypointsの数
    [SerializeField]
    private Transform[] points = new Transform[4];

    //現在の座標
    private int nowPoint = 0;

    //カメラ
    private NavMeshAgent Camera;
    //[SerializeField]
    //private GameObject CameraObject;

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

    //ターゲットを指定するオブジェクト
    //[SerializeField]
   // private GameObject LookAtObject;
    private Vector3 relativePos;

    // Use this for initialization
    void Start ()
    {

        //NavMeshの情報を取得
        Camera = GetComponent<NavMeshAgent>();

        //関数の呼び出し
        GettoNextPoint();

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
    }

    // Update is called once per frame
    void Update ()
    {
        //remainingDistanceとは↓
        //エージェントの位置および現在の経路での目標地点の間の距離（読み取り専用）
        // もし敵の目標地点と間の距離が小さかったら
        if (Camera.remainingDistance < 0.5f)
        {
            GettoNextPoint();
        }
        
    }

    //次の座標に移動するときの関数
    void GettoNextPoint()
    {
        //値が設定されてない場合return
        if (points.Length == 0)
        {
            return;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("New tag"))
        {
            Camera.destination = Target.transform.position;
        }

    }

    //スタートボタンをクリックしたら
    public void OnStartButton()
    {
        Camera.destination = points[0].position;

        StartButton.SetActive(false);
        CharacterCreateButton.SetActive(true);
    }

    //キャラクタークリエイトボタンを押したら
    public void OnCharacterCreate()
    {
        Camera.destination = points[1].position;

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(true);
        }

        CharacterCreateButton.SetActive(false);
        CharacterCreateEndButton.SetActive(true);
    }

    //完了ボタンを押したら
    public void OnCharacterCreateEndButton()
    {
        CharacterCreateEndButton.SetActive(false);

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(false);
        }

        Text.SetActive(true);
        Panel.SetActive(true);
        YesButton.SetActive(true);
        NoButton.SetActive(true);
    }

    //はいを押したら
    public void OnYesButton()
    {
        Camera.destination = points[2].position;

        SchoolIntroductionButton.SetActive(true);
        GameButton.SetActive(true);
        AppreciationButton.SetActive(true);
        ReCharacterCreateButton.SetActive(true);
        Text.SetActive(false);
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
    }

    //いいえを押したら
    public void OnNoButton()
    {
        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(true);
        }

        CharacterCreateEndButton.SetActive(true);
        Text.SetActive(false);
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
    }

    //学校紹介ボタンを押したら
    public void OnSchoolIntroduction()
    {
        SceneManager.LoadScene("ARScene");
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
        
        Camera.destination = points[1].position;
        Invoke("Rotate", 2.35f);

        for (int i = 0; i < ChangeButtons.Length; i++)
        {
            ChangeButtons[i].SetActive(true);
        }
        CharacterCreateEndButton.SetActive(true);
        SchoolIntroductionButton.SetActive(false);
        GameButton.SetActive(false);
        AppreciationButton.SetActive(false);
        ReCharacterCreateButton.SetActive(false);
    }

    void Rotate()
    {
        iTween.RotateTo(gameObject, iTween.Hash("y", -30f));
    }
    
}
