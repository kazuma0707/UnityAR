using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;//Convertを使う場合は忘れずに書く

public class InputManager : MonoBehaviour
{
    //-------定数--------//
    const int PASSWORD = 12345;

    //  リセットボタン
    [SerializeField]
    GameObject resetBotton;

    //  OKボタン
    [SerializeField]
    GameObject OKBotton;

    //  ミステキスト
    [SerializeField]
    GameObject missText;
    bool isRunning = false;
    [SerializeField]
    GameObject passText;

    InputField inputField;
    [SerializeField]
    GameObject gameManager;
    Coroutine missCoRoutine;

    int missTextTime = 0;
    bool missTextFlag = false;

    /// <summary>
    /// Startメソッド
    /// InputFieldコンポーネントの取得および初期化メソッドの実行
    /// </summary>
    void Start()
    {
        inputField = GetComponent<InputField>();

        InitInputField();
    }

    private void Update()
    {
        MissTextProcess();
    }

    /// <summary>
    /// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
    /// </summary>
    public void InputLogger()
    {

        string inputValue = inputField.text;

        Debug.Log(inputValue);

        InitInputField();
    }

    /// <summary>
    /// パスワード確認
    /// 
    /// </summary>
    public void InputPass()
    {

        int inputValue = 0;
        Int32.TryParse(inputField.text, out inputValue);

        if(inputValue == PASSWORD)
        {
            OKBotton.SetActive(false);
            resetBotton.SetActive(true);
            inputField.gameObject.SetActive(false);
            passText.SetActive(false);
        }
        else if(inputValue != PASSWORD && inputField.gameObject.activeSelf == true)
        {
            //missCoRoutine = StartCoroutine("MissCoRoutine");
            missTextFlag = true;
        }

        InitInputField();
    }


    /// <summary>
    /// InputFieldの初期化用メソッド
    /// 入力値をリセットして、フィールドにフォーカスする
    /// </summary>
    void InitInputField()
    {

        // 値をリセット
        inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }

    /****************************************************************
    *|　機能　ミステキスト関係の処理
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    void MissTextProcess()
    {
        if (missTextFlag == true)
        {
            missText.SetActive(true);
            missTextTime++;
        }
        if (missTextTime >= 100)
        {
            missText.SetActive(false);
            missTextFlag = false;
            missTextTime = 0;
        }
    }

    /****************************************************************
    *|　機能　ミステキスト関係の処理
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    public void MissTextReset()
    {
        missTextFlag = false;
        missTextTime = 0;
    }

    /****************************************************************
    *|　機能　ミステキストのコルーチン設定
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    IEnumerator MissCoRoutine()
    {
        //  実行中にもう一度コルーチンが呼び出されるのを防止
        if (isRunning) yield break;
        isRunning = true;

        //  ミステキストの表示
        missText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        missText.SetActive(false);

        isRunning = false;
    }
}
