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

// 髪型の登録番号
public enum HairNum
{
    SHORT_HAIR,
    LONG_HAIR,
    TWIN_TAILS,
    PONY_TAIL,
    IF
}

// 変更する部の登録番号
public enum ChangingPoint
{
    BODY,
    SKIN,
    EYE_DEF,
    HEAD,
    HAIR,
    EYE_LINE,
    EYE_PATTERN_L,
    EYE_PATTERN_R
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

    [SerializeField]
    private GameObject myChar;              // マイキャラオブジェクト

    [SerializeField]
    private GameObject[] changingPoints;     // 変更する部位
    [SerializeField]
    private GameObject[] changingHairPoints;    // 変更する髪の部位
    [SerializeField]
    private Vector3[] bodyScale;            // 体型

    [SerializeField]
    private GameObject[] hairObjs;          // 対応する髪型オブジェクト

    [SerializeField]
    private Material eyeLineMat;            // 目の形
    [SerializeField]
    private Material eyePatternMat;         // 目の模様

    [SerializeField]
    private Material FirstHairColorMat;     // 初期の髪の色
    [SerializeField]
    private Material FirstEyeColorMat;      // 初期の目の色
    [SerializeField]
    private Material FirstBodyColorMat;     // 初期の体の色

    private Color hairColor;                // 髪の色
    private Color eyeColor;                 // 目の色
    private Color bodyColor;                // 体の色

    
    private BodyNum bodyNum;                // 体型の登録番号
    private HairNum hairNum;                // 髪型の登録番号

    public void Awake()
    {
        // インスタンスが複数存在しないようにする
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);

        // // 体型と髪型の登録番号を設定
        bodyNum = BodyNum.NORMAL_BODY;
        hairNum = HairNum.SHORT_HAIR;

        // 各色の初期設定
        hairColor = FirstHairColorMat.color;
        eyeColor = FirstEyeColorMat.color;
        bodyColor = FirstBodyColorMat.color;
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪型を変える
    // 　引　数   | num：髪型の登録番号
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeHairObj(HairNum num)
    {
        // 子オブジェクトを検索
        foreach (var child in myChar.GetChildren())
        {
            // 既存の髪オブジェクトを削除
            if (child.tag == "HairObj")
            {
                Destroy(child);
                continue;
            }

            // 定位置に髪オブジェクトを作る
            if (child.name == "HairPos")
            {
                GameObject hair = Instantiate(hairObjs[(int)num], child.transform.position, Quaternion.identity);
                
                hair.transform.SetParent(child.transform.parent.transform);
                // 髪の色を設定
                hair.GetComponent<MeshRenderer>().material.color = hairColor;
                // 髪型の登録番号を設定
                hairNum = num;
            }
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪の色を変える
    // 　引　数   | color：髪の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeHairColor(Color color)
    {
        // マテリアルと色を設定
        for (int i = 0; i < changingHairPoints.Length; i++)
        {
            changingHairPoints[i].GetComponent<SkinnedMeshRenderer>().material.color = color;
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体型を変える
    // 　引　数   | num：体型の登録番号
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeBodyObj(BodyNum num)
    {       
        // myCharの一を保存
        Vector3 pos = myChar.transform.position;
        // 体型を設定
        changingPoints[(int)ChangingPoint.BODY].transform.localScale = bodyScale[(int)num];
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色を変える
    // 　引　数   | color：体の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeBodyColor(Color color)
    {
        // 色を設定
        changingPoints[(int)ChangingPoint.SKIN].GetComponent<SkinnedMeshRenderer>().material.color = color;
        changingPoints[(int)ChangingPoint.EYE_DEF].GetComponent<SkinnedMeshRenderer>().material.color = color;
        changingPoints[(int)ChangingPoint.HEAD].GetComponent<SkinnedMeshRenderer>().material.color = color;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の模様を変える
    // 　引　数   | mat：目の模様のマテリアル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyePattern(Material mat)
    {
        // マテリアルと色を設定
        changingPoints[(int)ChangingPoint.EYE_PATTERN_L].GetComponent<MeshRenderer>().material = mat;
        changingPoints[(int)ChangingPoint.EYE_PATTERN_R].GetComponent<MeshRenderer>().material = mat;
        ChangeEyeColor(eyeColor);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の形を変える
    // 　引　数   | mat：目の形のマテリアル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyeLine(Material mat)
    {
        // マテリアルを設定
        changingPoints[(int)ChangingPoint.EYE_LINE].GetComponent<SkinnedMeshRenderer>().material = mat;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の色を変える
    // 　引　数   | color：目の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyeColor(Color color)
    {
        // マテリアルを設定
        changingPoints[(int)ChangingPoint.EYE_PATTERN_L].GetComponent<MeshRenderer>().material.color = color;
        changingPoints[(int)ChangingPoint.EYE_PATTERN_R].GetComponent<MeshRenderer>().material.color = color;
    }    

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | マイキャラ生成
    // 　引　数   | myChar：マイキャラオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void CreateMyChar(GameObject myChar)
    {
        // 体型を設定
        myChar.transform.transform.localScale = bodyScale[(int)bodyNum];

        // 体の色を設定
        GameObject[] bodies = GameObject.FindGameObjectsWithTag("BodyObj");
        foreach (GameObject obs in bodies)
        {
            obs.GetComponent<SkinnedMeshRenderer>().material.color = bodyColor;
        }        

        // 髪型を設定       

        // 髪の色を設定
        GameObject[] hairs = GameObject.FindGameObjectsWithTag("HairObj");
        foreach (GameObject obs in hairs)
        {
            obs.GetComponent<SkinnedMeshRenderer>().material.color = hairColor;
        }

        // 目の形を設定
        GameObject[] eyeLines = GameObject.FindGameObjectsWithTag("eyeLineObj");
        foreach (GameObject obs in eyeLines)
        {
            obs.GetComponent<SkinnedMeshRenderer>().material = eyeLineMat;
        }

        // 目の模様と色を設定
        GameObject[] eyePatterns = GameObject.FindGameObjectsWithTag("eyePatternObj");
        foreach (GameObject obs in eyePatterns)
        {
            obs.GetComponent<MeshRenderer>().material = eyePatternMat;
            obs.GetComponent<MeshRenderer>().material.color = eyeColor;
        }
    }

    // 体型の登録番号のアクセッサ
    public BodyNum BodyNumber
    {
        get { return bodyNum; }
        set { bodyNum = value; }
    }

    // 髪型の登録番号のアクセッサ
    public HairNum HairNumber
    {
        get { return hairNum; }
        set { hairNum = value; }
    }

    // 目の形のアクセッサ
    public Material EyeLine
    {
        get { return eyeLineMat; }
        set { eyeLineMat = value; }
    }

    // 目の模様のアクセッサ
    public Material EyePattern
    {
        get { return eyePatternMat; }
        set { eyePatternMat = value; }
    }

    // 髪の色のアクセッサ
    public Color HairColor
    {
        get { return hairColor; }
        set { hairColor = value; }
    }

    // 目の色のアクセッサ
    public Color EyeColor
    {
        get { return eyeColor; }
        set { eyeColor = value; }
    }

    // 体の形のアクセッサ
    public Color BodyColor
    {
        get { return bodyColor; }
        set { bodyColor = value; }
    }

}
