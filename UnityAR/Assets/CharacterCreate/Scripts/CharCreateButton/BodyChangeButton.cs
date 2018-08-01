using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyChangeButton : MonoBehaviour
{
    [SerializeField]
    private BodyNum bodyNum;        // 体型の登録番号

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 体型を設定
        MyCharDataManager.Instance.ChangeBodyObj(bodyNum);
        MyCharDataManager.Instance.BodyNumber = bodyNum;
    }
}
