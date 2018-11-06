using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 体型の登録番号
public enum BodyNum
{
    SMALL_BODY,
    NORMAL_BODY,
    BIG_BODY
}

// 服の登録番号
public enum ClothNum
{
    NORMAL,
    SEIHUKU,
    NANKA
}

// 体の色の登録番号
public enum BodyColorNum
{
    NORMAL,
    BROWN,
    BIHAKU
}

// 目の模様の登録番号
public enum EyePatternNum
{
    NORMAL,
    NUM_2,
    NUM_3
}

// 目の色の登録番号
public enum EyeColorNum
{
    YELLOW,
    BLUE,
    GREEN
}

// 髪型の登録番号
public enum HairNum
{
    SHORT,
    LONG,
    TWIN
}

// 髪の色の登録番号
public enum HairColorNum
{
    PINK,
    YELLOW,
    GREEN
}

// 変更する部の登録番号
//public enum ChangingPoint
//{
//    SKIN,
//    BODY,
//    EYE_LINE,
//    HEAD,
//    HAIR,
//    EYE_PATTERN,
//}

public class MyCharDataManager : MonoBehaviour
{
    
    // シングルトン ///////////////////////////////////////////////////////////////////////////
    #region Singleton

    private static MyCharDataManager instance;      // MyCharDataManagerのインスタンス

