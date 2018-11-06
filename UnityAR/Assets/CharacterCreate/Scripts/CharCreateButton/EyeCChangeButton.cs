using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCChangeButton : MonoBehaviour
{
    [SerializeField]
    private Material resourceMatsNormal;          // 対応するマテリアル・ノーマルVer. 
    [SerializeField]
    private Material resourceMatsNum2;            // 対応するマテリアル・なんか２Ver. 
    [SerializeField]
    private Material resourceMatsNum3;            // 対応するマテリアル・なんか３Ver.

    [SerializeField]
    private EyeColorNum ecn;                      // 目の色の番号

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 目の色を変える        
        switch (MyCharDataManager.Instance.Data.eyePNum)
        {
            case EyePatternNum.NORMAL:
            default:
                MyCharDataManager.Instance.ChangeEyeColor(resourceMatsNormal);
                break;
            case EyePatternNum.NUM_2:
                MyCharDataManager.Instance.ChangeEyeColor(resourceMatsNum2);
                break;
            case EyePatternNum.NUM_3:
                MyCharDataManager.Instance.ChangeEyeColor(resourceMatsNum3);
                break;
        }

        // 目の色の番号を登録
        MyCharDataManager.Instance.Data.ecn = ecn;
    }
}
