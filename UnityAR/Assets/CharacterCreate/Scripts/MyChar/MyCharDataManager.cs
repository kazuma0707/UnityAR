using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ConstantName;

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
    CYBER
}

// 体の色の登録番号
public enum BodyColorNum
{
    NORMAL,
    BROWN,
    WHITE
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
    GREEN,
    RED
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
    GREEN,
    BLUE,
    RED
}

// 服の色の登録番号
public enum ClothColorNum
{
    PINK,
    YELLOW,
    GREEN,
    BLUE,
    RED
}

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
        public Material clothColor;     // 服の色
        public BodyNum bodyScale;       // 体型の番号
        public Material[] bodyColor;    // 体の色

        public ClothNum clothNum;       // 服の種類の番号
        public ClothColorNum ccn;       // 服の色の番号
        public BodyColorNum bcn;        // 体の色の番号

        public EyePatternNum eyePNum;   // 目の模様の番号
        public EyeColorNum ecn;         // 目の色の番号

        public HairNum hairNum;         // 髪型の番号
        public HairColorNum hcn;        // 髪の色の番号
    }

    // 定数宣言 /////////////////////////////////////////////////////////////////////////////
    public const int LEFT_EYE = 0;                                   // 左目
    public const int RIGHT_EYE = 1;                                  // 右目
    public const int BODY_COLOR = 0;                                 // 体の色
    public const int HEAD_COLOR = 1;                                 // 顔の色
    public const string SOTAI_MODEL = "skin";                        // 素体モデル
    public const string HIPS_BONE = "mixamorig:Hips";                // 素体のBone(Hips)
    public const string HAIR_BONE = "hiar";                          // 素体のBone(Hair)
    public const string LEFT_UP_LEG_BONE = "mixamorig:LeftUpLeg";    // 素体のBone(LeftUpLeg)
    public const string RIGHT_UP_LEG = "mixamorig:RightUpLeg";       // 素体のBone(RightUpLeg)
    public const string SPINE_BONE = "mixamorig:Spine";              // 素体のBone(Spine)

    /////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField]
    private GameObject sotai;               // 素体モデル

    private GameObject sotaiBone;           // 素体のBone
    private GameObject hairBaseBone;        // 素体の髪型の基点となるBone
    private Transform leftUpLeg;            // 素体のLeftUpLegBone
    private Transform rightUpLeg;           // 素体のRightUpLegBone
    private Transform spine;                // 素体のSpineUpLegBone

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
    private GameObject defaultCloth;             // 初期の服
    [SerializeField]
    private Material defaultClothColorMat;       // 初期の服の色

    [SerializeField]
    private BodyNum defaultBodyScale;            // 初期の体型
    [SerializeField]
    private Material[] defaultBodyColorMat;      // 初期の体の色(0:skin, 1:face)

    private bool sceneLoadOnce;                  // タイトルシーンが初回ロードかどうかのフラグ(true：2回目以降, false：初回)
   
    public void Awake()
    {
        // インスタンスが複数存在しないようにする
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);

        // 素体のBoneを取得
        sotaiBone = sotai.transform.Find(HIPS_BONE).gameObject;

        // 素体の髪型の基点となるBoneを取得
        hairBaseBone = sotai.transform.FindDeep(HAIR_BONE).gameObject;

        // DynamicBoneで除外したいオブジェクトを取得
        leftUpLeg = sotaiBone.transform.Find(LEFT_UP_LEG_BONE);
        rightUpLeg = sotaiBone.transform.Find(RIGHT_UP_LEG);
        spine = sotaiBone.transform.Find(SPINE_BONE);

        // セーブデータにデフォルト値を設定
        saveData = new MyCharData();

        saveData.hair = defaultHair;
        saveData.hairColor = defaultHairColorMat;
        saveData.eyeLine = defaultEyeLine;
        saveData.eyePattern = defaultEyePatternMat;
        saveData.cloth = defaultCloth;
        saveData.clothColor = defaultClothColorMat;
        saveData.bodyScale = defaultBodyScale;
        saveData.bodyColor = defaultBodyColorMat;

        CharaCreateManager.ChangeEyeLineObj(saveData.eyeLine, sotaiBone);

        ReCreate(sotai);

        sceneLoadOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 現在のシーンがタイトルシーンでなければタイトルシーン初回ロードのフラグを上げる
        if (SceneManager.GetActiveScene().name != SceneName.CharCreate)
            sceneLoadOnce = true;
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
            sotaiBone = sotai.transform.Find(HIPS_BONE).gameObject;
            hairBaseBone = sotai.transform.FindDeep(HAIR_BONE).gameObject;
            leftUpLeg = sotaiBone.transform.Find(LEFT_UP_LEG_BONE);
            rightUpLeg = sotaiBone.transform.Find(RIGHT_UP_LEG);
            spine = sotaiBone.transform.Find(SPINE_BONE);
        }

        // 何も変わっていなければ服を変える
        if (saveData.cloth.name != defaultCloth.name)
        {
            // DynamicBoneを除外
            RemoveDB(sotaiBone);
            // 服を変える
            CharaCreateManager.ChangeClothObj(saveData.cloth, sotaiBone);
            // DynamicBoneの設定
            StartCoroutine("SettingClothDB");
        }

        // 服の色を変える
        CharaCreateManager.ChangeClothColor(saveData.clothColor,sotai);

        // 何も変わっていなければ目の形を変える
        if (saveData.eyeLine.name != defaultEyeLine.name)
        {
            CharaCreateManager.ChangeEyeLineObj(saveData.eyeLine, sotaiBone);
        }
        // DynamicBoneを除外
        RemoveDB(hairBaseBone);
        // 髪型を変える
        CharaCreateManager.ChangeHairObj(saveData.hair, sotaiBone);
        // DynamicBoneの設定
        StartCoroutine("SettingHairDB");

        // 髪の色を変える
        CharaCreateManager.ChangeHairColor(saveData.hairColor, sotai);

        // 目の模様(＋色)を変える
        CharaCreateManager.ChangeEyePattern(saveData.eyePattern, sotai);

        // 体型を変える
        CharaCreateManager.ChangeBodyScale(saveData.bodyScale, sotai);

        // 体の色を変える
        CharaCreateManager.ChangeBodyColor(saveData.bodyColor, sotai);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | デフォルトに戻す
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ResetDefault()
    {
        if (!sotai) sotai = GameObject.Find(SOTAI_MODEL);

        // 服を変える(既に同じものを選択していなければ)
        if (saveData.cloth.name != defaultCloth.name)
        {
            // DynamicBoneを除外
            RemoveDB(sotaiBone);

            CharaCreateManager.ChangeClothObj(defaultCloth, sotaiBone);
            // DynamicBoneの設定
            StartCoroutine("SettingClothDB");
        }

        // 服の色を変える
        CharaCreateManager.ChangeClothColor(defaultClothColorMat, sotai);

        // 目の形を変える(既に同じものを選択していなければ)
        if (saveData.eyeLine.name != defaultEyeLine.name)
            CharaCreateManager.ChangeEyeLineObj(defaultEyeLine, sotaiBone);

        // 髪型を変える(既に同じものを選択していなければ)
        if (saveData.hair.name != defaultHair.name)
        {
            // DynamicBoneを除外
            RemoveDB(hairBaseBone);
            CharaCreateManager.ChangeHairObj(defaultHair, sotaiBone);
            // DynamicBoneの設定
            SettingHairDB();
        }
            

        // 髪の色を変える(既に同じものを選択していなければ)
        if (saveData.hairColor.name != defaultHairColorMat.name)
            CharaCreateManager.ChangeHairColor(defaultHairColorMat, sotai);

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
        saveData.clothColor = defaultClothColorMat;
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

        // DynamicBoneを除外
        RemoveDB(sotaiBone);
        
        // 服を変え、セーブデータに保存
        CharaCreateManager.ChangeClothObj(newCloth, sotaiBone);
        saveData.cloth = newCloth;

        // 体の色を変え、セーブデータに保存
        CharaCreateManager.ChangeBodyColor(newColor, sotai);
        saveData.bodyColor = newColor;

        // DynamicBoneの設定
        StartCoroutine("SettingClothDB");

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 服の色を変える
    // 　引　数   | newColor：目の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeClothColor(Material newColor)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.clothColor.name == newColor.name) return;

        // 服の色を変え、セーブデータに保存
        CharaCreateManager.ChangeClothColor(newColor, sotai);
        saveData.clothColor = newColor;
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
        CharaCreateManager.ChangeHairColor(newColor, sotai);
        saveData.hairColor = newColor;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪型を変える
    // 　引　数   | newHair：髪型, newColor：髪の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeHairObj(GameObject newHair, Material newColor)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.hair.name == newHair.name) return;

        // DynamicBoneを除外
        RemoveDB(hairBaseBone);

        // 髪型を変え、セーブデータに保存
        CharaCreateManager.ChangeHairObj(newHair, sotaiBone);
        saveData.hair = newHair;

        // 髪の色を変え、セーブデータに保存
        CharaCreateManager.ChangeHairColor(newColor, sotai);
        saveData.hairColor = newColor;

        // DynamicBoneの設定
        StartCoroutine("SettingHairDB");
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
        if (saveData.bodyColor[BODY_COLOR].name == newColor[BODY_COLOR].name &&
            saveData.bodyColor[HEAD_COLOR].name == newColor[HEAD_COLOR].name)
            return;
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
    public void ChangeEyeLineObj(GameObject newLine)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.eyeLine.name == newLine.name) return;

        // 目の形を変え、セーブデータに保存
        CharaCreateManager.ChangeEyeLineObj(newLine, sotaiBone);
        saveData.eyeLine = newLine;

        // 体の色を変える
        CharaCreateManager.ChangeBodyColor(saveData.bodyColor, sotai);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の色を変える
    // 　引　数   | newColor：目の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyeColor(Material newColor)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.eyePattern.name == newColor.name) return;

        // 目の模様を変え、セーブデータに保存
        CharaCreateManager.ChangeEyeColor(newColor, sotai);
        saveData.eyePattern = newColor;
    }
       
    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 服についているDynamicBoneを設定
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public IEnumerator SettingClothDB()
    {
        // 1フレーム置いてから処理をする
        yield return null;
        // 素体のBoneにDynamicBoneコンポーネントをアタッチ
        DynamicBone db = sotaiBone.AddComponent<DynamicBone>();
        
        // 素体のBoneをルートとして設定
        db.m_Root = sotaiBone.transform;

        // 円の当たり判定の半径を設定
        db.m_Radius = 0.05f;

        // 除外したいオブジェクトを除外リストに追加
        db.m_Exclusions = new List<Transform>();        
        db.m_Exclusions.Add(leftUpLeg);
        db.m_Exclusions.Add(rightUpLeg);
        db.m_Exclusions.Add(spine);

        // コライダーが付いているBoneオブジェクトを取得
        DynamicBoneCollider[] DBCs = sotaiBone.GetComponentsInChildren<DynamicBoneCollider>();

        // コライダーが付いているBoneオブジェクトをコライダーリストに追加
        db.m_Colliders = new List<DynamicBoneColliderBase>();
        db.m_Colliders.AddRange(DBCs);     

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪型についているDynamicBoneを設定
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public IEnumerator SettingHairDB()
    {
        // 1フレーム置いてから処理をする
        yield return null;
        // 髪型の基点となるBoneにDynamicBoneコンポーネントをアタッチ
        DynamicBone db = hairBaseBone.AddComponent<DynamicBone>();

        // 髪型の基点となるBoneをルートとして設定
        db.m_Root = hairBaseBone.transform;

        // Boneの減速の値を設定
        db.m_Damping = 0.2f;

        // 円の当たり判定の半径を設定
        db.m_Radius = 0.05f;

        // 除外したいオブジェクトを除外リストに追加
        db.m_Exclusions = new List<Transform>();
        
        // コライダーが付いているBoneオブジェクトを取得
        DynamicBoneCollider[] DBCs = sotaiBone.GetComponentsInChildren<DynamicBoneCollider>();

        // コライダーが付いているBoneオブジェクトをコライダーリストに追加
        db.m_Colliders = new List<DynamicBoneColliderBase>();
        db.m_Colliders.AddRange(DBCs);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | DynamicBoneを除外
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void RemoveDB(GameObject bone)
    {
        // 素体のBoneにDynamicBoneコンポーネントをアタッチ
        bone.RemoveComponent<DynamicBone>(); 
        //sotaiBone.GetComponent<DynamicBone>().enabled = false;       
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体型を変える(ミニゲーム用)
    // 　引　数   | newScale：体型の登録番号
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeBodyScaleInGame(BodyNum newScale)
    {
        // 既に同じものを選択していたら何もしない
        if (saveData.bodyScale == newScale) return;

        // 体型を変え、セーブデータに保存
        CharaCreateManager.ChangeBodyScale(newScale, sotai);
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

    // タイトルシーン初回ロードのフラグのアクセッサ
    public bool SceneLoadOnce
    {
        get { return sceneLoadOnce; }
    }
}
