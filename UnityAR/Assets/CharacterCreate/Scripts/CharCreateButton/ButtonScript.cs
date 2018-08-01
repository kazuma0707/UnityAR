using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;   // 対応するボタン
    
    private bool active = false;  // 表示フラグ

    // Use this for initialization
    void Start()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        // 表示フラグが下りていたら、Buttonを非表示にする
        if (!active) SetButtonActive(active);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | Buttonの表示を設定
    // 　引　数   | flag：表示フラグ
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void SetButtonActive(bool flag)
    {
        // 子オブジェクトを検索
        foreach (Transform child in transform)
        {
            // 子オブジェクトのタグがStatusButtonだったら表示設定をする
            if (child.tag == "StatusButton") child.gameObject.SetActive(flag);
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 他のButtonの表示フラグを下す
        MyCanvas.NotActiveAnother(gameObject.name);

        active = !active;

        // 子Buttonを表示又は非表示にする
        SetButtonActive(active);
    }

    // 表示フラグのアクセッサ
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }
}
