using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothCChangeButton : MonoBehaviour
{
    [SerializeField]
    private ClothColorNum ccn;                    // 服の色の番号

    [SerializeField]
    private MaterialManager matManager;           // マテリアルの管理するオブジェクト
    
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

        // 現在着ている服を調べる
        switch (MyCharDataManager.Instance.Data.clothNum)
        {
            case ClothNum.NORMAL:
            default:
                FitClothColorMaterial(matManager.ClothNormalMats);
                break;
            case ClothNum.SEIHUKU:
                FitClothColorMaterial(matManager.ClothSeihukuMats);
                break;
            case ClothNum.CYBER:
                FitClothColorMaterial(matManager.ClothCyberMats);
                break;
            case ClothNum.GYM:
                FitClothColorMaterial(matManager.ClotheGymMats);
                break;
        }

        // 服の色の番号を登録
        MyCharDataManager.Instance.Data.ccn = ccn;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 服の色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitClothColorMaterial(Material[] matMana)
    {
        // 服の色を変える
        switch (ccn)
        {
            case ClothColorNum.PINK:
            default:
                MyCharDataManager.Instance.ChangeClothColor(matMana[(int)ClothColorNum.PINK]);
                break;
            case ClothColorNum.YELLOW:
                MyCharDataManager.Instance.ChangeClothColor(matMana[(int)ClothColorNum.YELLOW]);
                break;
            case ClothColorNum.GREEN:
                MyCharDataManager.Instance.ChangeClothColor(matMana[(int)ClothColorNum.GREEN]);
                break;
            case ClothColorNum.BLUE:
                MyCharDataManager.Instance.ChangeClothColor(matMana[(int)ClothColorNum.BLUE]);
                break;
            case ClothColorNum.RED:
                MyCharDataManager.Instance.ChangeClothColor(matMana[(int)ClothColorNum.RED]);
                break;
        }
    }
}
