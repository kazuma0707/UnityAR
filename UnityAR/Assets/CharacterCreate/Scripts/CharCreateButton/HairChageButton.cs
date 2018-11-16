using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairChageButton : MonoBehaviour
{
    [SerializeField]
    private GameObject resourceObj;            // 差し替えるモデル

    [SerializeField]
    private Material resourceMatsPink;         // 対応するマテリアル・ピンクVer.
    [SerializeField]
    private Material resourceMatsYellow;       // 対応するマテリアル・イエローVer.
    [SerializeField]
    private Material resourceMatsGreen;        // 対応するマテリアル・グリーンVer.

    [SerializeField]
    private HairNum hn;                        // 髪型の番号

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 髪の色に合わせて髪を変える
        switch (MyCharDataManager.Instance.Data.hcn)
        {
            case HairColorNum.PINK:
            default:
                // 髪型を変える
                MyCharDataManager.Instance.ChangeHairObj(resourceObj, resourceMatsPink);
                break;
            case HairColorNum.YELLOW:
                // 髪型を変える
                MyCharDataManager.Instance.ChangeHairObj(resourceObj, resourceMatsYellow);
                break;
            case HairColorNum.GREEN:
                // 髪型を変える
                MyCharDataManager.Instance.ChangeHairObj(resourceObj, resourceMatsGreen);
                break;
        }

        // 現在選択中の服を登録
        MyCharDataManager.Instance.Data.hairNum = hn;
    }
}
