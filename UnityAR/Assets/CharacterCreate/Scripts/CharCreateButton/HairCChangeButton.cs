using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCChangeButton : MonoBehaviour
{
    [SerializeField]
    private Material resourceMatsShortHair;          // 対応するマテリアル・ショートヘアーVer.
    [SerializeField]
    private Material resourceMatsLongHair;           // 対応するマテリアル・ロングヘアーVer.
    [SerializeField]
    private Material resourceMatsTwinTail;           // 対応するマテリアル・ツインテールVer.

    [SerializeField]
    private HairColorNum hcn;                        // 髪の色の番号

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 髪型に合わせて髪(＋それに合わせた体の色)を変える
        switch (MyCharDataManager.Instance.Data.hairNum)
        {
            case HairNum.SHORT:
            default:
                // 髪の色を変える
                MyCharDataManager.Instance.ChangeHairColor(resourceMatsShortHair);
                break;
            case HairNum.LONG:
                // 髪の色を変える
                MyCharDataManager.Instance.ChangeHairColor(resourceMatsLongHair);
                break;
            case HairNum.TWIN:
                // 髪の色を変える
                MyCharDataManager.Instance.ChangeHairColor(resourceMatsTwinTail);
                break;
        }

        // 髪の色の番号を登録
        MyCharDataManager.Instance.Data.hcn = hcn;
    }
}
