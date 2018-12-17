using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCreateManager : MonoBehaviour
{
   
    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 服を変える
    // 　引　数   | newCloth：髪型, bone：素体のBone
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeClothObj(GameObject newCloth, GameObject bone)
    {
        ChangeBone cB = new ChangeBone();
        cB.ChangeClothes(newCloth, bone);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 服の色を変える
    // 　引　数   | newColor：服の色, sotai：素体モデル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeClothColor(Material newColor, GameObject sotai)
    {
        // 色を設定       
        foreach (SkinnedMeshRenderer smr in sotai.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (smr.tag == "Clothes")
                smr.material = newColor;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪型を変える
    // 　引　数   | newHair：髪型, bone：素体のBone
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeHairObj(GameObject newHair, GameObject bone)
    {
        ChangeHair cH = new ChangeHair();
        cH.ChangeHairs(newHair, bone);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪の色を変える
    // 　引　数   | newColor：髪の色, sotai：素体モデル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeHairColor(Color newColor, GameObject sotai)
    {
        // 色を設定       
        foreach (SkinnedMeshRenderer smr in sotai.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (smr.tag == "HairObj")
            {                
                smr.material.SetColor(MyCharDataManager.BASE_COLOR, newColor);
                smr.material.SetColor(MyCharDataManager.SECOND_SHADE_COLOR, newColor);
            }
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体型を変える
    // 　引　数   | newScale：体型の登録番号, sotai：素体モデル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeBodyScale(BodyNum newScale, GameObject sotai)
    {
        // myCharの位置を保存
        //Vector3 pos = myChar.transform.position;
        Vector3[] scales = MyCharDataManager.Instance.BodyScales;
        // 体型を設定
        sotai.transform.localScale = scales[(int)newScale];
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色を変える
    // 　引　数   | newColor：体の色の配列, sotai：素体モデル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeBodyColor(Material[] newColor, GameObject sotai)
    {
        // 色を設定       
        foreach (SkinnedMeshRenderer smr in sotai.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            switch (smr.tag)
            {
                case "BodyObj":
                    smr.material = newColor[MyCharDataManager.BODY_COLOR];
                    break;
                case "HeadObj":
                    smr.material = newColor[MyCharDataManager.HEAD_COLOR];
                    break;
                default:
                    break;
            }
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の模様を変える
    // 　引　数   | newPattern：目の模様のマテリアル, sotai：素体モデル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeEyePattern(Material newPattern, GameObject sotai)
    {
        // マテリアルと色を設定
        foreach (SkinnedMeshRenderer smr in sotai.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (smr.tag == "eyePatternObj")
                smr.material = newPattern;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の形を変える
    // 　引　数   | bone：素体のBone
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeEyeLineObj(GameObject newLine, GameObject bone)
    {
        ChangeEyeLine cel = new ChangeEyeLine();
        cel.ChangeEyeLines(newLine, bone);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の色を変える
    // 　引　数   | newColor：目の色, sotai：素体モデル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    static public void ChangeEyeColor(Material newColor, GameObject sotai)
    {
        // 色を設定       
        foreach (SkinnedMeshRenderer smr in sotai.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (smr.tag == "eyePatternObj")
                smr.material = newColor;
        }
    }
}
