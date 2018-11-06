using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePChangeButton : MonoBehaviour
{
    [SerializeField]
    private Material resourceMatsYellow;          // 対応するマテリアル・イエローVer. 
    [SerializeField]
    private Material resourceMatsBlue;            // 対応するマテリアル・ブルーVer. 
    [SerializeField]
    private Material resourceMatsGreen;            // 対応するマテリアル・グリーンVer.

    [SerializeField]
    private EyePatternNum epn;                      // 目の模様の番号

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 目の色を変える        
        switch (MyCharDataManager.Instance.Data.ecn)
        {
            case EyeColorNum.YELLOW:
            default:
                MyCharDataManager.Instance.ChangeEyePattern(resourceMatsYellow);
                break;
            case EyeColorNum.BLUE:
                MyCharDataManager.Instance.ChangeEyePattern(resourceMatsBlue);
                break;
            case EyeColorNum.GREEN:
                MyCharDataManager.Instance.ChangeEyePattern(resourceMatsGreen);
                break;
        }

        // 目の色の番号を登録
        MyCharDataManager.Instance.Data.eyePNum = epn;
    }
}
