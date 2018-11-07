using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLChangeButton : MonoBehaviour
{
    [SerializeField]
    private GameObject resourceObj;           // 差し替えるモデル
    
    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 目の模様を変える
        MyCharDataManager.Instance.ChangeEyeLineObj(resourceObj);
    }
}
