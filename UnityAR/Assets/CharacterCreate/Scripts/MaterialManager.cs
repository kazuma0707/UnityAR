using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    
    //// Unit /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Header("Unit")]
    [Header("ノーマルの色(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] clothNormalMatsUnit;              // ノーマルの色のマテリアルUnit
    [Header("制服の色(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] clothSeihukuMatsUnit;             // 制服の色のマテリアルUnit
    [Header("サイバーの色(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] clothCyberMatsUnit;               // サイバーの色のマテリアルUnit
    [Header("体操着の色(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] clothGymClothesMatsUnit;          // 体操着の色のマテリアルUnit

    [Header("ノーマルのSkin(N・B・W)Unit")]
    [SerializeField]
    private Material[] skinNormalMatsUnit;              // ノーマルのSkinのマテリアルUnit
    [Header("制服のSkin(N・B・W)Unit")]
    [SerializeField]
    private Material[] skinSeihukuMatsUnit;             // 制服のSkinのマテリアルUnit
    [Header("サイバーのSkin(N・B・W)Unit")]
    [SerializeField]
    private Material[] skinCyberMatsUnit;               // サイバーのSkinのマテリアルUnit
    [Header("体操着のSkin(N・B・W)Unit")]
    [SerializeField]
    private Material[] skinGymClothesMatsUnit;          // 体操着のSkinのマテリアルUnit

    [Header("前髪ありの肌色Face(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] longNormalFaceMatsUnit;           // 前髪ありの肌色FaceのマテリアルUnit
    [Header("前髪なしの肌色Face(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] shortNormalFaceMatsUnit;           // 前髪なしの肌色FaceのマテリアルUnit
    [Header("前髪ありの褐色Face(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] longBrownFaceMatsUnit;           // 前髪ありの褐色FaceのマテリアルUnit
    [Header("前髪なしの褐色Face(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] shortBrownFaceMatsUnit;           // 前髪なしの褐色FaceのマテリアルUnit
    [Header("前髪ありの美白Face(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] longWhiteFaceMatsUnit;           // 前髪ありの美白FaceのマテリアルUnit
    [Header("前髪なしの美白Face(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] shortWhiteFaceMatsUnit;           // 前髪なしの美白FaceのマテリアルUnit

    [Header("ショートヘア(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] shortHairMatsUnit;               // ショートヘアのマテリアルUnit
    [Header("ロングヘア(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] longHairMatsUnit;                // ロングヘアのマテリアルUnit
    [Header("ツインテール(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] twinTailMatsUnit;                // ツインテールのマテリアルUnit
    [Header("ポニーテール(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] ponyTailMatsUnit;                // ポニーテールのマテリアルUnit

    [Header("目パターン1(Y・B・G・R)Unit")]
    [SerializeField]
    private Material[] eyePatternOneMatsUnit;           // 目パターン1のマテリアルUnit
    [Header("目パターン2(Y・B・G・R)Unit")]
    [SerializeField]
    private Material[] eyePatternTwoMatsUnit;           // 目パターン2のマテリアルUnit
    [Header("目パターン3(Y・B・G・R)Unit")]
    [SerializeField]
    private Material[] eyePatternThreeMatsUnit;           // 目パターン3のマテリアルUnit

    private void Start()
    {
      
    }

    ///// マテリアルのアクセッサ ///////////////////////////////////////////////////////////////////////

    // Cloth ///////////////////////////////////////////

    // ノーマルの色のアクセッサ
    public Material[] ClothNormalMats
    {
        get { return clothNormalMatsUnit; }
    }

    // 制服の色のアクセッサ
    public Material[] ClothSeihukuMats
    {
        get { return clothSeihukuMatsUnit; }
    }

    // サイバーの色のアクセッサ
    public Material[] ClothCyberMats
    {
        get { return clothCyberMatsUnit; }
    }

    // 体操着の色のアクセッサ
    public Material[] ClotheGymMats
    {
        get { return clothGymClothesMatsUnit; }
    }

    // Skin ///////////////////////////////////////////

    // ノーマルのSkinのアクセッサ
    public Material[] SkinNormalMats
    {
        get { return skinNormalMatsUnit; }
    }

    // 制服のSkinのアクセッサ
    public Material[] SkinSeihukuMats
    {
        get { return skinSeihukuMatsUnit; }
    }

    // サイバーのSkinのアクセッサ
    public Material[] SkinCyberMats
    {
        get { return skinCyberMatsUnit; }
    }

    // 体操着のSkinのアクセッサ
    public Material[] SkinGymClothesMats
    {
        get { return skinGymClothesMatsUnit; }
    }

    // Face ///////////////////////////////////////////

    // 前髪ありの肌色Faceのアクセッサ
    public Material[] LongNormalFaceMats
    {
        get { return longNormalFaceMatsUnit; }
    }

    // 前髪なしの肌色Faceのアクセッサ
    public Material[] ShortNormalFaceMats
    {
        get { return shortNormalFaceMatsUnit; }
    }

    // 前髪ありの褐色Faceのアクセッサ
    public Material[] LongBrownFaceMats
    {
        get { return longBrownFaceMatsUnit; }
    }

    // 前髪なしの褐色Faceのアクセッサ
    public Material[] ShortBrownFaceMats
    {
        get { return shortBrownFaceMatsUnit; }
    }

    // 前髪ありの美白Faceのアクセッサ
    public Material[] LongWhiteFaceMats
    {
        get { return longWhiteFaceMatsUnit; }
    }

    // 前髪なしの美白Faceのアクセッサ
    public Material[] ShortWhiteFaceMats
    {
        get { return shortWhiteFaceMatsUnit; }
    }

    // Hair ///////////////////////////////////////////


    // ショートヘアのカラーのアクセッサ
    public Material[] ShortHairMats
    {
        get { return shortHairMatsUnit; }
    }

    // ロングヘアのカラーのアクセッサ
    public Material[] LongHairMats
    {
        get { return longHairMatsUnit; }
    }

    // ツインテールのカラーのアクセッサ
    public Material[] TwinTailMats
    {
        get { return twinTailMatsUnit; }
    }

    // ポニーテールのカラーのアクセッサ
    public Material[] PonyTailMats
    {
        get { return ponyTailMatsUnit; }
    }

    // Eye ///////////////////////////////////////////

    // 目パターン1のカラーのアクセッサ
    public Material[] EyePatternOneMats
    {
        get { return eyePatternOneMatsUnit; }
    }

    // 目パターン2のカラーのアクセッサ
    public Material[] EyePatternTwoMats
    {
        get { return eyePatternTwoMatsUnit; }
    }

    // 目パターン3のカラーのアクセッサ
    public Material[] EyePatternThreeMats
    {
        get { return eyePatternThreeMatsUnit; }
    }
}
