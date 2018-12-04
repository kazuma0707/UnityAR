using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //=============  定数　===============//
    // 幹の登録番号
    private const int RIGHT = 0;
    private const int LEFT = 1;

    // レベルが変わる時間
    private const float LEVEL1_TIME = 20.0f;//15.0f;
    private const float LEVEL2_TIME = 20.0f;//25.0f;
    private const float LEVEL3_TIME = 30.0f;//25.0f;

    // レベルに応じたスピード
    private const float LEVEL1_SPEED = 0.01f;
    private const float LEVEL2_SPEED = 0.03f;

    //  障害物の落下間隔
    private  float obstacleInterval = 5.0f;
    private const float OBSTACLE_INTERVAL_LEVEL2 = 0.75f;

    //  警告から障害物生成までの時間
    private const float WARNING_TIME = 1.5f;
    //  レベルアップテキスト表示時間
    private const float LEVEL_UP_TEXT_TIME = 2.0f;

    //  障害物の発生確率(obstaclePercent / 100)
    private int obstaclePercent = 35;
    private const int OBSTACLE_PERCENT_LEVEL2 = 90;

    //  警告表示の点滅時間(数字　大→点滅 早　小→点滅 遅)
    private const int FLASH_TIME = 5;
    //  レベルアップ表示の点滅時間(数字　大→点滅 早　小→点滅 遅)
    private const int LEVEL_UP_FLASH_TIME = 10;

    //  フェード時間
    private const float FADE_TIME = 2.0f;

    [SerializeField]
    private GameObject sotai;                // 素体モデル
    [SerializeField]
    private GameObject standPre;                // 台のプレハブ
    [SerializeField]
    private GameObject obstaclePre;             //  障害物プレハブ
    [SerializeField]
    private GameObject[] firstStands;           // 初期の台   
    [SerializeField]
    private Text scoreText;                         // スコア
    [SerializeField]
    private float fallSpeed;                    // 落下速度
    private int level = 1;                      // ゲームレベル
    private float timer = 0.0f;                 // タイマー
    private int levelScore = 0;                 //  レベルに応じた加算スコア
    private int score = 0;
    private float everySecond = 0.0f;
    public static int gameScore;                //  スコア(ランキングシーンへ共有するためpublic static)
    private float deletePos = -1.0f;            // 消える位置
    private Vector3 rightPos = new Vector3(0.7f, 7.0f, 3.5f);            // 右の台が生成される位置
    private Vector3 leftPos = new Vector3(-0.7f, 7.0f, 3.5f);            // 左の台が生成される位置
    private Vector3 centerPos = new Vector3(0.0f, 5.5f, 3.5f);          // 中央の障害物が生成される位置
    private List<GameObject> standList = new List<GameObject>();   // 台のリスト

    //  障害物関係
    float startTime = 0.0f;
    bool intervalFlag = false;
    [SerializeField]
    GameObject warningObject;
    int warningTime = 0;
    bool isRunning = false;

    //  レベルアップフラグ
    [SerializeField]
    GameObject LevelUpText;
    bool LevelUpFlag = false;

    //  ポーズフラグ
    public bool pauseFlag = false;

    private float count = 4.0f;             //  カウントダウン用変数
    [SerializeField]
    Text countDownText;

    private float waitGame = 0.0f;

    // Use this for initialization
    void Start()
    {
        //  static public変数の初期化
        gameScore = 0;

        // 素体モデルにデータを適用させる
        //MyCharDataManager.Instance.ReCreate(sotai);
        //MyCharDataManager.Instance.ChangeBodyScaleInGame(BodyNum.NORMAL_BODY);

        //  ゲームの停止
        GameStop();

        // 初期の台をリストに追加
        for (int i = 0; i < firstStands.Length; i++)
        {
            standList.Add(firstStands[i]);
        }

        StartCoroutine("TextCoRoutine");
    }

    // Update is called once per frame
    void Update()
    {

        // 時間計測
        timer += Time.deltaTime;
        everySecond -= Time.deltaTime;
        // タイムによってレベルを変える
        LevelChange();

        // 台が3つ以下になったら追加
        if (standList.Count < 4) CreateStem();

        // リスト検索
        for (int i = 0; i < standList.Count; i++)
        {
            // 台を下げる
            standList[i].transform.Translate(0, -fallSpeed, 0);
            // 一定の位置まで下がったら消す
            if (standList[i].transform.position.y <= deletePos)
            {
                Destroy(standList[i]);
                standList.RemoveAt(0);
                continue;
            }
        }

        //  毎秒スコアを加算する
        if (everySecond <= 0.0f && pauseFlag == false)
        {
            GameObject player = GameObject.FindGameObjectWithTag("TriggerCollider");
            float playerY = player.transform.position.y;
            bool obstaclFlag = player.GetComponent<UnityChanControlScriptWithRgidBody>().GetObstacleFlag();
            if (playerY >= -3.0f && obstaclFlag == false)
            {
                // タイムをテキストに表示
                gameScore += levelScore;//(int)timer * levelScore;
            }
            everySecond = 1.0f;
        }
        scoreText.text = gameScore.ToString();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            float num1 = standList[0].transform.position.y - standList[1].transform.position.y;
            Debug.Log(standList[0].name + " , " + standList[1].name + " 差 " + num1);
            float num2 = standList[1].transform.position.y - standList[2].transform.position.y;
            Debug.Log(standList[1].name + " , " + standList[2].name + " 差 " + num2);
        }

        if(timer >= LEVEL3_TIME)
        {
            obstacleInterval = OBSTACLE_INTERVAL_LEVEL2;
        }

        if (timer - startTime >= obstacleInterval)
        {
            intervalFlag = true;
            startTime = 0.0f;
        }

        //  警告オブジェクトがアクティブ状態なら
        if(warningObject.activeSelf)
        {
            float flash = Mathf.Abs(Mathf.Sin(Time.time * FLASH_TIME));
            //  点滅を行う
            warningObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 0.0f, flash);
        }

        //  レベルアップテキストがアクティブ状態なら
        if (LevelUpText.activeSelf)
        {
            float flash = Mathf.Abs(Mathf.Sin(Time.time * LEVEL_UP_FLASH_TIME));
            //  点滅を行う
            LevelUpText.GetComponent<Text>().color = new Color(1.0f, 0.0f, 0.0f, flash);
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 新しい台を生成
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void CreateStem()
    {
        GameObject obj = null;

        // ランダムで生成される台を決める
        int num = Random.Range(0, 2);
        int obstacleNum = Random.Range(0, 100);
        // 決められた新しい台を生成       
        switch (num)
        {
            case RIGHT:
            default:
                obj = Instantiate(standPre, rightPos, Quaternion.identity);
                break;
            case LEFT:
                obj = Instantiate(standPre, leftPos, Quaternion.identity);
                break;
        }

        //  障害物の生成
        if (timer >= LEVEL2_TIME)
        {
            //  レベル3になったら障害物の確率を上げる
            if(timer >= LEVEL3_TIME)
            {
                obstaclePercent = OBSTACLE_PERCENT_LEVEL2;
            }
            //  障害物を出すコルーチンを開始する
            if (intervalFlag && obstacleNum <= obstaclePercent)
            {
                StartCoroutine("coRoutine");
            }
        }

        // リストに追加
        standList.Add(obj);
    }
    
    /****************************************************************
    *|　機能　カウントダウンの表示
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    public void CountDown()
    {
        if(count > 0.0f)
        {
            count = count - Time.unscaledDeltaTime;
            countDownText.text = count.ToString("f0");
            if((int)count == 0)
            {
                countDownText.text = "";
            }
        }
        else
        {
            countDownText.enabled = false;
        }
    }

    /****************************************************************
    *|　機能　スタート時のゲーム停止処理
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    void GameStop()
    {
        pauseFlag = true;
        Time.timeScale = 0.0f;
    }

    /****************************************************************
    *|　機能 ゲーム再開処理(開始)
    *|　引数 なし
    *|　戻値 なし
    ***************************************************************/
    public void GameStart()
    {
        Time.timeScale = 1.0f;
        pauseFlag = false;
    }

    /****************************************************************
    *|　機能　テキストのコルーチン設定
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    IEnumerator TextCoRoutine()
    {
        //  実行中にもう一度コルーチンが呼び出されるのを防止
        if (isRunning) yield break;
        isRunning = true;

        yield return new WaitForSeconds(LEVEL2_TIME);

        LevelUpText.SetActive(true);
        //  テキストを消すまでの間隔
        yield return new WaitForSeconds(LEVEL_UP_TEXT_TIME);
        LevelUpText.SetActive(false);

        yield return new WaitForSeconds(LEVEL3_TIME - LEVEL2_TIME - LEVEL_UP_TEXT_TIME);

        LevelUpText.SetActive(true);
        //  テキストを消すまでの間隔
        yield return new WaitForSeconds(LEVEL_UP_TEXT_TIME);
        LevelUpText.SetActive(false);

        isRunning = false;
    }

    /****************************************************************
    *|　機能　障害物のコルーチン設定
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    IEnumerator coRoutine()
    {
        //  実行中にもう一度コルーチンが呼び出されるのを防止
        if (isRunning) yield break;
        isRunning = true;

        //  警告の表示
        warningObject.SetActive(true);
        //  障害物を出すまでの間隔
        yield return new WaitForSeconds(WARNING_TIME);
        //  警告表示を消す
        warningObject.SetActive(false);
        //  障害物を出す
        Instantiate(obstaclePre, centerPos, Quaternion.identity);
        intervalFlag = false;
        startTime = timer;

        isRunning = false;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | レベルを変える
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void LevelChange()
    {
        if(timer <= LEVEL1_TIME)
        {
            fallSpeed = LEVEL1_SPEED;
            level = 1;
            levelScore = 10;
        }
        else if (timer < LEVEL3_TIME)
        {
            fallSpeed = LEVEL2_SPEED;
            level = 2;
            levelScore = 100;
        }
        else if (timer >= LEVEL3_TIME)
        {
            levelScore = 1000;
        }
    }

    public float FallSpeed
    {
        get { return fallSpeed; }
    }
    public float Level
    {
        get { return level; }
    }


}
