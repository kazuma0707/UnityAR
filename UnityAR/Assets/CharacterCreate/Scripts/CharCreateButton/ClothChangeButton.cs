using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChangeButton : MonoBehaviour
{
    [Header("差し替えるモデル")]
    [Header("Unit用のモデル")]
    [SerializeField]
    private GameObject unitObj;                // Unit用のモデル

    private GameObject resourceObj;            // 差し替えるモデル

    [SerializeField]
    private ClothNum cn;                       // 服の番号

    [SerializeField]
    private MaterialManager matManager;         // マテリアルの管理するオブジェクト


    private Material[] mat = new Material[2];

    private void Start()
    {
        resourceObj = unitObj;
    }

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

        // 体の色に合わせて服(＋それに合わせた体の色)を変える
        switch (cn)
        {
            case ClothNum.NORMAL:
            default:
                // マテリアルを配列に設定
                FitSkinMaterial(matManager.SkinNormalMats);
                mat[MyCharDataManager.HEAD_COLOR] = MyCharDataManager.Instance.Data.bodyColor[MyCharDataManager.HEAD_COLOR];
                // 服を変える
                MyCharDataManager.Instance.ChangeClothObj(resourceObj, mat);
                // 服の色を変える
                FitClothColorMaterial(matManager.ClothNormalMats);
                break;
            case ClothNum.SEIHUKU:
                // マテリアルを配列に設定
                FitSkinMaterial(matManager.SkinSeihukuMats);
                mat[MyCharDataManager.HEAD_COLOR] = MyCharDataManager.Instance.Data.bodyColor[MyCharDataManager.HEAD_COLOR];
                // 服を変える
                MyCharDataManager.Instance.ChangeClothObj(resourceObj, mat);
                // 服の色を変える
                FitClothColorMaterial(matManager.ClothSeihukuMats);
                break;
            case ClothNum.CYBER:
                // マテリアルを配列に設定
                FitSkinMaterial(matManager.SkinCyberMats);
                mat[MyCharDataManager.HEAD_COLOR] = MyCharDataManager.Instance.Data.bodyColor[MyCharDataManager.HEAD_COLOR];
                // 服を変える
                MyCharDataManager.Instance.ChangeClothObj(resourceObj, mat);
                // 服の色を変える
                FitClothColorMaterial(matManager.ClothCyberMats);
                break;
            case ClothNum.GYM:
                // マテリアルを配列に設定
                FitSkinMaterial(matManager.SkinGymClothesMats);
                mat[MyCharDataManager.HEAD_COLOR] = MyCharDataManager.Instance.Data.bodyColor[MyCharDataManager.HEAD_COLOR];
                // 服を変える
                MyCharDataManager.Instance.ChangeClothObj(resourceObj, mat);
                // 服の色を変える
                FitClothColorMaterial(matManager.ClotheGymMats);
                break;
        }

        // 現在選択中の服を登録
        MyCharDataManager.Instance.Data.clothNum = cn;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色に合わせてSkinの色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitSkinMaterial(Material[] matMana)
    {
        // 体の色に合わせて服(＋それに合わせた体の色)を変える
        switch (MyCharDataManager.Instance.Data.bcn)
        {
            case BodyColorNum.NORMAL:
            default:
                // マテリアルを配列に設定
                mat[MyCharDataManager.BODY_COLOR] = matMana[(int)BodyColorNum.NORMAL];               
                break;
            case BodyColorNum.BROWN:
                // マテリアルを配列に設定
                mat[MyCharDataManager.BODY_COLOR] = matMana[(int)BodyColorNum.BROWN];
                break;
            case BodyColorNum.WHITE:
                // マテリアルを配列に設定
                mat[MyCharDataManager.BODY_COLOR] = matMana[(int)BodyColorNum.WHITE];
                break;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 服の色を変える
    // 　引　数   | matMana：マテリアルの管理するオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void FitClothColorMaterial(Material[] matMana)
    {
        // 服の色を変える
        switch (MyCharDataManager.Instance.Data.ccn)
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
