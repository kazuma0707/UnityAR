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
    EYE_PATTERN_R,
    MATSUGE,
    MAYU
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

    public const int left = 0;      // 左
    public const int right = 1;     // 右

    [SerializeField]
    private GameObject myChar;              // マイキャラオブジェクト

    [SerializeField]
    private GameObject[] hairObjs;          // 対応する髪型オブジェクト

    [SerializeField]
    private Vector3[] bodyScale;            // 体型

    [SerializeField]
    private Material eyeLineMat;            // 目の形
    [SerializeField]
    private Material[] eyePatternMat;         // 目の模様

    private Material hairColor;                // 髪の色
    private Color eyeColor;                 // 目の色
    private Material[] bodyColor;                // 体の色

    
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
    }
    
    // Update is called once per frame
    void Update()
    {

    }  

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | マイキャラ生成
    // 　引　数   | mC：マイキャラオブジェクト
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void CreateMyChar(GameObject mC)
    {
        // 体型を設定
        mC.transform.transform.localScale = bodyScale[(int)bodyNum];

        // 体の色を設定
        GameObject[] bodies = GameObject.FindGameObjectsWithTag("BodyObj");
        foreach (GameObject obs in bodies)
        {
            switch (obs.name)
            {
                case "polySurface4":
                case "transform6":
                default:
                    obs.GetComponent<Renderer>().material = bodyColor[0];
                    break;
                case "transform22":
                    obs.GetComponent<Renderer>().material = bodyColor[1];
                    break;                    
            }            
        }

        // 髪型を設定   
        // 子オブジェクトを検索
        foreach (var child in mC.GetChildren())
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
                GameObject hair = Instantiate(hairObjs[(int)bodyNum], child.transform.position, Quaternion.identity);

                hair.transform.SetParent(child.transform.parent.transform);
                // 髪の色を設定
                hair.GetComponent<Renderer>().material = hairColor;
            }
        }

        // 髪の色を設定
        //GameObject[] hairs = GameObject.FindGameObjectsWithTag("HairObj");
        //foreach (GameObject obs in hairs)
        //{
        //    obs.GetComponent<Renderer>().material.color = hairColor;
        //}

        // 目の形を設定
        GameObject[] eyeLines = GameObject.FindGameObjectsWithTag("eyeLineObj");
        foreach (GameObject obs in eyeLines)
        {
            obs.GetComponent<SkinnedMeshRenderer>().material = eyeLineMat;
        }

        // 目の模様と色を設定
        GameObject[] eyePatterns = GameObject.FindGameObjectsWithTag("eyePatternObj");

        eyePatterns[0].GetComponent<MeshRenderer>().material = eyePatternMat[1];
        eyePatterns[1].GetComponent<MeshRenderer>().material = eyePatternMat[0];
        for (int i = 0; i < eyePatterns.Length; i++)
        {
            eyePatterns[i].GetComponent<MeshRenderer>().material.color = eyeColor;
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
    public Material[] EyePattern
    {
        get { return eyePatternMat; }
        set { eyePatternMat = value; }
    }

    // 髪の色のアクセッサ
    public Material HairColor
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

    // 体の色のアクセッサ
    public Material[] BodyColor
    {
        get { return bodyColor; }
        set { bodyColor = value; }
    }

    // 体型のアクセッサ
    public Vector3[] BodySize
    {
        get { return bodyScale; }
    }

    // 髪型のアクセッサ
    public GameObject[] HairObj
    {
        get { return hairObjs; }
    }
}
