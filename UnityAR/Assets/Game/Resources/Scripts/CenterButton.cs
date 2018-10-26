using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterButton : MonoBehaviour
{

    [SerializeField]
    GameObject script;                      //  プレイヤーのスクリプト
    ColorBlock startCB;                     //  初期のボタンカラー

    // Use this for initialization
    void Start()
    {
        startCB = this.GetComponent<Button>().colors;
    }

    // Update is called once per frame
    void Update()
    {
        //  ジャンプ不可能な状態なら
        if (script.GetComponent<UnityChanControlScriptWithRgidBody>().GetJumpPossibleFlag() == false)
        {
            //  ボタンの透明度を変える
            ButtonAlphaChange();
        }
        else
        {
            this.GetComponent<Button>().colors = startCB;
        }
    }

    public void OnClickButton()
    {
        script.GetComponent<UnityChanControlScriptWithRgidBody>().CenterButtonFlag = true;
    }

    /****************************************************************
   *|　機能　ボタンの透明度を変える
   *|　引数　なし
   *|　戻値　なし
   ***************************************************************/
    void ButtonAlphaChange()
    {
        Color color = new Color(1, 1, 1, 0.3f);                 //  通常時のボタン透明度
        Color pressColor = new Color(1, 1, 1, 0.1f);            //  ボタン押下時の透明度
        ColorBlock cb = this.GetComponent<Button>().colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = pressColor;
        this.GetComponent<Button>().colors = cb;
    }
}