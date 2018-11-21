using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField]
    private MyCharDataManager mdm;                   // マイキャラのデータマネージャー

    private int materialVerion;                      // マテリアルのVerion

    //// Normal TS /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Header("Normal TS")]

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

//// Teleport /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Header("Teleport")]
    [Header("ノーマルの色(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] clothNormalMatsTL;              // ノーマルの色のマテリアルTL
    [Header("制服の色(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] clothSeihukuMatsTL;             // 制服の色のマテリアルTL
    [Header("サイバーの色(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] clothCyberMatsTL;               // サイバーの色のマテリアルTL

    [Header("ノーマルのSkin(N・B・W)TL")]
    [SerializeField]
    private Material[] skinNormalMatsTL;              // ノーマルのSkinのマテリアルTL
    [Header("制服のSkin(N・B・W)TL")]
    [SerializeField]
    private Material[] skinSeihukuMatsTL;             // 制服のSkinのマテリアルTL
    [Header("サイバーのSkin(N・B・W)TL")]
    [SerializeField]
    private Material[] skinCyberMatsTL;               // サイバーのSkinのマテリアルTL

    [Header("前髪ありの肌色Face(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] longNormalFaceMatsTL;           // 前髪ありの肌色FaceのマテリアルTL
    [Header("前髪なしの肌色Face(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] shortNormalFaceMatsTL;           // 前髪なしの肌色FaceのマテリアルTL
    [Header("前髪ありの褐色Face(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] longBrownFaceMatsTL;           // 前髪ありの褐色FaceのマテリアルTL
    [Header("前髪なしの褐色Face(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] shortBrownFaceMatsTL;           // 前髪なしの褐色FaceのマテリアルTL
    [Header("前髪ありの美白Face(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] longWhiteFaceMatsTL;           // 前髪ありの美白FaceのマテリアルTL
    [Header("前髪なしの美白Face(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] shortWhiteFaceMatsTL;           // 前髪なしの美白FaceのマテリアルTL

    [Header("ショートヘア(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] shortHairMatsTL;           // ショートヘアのマテリアルTL
    [Header("ロングヘア(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] longHairMatsTL;           // ロングヘアのマテリアルTL
    [Header("ツインテール(P・Y・G・B・R)TL")]
    [SerializeField]
    private Material[] twinTailMatsTL;           // ツインテールのマテリアルTL

    [Header("目パターン1(Y・B・G・R)TL")]
    [SerializeField]
    private Material[] eyePatternOneMatsTL;           // 目パターン1のマテリアルTL
    [Header("目パターン2(Y・B・G・R)TL")]
    [SerializeField]
    private Material[] eyePatternTwoMatsTL;           // 目パターン2のマテリアルTL
    [Header("目パターン3(Y・B・G・R)TL")]
    [SerializeField]
    private Material[] eyePatternThreeMatsTL;           // 目パターン3のマテリアルTL

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

    [Header("ノーマルのSkin(N・B・W)Unit")]
    [SerializeField]
    private Material[] skinNormalMatsUnit;              // ノーマルのSkinのマテリアルUnit
    [Header("制服のSkin(N・B・W)Unit")]
    [SerializeField]
    private Material[] skinSeihukuMatsUnit;             // 制服のSkinのマテリアルUnit
    [Header("サイバーのSkin(N・B・W)Unit")]
    [SerializeField]
    private Material[] skinCyberMatsUnit;               // サイバーのSkinのマテリアルUnit

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
    private Material[] shortHairMatsUnit;           // ショートヘアのマテリアルUnit
    [Header("ロングヘア(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] longHairMatsUnit;           // ロングヘアのマテリアルUnit
    [Header("ツインテール(P・Y・G・B・R)Unit")]
    [SerializeField]
    private Material[] twinTailMatsUnit;           // ツインテールのマテリアルUnit

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
        // マイキャラのデータマネージャーの再設定
        if (!mdm)
            mdm = GameObject.Find("MyCharDataManager").GetComponent<MyCharDataManager>();

        materialVerion = mdm.MaterialVerion;
    }

    ///// マテリアルのアクセッサ ///////////////////////////////////////////////////////////////////////

    // Cloth ///////////////////////////////////////////

    // ノーマルの色のアクセッサ
    public Material[] ClothNormalMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch(materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:                    
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    clothNormalMats = clothNormalMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    clothNormalMats = clothNormalMatsUnit;
                    break;
            }
            return clothNormalMats;
        }
    }

    // 制服の色のアクセッサ
    public Material[] ClothSeihukuMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    clothSeihukuMats = clothSeihukuMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    clothSeihukuMats = clothSeihukuMatsUnit;
                    break;
            }
            return clothSeihukuMats;
        }
    }

    // サイバーの色のアクセッサ
    public Material[] ClothCyberMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    clothCyberMats = clothCyberMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    clothCyberMats = clothCyberMatsUnit;
                    break;
            }
            return clothCyberMats;
        }
    }

    // Skin ///////////////////////////////////////////

    // ノーマルのSkinのアクセッサ
    public Material[] SkinNormalMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    skinNormalMats = skinNormalMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    skinNormalMats = skinNormalMatsUnit;
                    break;
            }
            return skinNormalMats;
        }
    }

    // 制服のSkinのアクセッサ
    public Material[] SkinSeihukuMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    skinSeihukuMats = skinSeihukuMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    skinSeihukuMats = skinSeihukuMatsUnit;
                    break;
            }
            return skinSeihukuMats;
        }
    }

    // サイバーのSkinのアクセッサ
    public Material[] SkinCyberMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    skinCyberMats = skinCyberMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    skinCyberMats = skinCyberMatsUnit;
                    break;
            }
            return skinCyberMats;
        }
    }

    // Face ///////////////////////////////////////////

    // 前髪ありの肌色Faceのアクセッサ
    public Material[] LongNormalFaceMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    longNormalFaceMats = longNormalFaceMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    longNormalFaceMats = longNormalFaceMatsUnit;
                    break;
            }
            return longNormalFaceMats;
        }
    }

    // 前髪なしの肌色Faceのアクセッサ
    public Material[] ShortNormalFaceMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    shortNormalFaceMats = shortNormalFaceMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    shortNormalFaceMats = shortNormalFaceMatsUnit;
                    break;
            }
            return shortNormalFaceMats;
        }
    }

    // 前髪ありの褐色Faceのアクセッサ
    public Material[] LongBrownFaceMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    longBrownFaceMats = longBrownFaceMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    longBrownFaceMats = longBrownFaceMatsUnit;
                    break;
            }
            return longBrownFaceMats;
        }
    }

    // 前髪なしの褐色Faceのアクセッサ
    public Material[] ShortBrownFaceMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    shortBrownFaceMats = shortBrownFaceMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    shortBrownFaceMats = shortBrownFaceMatsUnit;
                    break;
            }
            return shortBrownFaceMats;
        }
    }

    // 前髪ありの美白Faceのアクセッサ
    public Material[] LongWhiteFaceMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    longWhiteFaceMats = longWhiteFaceMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    longWhiteFaceMats = longWhiteFaceMatsUnit;
                    break;
            }
            return longWhiteFaceMats;
        }
    }

    // 前髪なしの美白Faceのアクセッサ
    public Material[] ShortWhiteFaceMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    shortWhiteFaceMats = shortWhiteFaceMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    shortWhiteFaceMats = shortWhiteFaceMatsUnit;
                    break;
            }
            return shortWhiteFaceMats;
        }
    }

    // Hair ///////////////////////////////////////////


    // ショートヘアのカラーのアクセッサ
    public Material[] ShortHairMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    shortHairMats = shortHairMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    shortHairMats = shortHairMatsUnit;
                    break;
            }
            return shortHairMats;
        }
    }

    // ロングヘアのカラーのアクセッサ
    public Material[] LongHairMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    longHairMats = longHairMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    longHairMats = longHairMatsUnit;
                    break;
            }
            return longHairMats;
        }
    }

    // ツインテールのカラーのアクセッサ
    public Material[] TwinTailMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    twinTailMats = twinTailMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    twinTailMats = twinTailMatsUnit;
                    break;
            }
            return twinTailMats;
        }
    }

    // Eye ///////////////////////////////////////////

    // 目パターン1のカラーのアクセッサ
    public Material[] EyePatternOneMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    eyePatternOneMats = eyePatternOneMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    eyePatternOneMats = eyePatternOneMatsUnit;
                    break;
            }
            return eyePatternOneMats;
        }
    }

    // 目パターン2のカラーのアクセッサ
    public Material[] EyePatternTwoMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    eyePatternTwoMats = eyePatternTwoMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    eyePatternTwoMats = eyePatternTwoMatsUnit;
                    break;
            }
            return eyePatternTwoMats;
        }
    }

    // 目パターン3のカラーのアクセッサ
    public Material[] EyePatternThreeMats
    {
        get
        {
            // マテリアルのVerionに合わせて渡す物を変える
            switch (materialVerion)
            {
                case MyCharDataManager.MATERIAL_VERSION_NORMAL:
                default:
                    break;
                case MyCharDataManager.MATERIAL_VERSION_TELEPORT:
                    eyePatternThreeMats = eyePatternThreeMatsTL;
                    break;
                case MyCharDataManager.MATERIAL_VERSION_UNIT:
                    eyePatternThreeMats = eyePatternThreeMatsUnit;
                    break;
            }
            return eyePatternThreeMats;
        }
    }
}
