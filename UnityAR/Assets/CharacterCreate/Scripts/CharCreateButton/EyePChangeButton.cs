using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePChangeButton : MonoBehaviour
{
    [SerializeField]
    private EyePatternNum epn;                 // 目の模様の番号

    [SerializeField]
    private MaterialManager matManager;        // マテリアルの管理するオブジェクト

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // マテリアルの管理するオブジェクトが無い場合、再設定
        if (!matManager)
            matManager = GameObject.Find("MaterialManager").GetComponent<MaterialManager>();

        // 目の色を変える        
        switch (epn)
        {
            case EyePatternNum.NORMAL:
            default:
                FitEyeColorMaterial(matManager.EyePatternOneMats);                
                break;
            case EyePatternNum.NUM_2:
                FitEyeColorMaterial(matManager.EyePatternTwoMats);
                break;
            case EyePatternNum.NUM_3:
                FitEyeColorMaterial(matManager.EyePatternThreeMats);
                break;
        }

        // 目の色の番号を登録
        MyCharDataManager.Instance.Data.eyePNum = epn;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitEyeColorMaterial(Material[] matMana)
    {
        // 目の色を変える        
        switch (MyCharDataManager.Instance.Data.ecn)
        {
            case EyeColorNum.YELLOW:
            default:
                MyCharDataManager.Instance.ChangeEyePattern(matMana[(int)EyeColorNum.YELLOW]);
                break;
            case EyeColorNum.BLUE:
                MyCharDataManager.Instance.ChangeEyePattern(matMana[(int)EyeColorNum.BLUE]);
                break;
            case EyeColorNum.GREEN:
                MyCharDataManager.Instance.ChangeEyePattern(matMana[(int)EyeColorNum.GREEN]);
                break;
            case EyeColorNum.RED:
                MyCharDataManager.Instance.ChangeEyePattern(matMana[(int)EyeColorNum.RED]);
                break;
        }
    }
}
