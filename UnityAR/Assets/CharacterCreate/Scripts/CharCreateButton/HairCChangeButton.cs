using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCChangeButton : MonoBehaviour
{
    [SerializeField]
    private Material resourceMat;          // 対応するマテリアル

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 髪の色を変える
        MyCharDataManager.Instance.ChangeHairColor(resourceMat);
    }
}
