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
    private Animator UiAnimation;

    // Use this for initialization
    void Start()
    {
        // 親オブジェクトを取得
        parent = this.transform.parent.gameObject;
        UiAnimation = GameObject.Find("Canvas").GetComponent<Animator>();

        if (!cCCamera) cCCamera = GameObject.Find("CCCamera");
    }
	
	// Update is called once per frame
	void Update ()
    {
        
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
    public void OnClick(string objName)
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
        cCCamera.GetComponent<CharaCreCameraCtrl>().CameraSetPos(cCCSetPosNum);
        switch (objName)
        {
            case "HairButton":
                this.OnClickHairButton();
                break;
            case "EyeLineButton":
                this.OnClickEyeLineButton();
                break;
            case "EyePatternButton":
                this.OnClickEyePatternButton();
                break;
            case "BodyButton":
                this.OnClickBodyButton();
                break;
            case "ClothButton":
                this.OnClickClothButton();
                break;
        }



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

    //髪
    public void OnClickHairButton()
    {
        if(!UiAnimation.GetBool("isHair"))
        {
            UiAnimation.SetBool("isHair",true);
            UiAnimation.SetBool("isEyeLine", false);
            UiAnimation.SetBool("isEyepattern", false);
            UiAnimation.SetBool("isBody", false);
            UiAnimation.SetBool("isCloth", false);
        }
        else
        {
            UiAnimation.SetBool("isHair", false);
        }
    }
    //目の形
    public void OnClickEyeLineButton()
    {
        if (!UiAnimation.GetBool("isEyeLine"))
        {
            UiAnimation.SetBool("isEyeLine", true);
            UiAnimation.SetBool("isHair", false);
            UiAnimation.SetBool("isEyepattern", false);
            UiAnimation.SetBool("isBody", false);
            UiAnimation.SetBool("isCloth", false);
        }
        else
        {
            UiAnimation.SetBool("isEyeLine", false);
        }
    }
    //目の色
    public void OnClickEyePatternButton()
    {
        if (!UiAnimation.GetBool("isEyepattern"))
        {
            UiAnimation.SetBool("isEyepattern", true);
            UiAnimation.SetBool("isHair", false);
            UiAnimation.SetBool("isEyeLine", false);
            UiAnimation.SetBool("isBody", false);
            UiAnimation.SetBool("isCloth", false);
        }
        else
        {
            UiAnimation.SetBool("isEyepattern", false);
        }
    }
    //体
    public void OnClickBodyButton()
    {
        if (!UiAnimation.GetBool("isBody"))
        {
            UiAnimation.SetBool("isBody", true);
            UiAnimation.SetBool("isHair", false);
            UiAnimation.SetBool("isEyeLine", false);
            UiAnimation.SetBool("isEyepattern", false);
            UiAnimation.SetBool("isCloth", false);
        }
        else
        {
            UiAnimation.SetBool("isBody", false);
        }
    }
    //服
    public void OnClickClothButton()
    {
        if (!UiAnimation.GetBool("isCloth"))
        {
            UiAnimation.SetBool("isCloth", true);
            UiAnimation.SetBool("isHair", false);
            UiAnimation.SetBool("isEyeLine", false);
            UiAnimation.SetBool("isEyepattern", false);
            UiAnimation.SetBool("isBody", false);
        }
        else
        {
            UiAnimation.SetBool("isCloth", false);
        }
    }

    public  void HideAnimation()
    {
        UiAnimation.SetBool("isCloth", false);
        UiAnimation.SetBool("isHair", false);
        UiAnimation.SetBool("isEyeLine", false);
        UiAnimation.SetBool("isEyepattern", false);
        UiAnimation.SetBool("isBody", false);
    }

}
