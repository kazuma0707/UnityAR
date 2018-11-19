using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [Header("ノーマルの色(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] clothNormalMats;              // ノーマルの色のマテリアル
    [Header("制服の色(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] clothSeihukuMats;             // 制服の色のマテリアル
    [Header("サイバーの色(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] clothCyberMats;               // サイバーの色のマテリアル

    [Header("ノーマルのSkin(N・B・W)")]
    [SerializeField]
    private Material[] skinNormalMats;              // ノーマルのSkinのマテリアル
    [Header("制服のSkin(N・B・W)")]
    [SerializeField]
    private Material[] skinSeihukuMats;             // 制服のSkinのマテリアル
    [Header("サイバーのSkin(N・B・W)")]
    [SerializeField]
    private Material[] skinCyberMats;               // サイバーのSkinのマテリアル

    [Header("前髪ありの肌色Face(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] longNormalFaceMats;           // 前髪ありの肌色Faceのマテリアル
    [Header("前髪なしの肌色Face(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] shortNormalFaceMats;           // 前髪なしの肌色Faceのマテリアル
    [Header("前髪ありの褐色Face(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] longBrownFaceMats;           // 前髪ありの褐色Faceのマテリアル
    [Header("前髪なしの褐色Face(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] shortBrownFaceMats;           // 前髪なしの褐色Faceのマテリアル
    [Header("前髪ありの美白Face(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] longWhiteFaceMats;           // 前髪ありの美白Faceのマテリアル
    [Header("前髪なしの美白Face(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] shortWhiteFaceMats;           // 前髪なしの美白Faceのマテリアル

    [Header("ショートヘア(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] shortHairMats;           // ショートヘアのマテリアル
    [Header("ロングヘア(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] longHairMats;           // ロングヘアのマテリアル
    [Header("ツインテール(P・Y・G・B・R)")]
    [SerializeField]
    private Material[] twinTailMats;           // ツインテールのマテリアル

    [Header("目パターン1(Y・B・G・R)")]
    [SerializeField]
    private Material[] eyePatternOneMats;           // 目パターン1のマテリアル
    [Header("目パターン2(Y・B・G・R)")]
    [SerializeField]
    private Material[] eyePatternTwoMats;           // 目パターン2のマテリアル
    [Header("目パターン3(Y・B・G・R)")]
    [SerializeField]
    private Material[] eyePatternThreeMats;           // 目パターン3のマテリアル

    ///////// マテリアルのアクセッサ ///////////////////////////////////////////////////////////////////////

    // Cloth ///////////////////////////////////////////

    // ノーマルの色のアクセッサ
    public Material[] ClothNormalMats
    {
        get { return clothNormalMats; }
    }

    // 制服の色のアクセッサ
    public Material[] ClothSeihukuMats
    {
        get { return clothSeihukuMats; }
    }

    // サイバーの色のアクセッサ
    public Material[] ClothCyberMats
    {
        get { return clothCyberMats; }
    }

    // Skin ///////////////////////////////////////////

    // ノーマルのSkinのアクセッサ
    public Material[] SkinNormalMats
    {
        get { return skinNormalMats; }
    }

    // 制服のSkinのアクセッサ
    public Material[] SkinSeihukuMats
    {
        get { return skinSeihukuMats; }
    }

    // サイバーのSkinのアクセッサ
    public Material[] SkinCyberMats
    {
        get { return skinCyberMats; }
    }

    // Face ///////////////////////////////////////////

    // 前髪ありの肌色Faceのアクセッサ
    public Material[] LongNormalFaceMats
    {
        get { return longNormalFaceMats; }
    }

    // 前髪なしの肌色Faceのアクセッサ
    public Material[] ShortNormalFaceMats
    {
        get { return shortNormalFaceMats; }
    }

    // 前髪ありの褐色Faceのアクセッサ
    public Material[] LongBrownFaceMats
    {
        get { return longBrownFaceMats; }
    }

    // 前髪なしの褐色Faceのアクセッサ
    public Material[] ShortBrownFaceMats
    {
        get { return shortBrownFaceMats; }
    }

    // 前髪ありの美白Faceのアクセッサ
    public Material[] LongWhiteFaceMats
    {
        get { return longWhiteFaceMats; }
    }

    // 前髪なしの美白Faceのアクセッサ
    public Material[] ShortWhiteFaceMats
    {
        get { return shortWhiteFaceMats; }
    }

    // Hair ///////////////////////////////////////////


    // ショートヘアのカラーのアクセッサ
    public Material[] ShortHairMats
    {
        get { return shortHairMats; }
    }

    // ロングヘアのカラーのアクセッサ
    public Material[] LongHairMats
    {
        get { return longHairMats; }
    }

    // ツインテールのカラーのアクセッサ
    public Material[] TwinTailMats
    {
        get { return twinTailMats; }
    }

    // Eye ///////////////////////////////////////////

    // 目パターン1のカラーのアクセッサ
    public Material[] EyePatternOneMats
    {
        get { return eyePatternOneMats; }
    }

    // 目パターン2のカラーのアクセッサ
    public Material[] EyePatternTwoMats
    {
        get { return eyePatternTwoMats; }
    }

    // 目パターン3のカラーのアクセッサ
    public Material[] EyePatternThreeMats
    {
        get { return eyePatternThreeMats; }
    }
}
