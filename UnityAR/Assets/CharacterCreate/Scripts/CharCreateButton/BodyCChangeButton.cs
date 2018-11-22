using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCChangeButton : MonoBehaviour
{  
    [SerializeField]
    private BodyColorNum bcn;                    // 体の色の番号

    [SerializeField]
    private MaterialManager matManager;         // マテリアルの管理するオブジェクト

    private Material[] bodyMats = new Material[2];    // 0：skin, 1：face

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

        // 服とFaceの頭皮の色に合わせて体の色を変える
        switch (MyCharDataManager.Instance.Data.clothNum)
        {
            case ClothNum.NORMAL:
            default:
                FitSkinMaterial(matManager.SkinNormalMats);
                FitHairMaterial();
                MyCharDataManager.Instance.ChangeBodyColor(bodyMats);            
                break;
            case ClothNum.SEIHUKU:
                FitSkinMaterial(matManager.SkinSeihukuMats);
                FitHairMaterial();
                MyCharDataManager.Instance.ChangeBodyColor(bodyMats);
                break;
            case ClothNum.CYBER:
                FitSkinMaterial(matManager.SkinCyberMats);
                FitHairMaterial();
                MyCharDataManager.Instance.ChangeBodyColor(bodyMats);
                break;
        }

        // 体の色の番号を登録
        MyCharDataManager.Instance.Data.bcn = bcn;
    }   

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色に合わせてSkinの色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト(Skinマテリアル)
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitSkinMaterial(Material[] matMana)
    {
        // 体の色を変える
        switch (bcn)
        {
            case BodyColorNum.NORMAL:
            default:
                bodyMats[MyCharDataManager.BODY_COLOR] = matMana[(int)BodyColorNum.NORMAL];
                break;
            case BodyColorNum.BROWN:
                bodyMats[MyCharDataManager.BODY_COLOR] = matMana[(int)BodyColorNum.BROWN];
                break;
            case BodyColorNum.WHITE:
                bodyMats[MyCharDataManager.BODY_COLOR] = matMana[(int)BodyColorNum.WHITE];
                break;
        }
    }    

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪型を調べる
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitHairMaterial()
    {
        // 服とFaceの頭皮の色に合わせて体の色を変える
        switch (MyCharDataManager.Instance.Data.hairNum)
        {
            case HairNum.SHORT:
            case HairNum.LONG:
            default:
                FitFaceColorLongMaterial();
                break;
            case HairNum.TWIN:
                FitFaceColorShortMaterial();
                break;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色を調べる(Short)
    // 　引　数   | matMana：マテリアルの管理するオブジェクト(Skinマテリアル)
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitFaceColorShortMaterial()
    {
        // 体の色を変える
        switch (bcn)
        {
            case BodyColorNum.NORMAL:
            default:
                FitFaceMaterial(matManager.ShortNormalFaceMats);
                break;
            case BodyColorNum.BROWN:
                FitFaceMaterial(matManager.ShortBrownFaceMats);
                break;
            case BodyColorNum.WHITE:
                FitFaceMaterial(matManager.ShortWhiteFaceMats);
                break;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色を調べる(Long)
    // 　引　数   | matMana：マテリアルの管理するオブジェクト(Skinマテリアル)
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitFaceColorLongMaterial()
    {
        // 体の色を変える
        switch (bcn)
        {
            case BodyColorNum.NORMAL:
            default:
                FitFaceMaterial(matManager.LongNormalFaceMats);
                break;
            case BodyColorNum.BROWN:
                FitFaceMaterial(matManager.LongBrownFaceMats);
                break;
            case BodyColorNum.WHITE:
                FitFaceMaterial(matManager.LongWhiteFaceMats);
                break;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪の色に合わせてFaceの色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト(Skinマテリアル)
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitFaceMaterial(Material[] matMana)
    {
        // Faceの頭皮の色を変える
        switch (MyCharDataManager.Instance.Data.hcn)
        {
            case HairColorNum.PINK:
            default:
                bodyMats[MyCharDataManager.HEAD_COLOR] = matMana[(int)HairColorNum.PINK];
                break;
            case HairColorNum.YELLOW:
                bodyMats[MyCharDataManager.HEAD_COLOR] = matMana[(int)HairColorNum.YELLOW];
                break;
            case HairColorNum.GREEN:
                bodyMats[MyCharDataManager.HEAD_COLOR] = matMana[(int)HairColorNum.GREEN];
                break;
            case HairColorNum.BLUE:
                bodyMats[MyCharDataManager.HEAD_COLOR] = matMana[(int)HairColorNum.BLUE];
                break;
            case HairColorNum.RED:
                bodyMats[MyCharDataManager.HEAD_COLOR] = matMana[(int)HairColorNum.RED];
                break;
        }
    }
}
