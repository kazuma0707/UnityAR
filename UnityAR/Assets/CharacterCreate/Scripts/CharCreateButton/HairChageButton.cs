using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairChageButton : MonoBehaviour
{
    [SerializeField]
    private GameObject resourceObj;            // 差し替えるモデル
    
    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 髪型を変える
        MyCharDataManager.Instance.ChangeHairObj(resourceObj);
    }
}
