using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCanvas : MonoBehaviour
{
    static private Canvas canvas;

    // Use this for initialization
    void Start()
    {
        // Canvasコンポーネントを保持
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 指定したボタンの表示・非表示を設定する
    // 　引　数   | name：ボタンの名前, flag：表示フラグ
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public static void SetActive(string name, bool flag)
    {
        // 子オブジェクトを検索
        foreach (Transform child in canvas.transform)
        {
            // 指定した名前と一致
            if (child.name == name)
            {                
                // 表示フラグを設定
                child.gameObject.SetActive(flag);
                return;
            }
        }
        // 指定したオブジェクト名が見つからなかった
        Debug.LogWarning("Not found objname:" + name);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 全てのボタンの表示・非表示を設定する
    // 　引　数   | name：ボタンの名前
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public static void NotActiveAnother(string name)
    {
        // 子の要素を辿る
        foreach (Transform child in canvas.transform)
        {
            // 指定した名前以外の表示フラグを下す
            if (child.name != name && child.tag == "BaseButton")
            {
                child.gameObject.GetComponent<ButtonScript>().Active = false;
            }
        }
    }
    
}
