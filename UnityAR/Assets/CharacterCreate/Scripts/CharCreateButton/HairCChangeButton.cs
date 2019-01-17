using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCChangeButton : MonoBehaviour
{
    //[SerializeField]
    //private Color hairColor;                        // 髪の色の番号

    [SerializeField]
    private HairColorNum hcn;                        // 髪の色の番号

    [SerializeField]
    private MaterialManager matManager;             // マテリアルの管理するオブジェクト

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



        // 髪型を調べる
        switch (MyCharDataManager.Instance.Data.hairNum)
        {
            case HairNum.SHORT:
            default:
                FitHairColorMaterial(matManager.ShortHairMats);
                FitLongFaceColorMaterial();
                break;
            case HairNum.LONG:
                FitHairColorMaterial(matManager.LongHairMats);
                FitLongFaceColorMaterial();
                break;
            case HairNum.TWIN:
                FitHairColorMaterial(matManager.TwinTailMats);
                FitShortFaceColorMaterial();
                break;
        }

        // 髪の色の番号を登録
        MyCharDataManager.Instance.Data.hcn = hcn;

        // 髪の色を登録
        //ColorPicker picker = GameObject.Find("Picker 2.0").GetComponent<ColorPicker>();
        //picker.CurrentColor = hairColor;        
        //MyCharDataManager.Instance.Data.hairColor2 = hairColor;
        //MyCharDataManager.Instance.ChangeHairColor(hairColor);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪の色を調べる
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitHairColorMaterial(Material[] matMana)
    {
        MyCharDataManager.Instance.ChangeHairColor(matMana[(int)HairColorNum.PINK]);
        switch (hcn)
        {
            case HairColorNum.PINK:
            default:
                MyCharDataManager.Instance.ChangeHairColor(matMana[(int)HairColorNum.PINK]);
                break;
            case HairColorNum.YELLOW:
                MyCharDataManager.Instance.ChangeHairColor(matMana[(int)HairColorNum.YELLOW]);
                break;
            case HairColorNum.GREEN:
                MyCharDataManager.Instance.ChangeHairColor(matMana[(int)HairColorNum.GREEN]);
                break;
            case HairColorNum.BLUE:
                MyCharDataManager.Instance.ChangeHairColor(matMana[(int)HairColorNum.BLUE]);
                break;
            case HairColorNum.RED:
                MyCharDataManager.Instance.ChangeHairColor(matMana[(int)HairColorNum.RED]);
                break;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | Faceの色を調べる(Long)
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitLongFaceColorMaterial()
    {
        switch (MyCharDataManager.Instance.Data.bcn)
        {
            case BodyColorNum.NORMAL:
            default:
                FitFaceColorMaterial(matManager.LongNormalFaceMats);
                break;
            case BodyColorNum.BROWN:
                FitFaceColorMaterial(matManager.LongBrownFaceMats);
                break;
            case BodyColorNum.WHITE:
                FitFaceColorMaterial(matManager.LongWhiteFaceMats);
                break;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | Faceの色を調べる(Short)
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitShortFaceColorMaterial()
    {
        switch (MyCharDataManager.Instance.Data.bcn)
        {
            case BodyColorNum.NORMAL:
            default:
                FitFaceColorMaterial(matManager.ShortNormalFaceMats);
                break;
            case BodyColorNum.BROWN:
                FitFaceColorMaterial(matManager.ShortBrownFaceMats);
                break;
            case BodyColorNum.WHITE:
                FitFaceColorMaterial(matManager.ShortWhiteFaceMats);
                break;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | Faceの色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitFaceColorMaterial(Material[] matMana)
    {
        Material[] bodyMats = new Material[2];
        bodyMats[MyCharDataManager.BODY_COLOR] = MyCharDataManager.Instance.Data.bodyColor[MyCharDataManager.BODY_COLOR];
        switch (hcn)
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
        // 体の色を変える
        MyCharDataManager.Instance.ChangeBodyColor(bodyMats);
    }
}