    public static MyCharDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (MyCharDataManager)FindObjectOfType(typeof(MyCharDataManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(MyCharDataManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    //////////////////////////////////////////////////////////////////////////////////////////

    // マイキャラのデータの集合体クラス
    public class MyCharData 
    {
        public GameObject hair;         // 髪型
        public Material hairColor;      // 髪の色
        public GameObject eyeLine;      // 目の形
        public Material eyePattern;     // 目の模様
        public GameObject cloth;        // 服
        public BodyNum bodyScale;       // 体型の番号
        public Material[] bodyColor;    // 体の色

        public ClothNum clothNum;       // 服の種類の番号
        public BodyColorNum bcn;        // 体の色の番号

        public EyePatternNum eyePNum;   // 目の模様の番号
        public EyeColorNum ecn;         // 目の色の番号

        public HairNum hairNum;         // 髪型の番号
        public HairColorNum hcn;        // 髪の色の番号
    }

    public const int LEFT_EYE = 0;        // 左目
    public const int RIGHT_EYE = 1;       // 右目
    public const int BODY_COLOR = 0;      // 体の色
    public const int HEAD_COLOR = 1;      // 顔の色

    [SerializeField]
    private GameObject sotai;               // 素体モデル

    private GameObject sotaiBone;           // 素体のBone

    //[SerializeField]
    //private GameObject[] hairObjs;          // 対応する髪型オブジェクト
    [Header("体型(Vector3のScale)")]
    [SerializeField]
    private Vector3[] bodyScales;                  // 体型
        
    private MyCharData saveData;                 // マイキャラのセーブデータ
    //private MyCharData defaultData;              // マイキャラのデフォルト用データ

    [Header("初期状態のモデル")]
    [SerializeField]
    private GameObject defaultHair;              // 初期の髪型
    [SerializeField]
    private Material defaultHairColorMat;        // 初期の髪の色

    [SerializeField]
    private GameObject defaultEyeLine;           // 初期の目の形
    [SerializeField]
    private Material defaultEyePatternMat;       // 初期の目の模様

    [SerializeField]
    private GameObject defaultCloth;              // 初期の服

    [SerializeField]
    private BodyNum defaultBodyScale;            // 初期の体型
    [SerializeField]
    private Material[] defaultBodyColorMat;      // 初期の体の色(0:skin, 1:face)

    public void Awake()
    {
        // インスタンスが複数存在しないようにする
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);

        // 体型と髪型の登録番号を設定
        //bodyNum = BodyNum.NORMAL_BODY;

        sotaiBone = sotai.transform.Find("mixamorig:Hips").gameObject;

        saveData = new MyCharData();
        //defaultData = new MyCharData();

        saveData.hair = defaultHair;
        saveData.hairColor = defaultHairColorMat;
        saveData.eyeLine = defaultEyeLine;
        saveData.eyePattern = defaultEyePatternMat;
        saveData.cloth = defaultCloth;
        saveData.bodyScale = defaultBodyScale;
        saveData.bodyColor = defaultBodyColorMat;

    }
    
    // Update is called once per frame
    void Update()
    {
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 別シーンや作り直しする時に変更する部位の再設定
    // 　引　数   | mC：マイキャラの素体モデル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ReCreate(GameObject mC)
    {
        // 素体モデルが無い場合、再設定
        if (!sotai)
        {
            //sotai = GameObject.Find("skin");
            sotai = mC;
            sotaiBone = sotai.transform.Find("mixamorig:Hips").gameObject;
        }

        // 服を変える
        CharaCreateManager.ChangeClothObj(saveData.cloth, sotaiBone);

        // 目の形を変える
        //CharaCreateManager.ChangeEyeLine(saveData.eyeLine);

        // 髪型を変える
        //CharaCreateManager.ChangeHairObj(saveData.hair);

        // 髪の色を変える
        //CharaCreateManager.ChangeHairColor(saveData.hairColor);

        // 目の模様(＋色)を変える
        CharaCreateManager.ChangeEyePattern(saveData.eyePattern, sotai);

        // 体型を変える
        CharaCreateManager.ChangeBodyScale(saveData.bodyScale, sotai);

        // 体の色を変える
        CharaCreateManager.ChangeBodyColor(saveData.bodyColor, sotai);


        //// 髪の色を設定
        //ChangeHairColor(MyCharDataManager.Instance.HairColor);

        //// 体型を設定
        //ChangeBodyObj(MyCharDataManager.Instance.BodyNumber);

        //// 体の色を設定
        //ChangeBodyColor(MyCharDataManager.Instance.BodyColor[0], MyCharDataManager.Instance.BodyColor[1]);

        //// 目の形を設定
        //ChangeEyeLine(MyCharDataManager.Instance.EyeLine);

        //// 目の模様と色を設定
        //Material[] mat = MyCharDataManager.Instance.EyePattern;
        //ChangeEyePattern(mat[MyCharDataManager.LEFT_EYE], mat[MyCharDataManager.RIGHT_EYE]);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | デフォルトに戻す
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ResetDefault()
    {
        if (!sotai) sotai = GameObject.Find("skin");

        // 服を変える(既に同じものを選択していなければ)
        if (saveData.cloth.name != defaultCloth.name)
            CharaCreateManager.ChangeClothObj(defaultCloth, sotaiBone);

        // 目の形を変える(既に同じものを選択していなければ)
        if (saveData.eyeLine.name != defaultEyeLine.name)
            CharaCreateManager.ChangeEyeLine(defaultEyeLine);

        // 髪型を変える(既に同じものを選択していなければ)
        if (saveData.hair.name != defaultHair.name)
            CharaCreateManager.ChangeHairObj(defaultHair);

        // 髪の色を変える(既に同じものを選択していなければ)
        if (saveData.hairColor.name != defaultHairColorMat.name)
            CharaCreateManager.ChangeHairColor(defaultHairColorMat);

        // 目の模様(＋色)を変える(既に同じものを選択していなければ)
        if (saveData.eyePattern.name != defaultEyePatternMat.name)
            CharaCreateManager.ChangeEyePattern(defaultEyePatternMat, sotai);

        // 体型を変える(既に同じものを選択していなければ)
        if (saveData.bodyScale != defaultBodyScale)
            CharaCreateManager.ChangeBodyScale(defaultBodyScale, sotai);

        // 体の色を変える(既に同じものを選択していなければ)
        if (saveData.bodyColor[BODY_COLOR].name != defaultBodyColorMat[BODY_COLOR].name)
            CharaCreateManager.ChangeBodyColor(defaultBodyColorMat, sotai);

        // セーブデータに保存
        saveData.hair = defaultHair;
        saveData.hairColor = defaultHairColorMat;
        saveData.eyeLine = defaultEyeLine;
        saveData.eyePattern = defaultEyePatternMat;
        saveData.cloth = defaultCloth;
        saveData.bodyScale = defaultBodyScale;
        saveData.bodyColor = defaultBodyColorMat;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 服を変える
    // 　引　数   | newCloth：髪型, newColor：体の色の配列
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeClothObj(GameObject newCloth, Material[] newColor)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.cloth.name == newCloth.name) return;
        
        // 服を変え、セーブデータに保存
        CharaCreateManager.ChangeClothObj(newCloth, sotaiBone);
        saveData.cloth = newCloth;

        // 体の色を変え、セーブデータに保存
        CharaCreateManager.ChangeBodyColor(newColor, sotai);
        saveData.bodyColor = newColor;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪の色を変える
    // 　引　数   | newColor：髪の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeHairColor(Material newColor)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.hairColor.name == newColor.name) return;

        // 髪の色を変え、セーブデータに保存
        CharaCreateManager.ChangeHairColor(newColor);
        saveData.hairColor = newColor;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪型を変える
    // 　引　数   | newHair：髪型
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeHairObj(GameObject newHair)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.hair.name == newHair.name) return;

        // 髪型を変え、セーブデータに保存
        CharaCreateManager.ChangeHairObj(newHair);
        saveData.hair = newHair;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体型を変える
    // 　引　数   | newScale：体型の登録番号
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeBodyScale(BodyNum newScale)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.bodyScale == newScale) return;

        // 体型を変え、セーブデータに保存
        CharaCreateManager.ChangeBodyScale(newScale, sotai);
        saveData.bodyScale = newScale;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色を変える
    // 　引　数   | newColor：体の色の配列
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeBodyColor(Material[] newColor)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.bodyColor[BODY_COLOR].name == newColor[BODY_COLOR].name) return;
        // 体の色を変え、セーブデータに保存
        CharaCreateManager.ChangeBodyColor(newColor, sotai);
        saveData.bodyColor = newColor;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の模様を変える
    // 　引　数   | newPattern：目の模様のマテリアル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyePattern(Material newPattern)
    {
        Debug.Log(newPattern.name);
        // 既に同じものを選択していたら何もしない
        if (saveData.eyePattern.name == newPattern.name) return;

        // 目の模様を変え、セーブデータに保存
        CharaCreateManager.ChangeEyePattern(newPattern, sotai);
        saveData.eyePattern = newPattern;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の形を変える
    // 　引　数   | newLine：目の形
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyeLine(GameObject newLine)
    {        
        // 既に同じものを選択していたら何もしない
        if (saveData.eyeLine.name == newLine.name) return;

        // 目の形を変え、セーブデータに保存
        CharaCreateManager.ChangeEyeLine(newLine);
        saveData.eyeLine = newLine;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の色を変える
    // 　引　数   | newColor：目の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyeColor(Material newColor)
    {
        Debug.Log(newColor.name);
        // 既に同じものを選択していたら何もしない
        if (saveData.eyePattern.name == newColor.name) return;

        // 目の模様を変え、セーブデータに保存
        CharaCreateManager.ChangeEyeColor(newColor, sotai);
        saveData.eyePattern = newColor;
    }

    // 体型のアクセッサ
    public Vector3[] BodyScales
    {
        get { return bodyScales; }
    }

    // セーブデータのアクセッサ
    public MyCharData Data
    {
        get { return saveData; }
        set { saveData = value; }
    }
}
