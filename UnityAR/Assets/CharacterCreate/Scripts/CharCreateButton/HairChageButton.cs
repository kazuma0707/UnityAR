using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairChageButton : MonoBehaviour
{
    [Header("差し替えるモデル")]
    [SerializeField]
    private GameObject resourceObj;            // 差し替えるモデル    

    [SerializeField]
    private HairNum hn;                        // 髪型の番号

    [SerializeField]
    private MaterialManager matManager;         // マテリアルの管理するオブジェクト

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

        // 髪の色に合わせて髪型を変える
        switch (hn)
        {
            case HairNum.SHORT:
            default:
                FitHairColorMaterial(matManager.ShortHairMats);
                // Faceの色を変える        
                FitFaceColorLongMaterial();
                break;
            case HairNum.LONG:
                FitHairColorMaterial(matManager.LongHairMats);
                // Faceの色を変える
                FitFaceColorLongMaterial();
                break;
            case HairNum.TWIN:
                FitHairColorMaterial(matManager.TwinTailMats);
                // Faceの色を変える
                FitFaceColorShortMaterial();
                break;
        }

        
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪の色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitHairColorMaterial(Material[] matMana)
    {
        // 体の色に合わせて服(＋それに合わせた体の色)を変える
        switch (MyCharDataManager.Instance.Data.hcn)
        {
            case HairColorNum.PINK:
            default:
                // 髪型を変える
                MyCharDataManager.Instance.ChangeHairObj(resourceObj, matMana[(int)HairColorNum.PINK]);       
                break;
            case HairColorNum.YELLOW:
                // 髪型を変える
                MyCharDataManager.Instance.ChangeHairObj(resourceObj, matMana[(int)HairColorNum.YELLOW]);
                break;
            case HairColorNum.GREEN:
                // 髪型を変える
                MyCharDataManager.Instance.ChangeHairObj(resourceObj, matMana[(int)HairColorNum.GREEN]);
                break;
            case HairColorNum.BLUE:
                // 髪型を変える
                MyCharDataManager.Instance.ChangeHairObj(resourceObj, matMana[(int)HairColorNum.BLUE]);
                break;
            case HairColorNum.RED:
                // 髪型を変える
                MyCharDataManager.Instance.ChangeHairObj(resourceObj, matMana[(int)HairColorNum.RED]);
                break;
        }

        // 現在選択中の髪を登録
        MyCharDataManager.Instance.Data.hairNum = hn;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色を調べる(Short)
    // 　引　数   | matMana：マテリアルの管理するオブジェクト(Skinマテリアル)
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitFaceColorShortMaterial()
    {
        // 体の色を調べる
        switch (MyCharDataManager.Instance.Data.bcn)
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
        // 体の色を調べる
        switch (MyCharDataManager.Instance.Data.bcn)
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
    // 関数の内容 | 髪の色に合わせてFaceの頭皮の色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト(Skinマテリアル)
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitFaceMaterial(Material[] matMana)
    {
        Material[] bodyMats = new Material[2];
        bodyMats[MyCharDataManager.BODY_COLOR] = MyCharDataManager.Instance.Data.bodyColor[MyCharDataManager.BODY_COLOR];
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
        // 体の色を変える
        MyCharDataManager.Instance.ChangeBodyColor(bodyMats);
    }
}
