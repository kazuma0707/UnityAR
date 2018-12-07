using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [Header("表示させたいボタン")]
    [SerializeField]
    private GameObject[] buttons;       // 対応するボタン
    [Header("キャラクリ用カメラ")]
    [SerializeField]
    private GameObject cCCamera;        // キャラクリ用カメラ
    [Header("キャラクリ用カメラの位置番号")]
    [SerializeField]
    private CCCSetPosNum cCCSetPosNum;  // キャラクリ用カメラの位置番号   

    private GameObject parent;          // 親オブジェクト
    private bool active = false;        // アクティブ状態を管理

    // Use this for initialization
    void Start()
    {
        // 親オブジェクトを取得
        parent = this.transform.parent.gameObject;

        if (!cCCamera) cCCamera = GameObject.Find("CCCamera");
    }
	
	// Update is called once per frame
	void Update ()
    {
        // アクティブ状態を設定
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(active);
        }       
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 子Buttonを非表示にする
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ActiveFalse()
    {
        // 対応するボタンを非表示にする
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 子オブジェクトを検索
        ButtonScript[] child = parent.GetComponentsInChildren<ButtonScript>();
        foreach (ButtonScript obj in child)
        {
            // 子オブジェクトのタグがStatusButtonだったら表示設定をする
            if (obj.name != this.name) obj.Active = false;
        }
        // アクティブ状態を変える
        active = !active;

        // キャラクリ用カメラとViewPointを特定の位置に移動させる
        //cCCamera.GetComponent<CharaCreCameraCtrl>().CameraSetPos(cCCSetPosNum);
    }

    // activeのアクセッサ
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }


    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 非アクティブになったときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void OnDisable()
    {
        // 非表示にする
        active = false;
        ActiveFalse();
    }
}
