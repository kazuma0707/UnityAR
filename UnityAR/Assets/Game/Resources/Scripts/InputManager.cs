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

    //  ミステキスト
    [SerializeField]
    GameObject missText;
    bool isRunning = false;

    InputField inputField;
    [SerializeField]
    GameObject gameManager;
    Coroutine missCoRoutine;

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
        if (gameManager.GetComponent<Ranking>().modeFlag == false)
        {
            StopCoroutine(missCoRoutine);
        }
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
            resetBotton.SetActive(true);
        }
        else if(inputValue != PASSWORD && inputField.gameObject.activeSelf == true)
        {
            missCoRoutine = StartCoroutine("MissCoRoutine");
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
