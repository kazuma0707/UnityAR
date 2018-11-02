using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCreateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject myChar;                  // マイキャラオブジェクト

    [Header("変更する部位の登録")]
    [SerializeField]
    private GameObject[] changingPoints;        // 変更する部位

    [Header("変更する髪の部位の登録")]
    [SerializeField]
    private GameObject[] changingHairPoints;    // 変更する髪の部位

    [Header("初期状態のモデル")]
    [SerializeField]
    private BodyNum defaultBodyScale;         // 初期の体型
    [SerializeField]
    private HairNum defaultHair;              // 初期の髪型
    [SerializeField]
    private Material defaultEyeLineMat;       // 初期の目の形
    [SerializeField]
    private Material[] defaultEyePatternMat;   // 初期の目の模様
    [SerializeField]
    private Material defaultHairColorMat;     // 初期の髪の色
    [SerializeField]
    private Material defaultEyeColorMat;      // 初期の目の色
    [SerializeField]
    private Material[] defaultBodyColorMat;     // 初期の体の色(0:skin, 1:face)

    private GameObject[] hairObjs;              // 髪型

    private Vector3[] bodyScale;                // 体型
    
    // Use this for initialization
    void Start ()
    {
        bodyScale = MyCharDataManager.Instance.BodySize;
        hairObjs = MyCharDataManager.Instance.HairObj;

        // 各色の初期設定
        MyCharDataManager.Instance.HairColor = defaultHairColorMat;
        MyCharDataManager.Instance.EyeColor = defaultEyeColorMat.color;
        MyCharDataManager.Instance.EyePattern = defaultEyePatternMat;

        //MyCharDataManager.Instance.BodyColor[0] = defaultBodyColorMat[0];
        //MyCharDataManager.Instance.BodyColor[1] = defaultBodyColorMat[1];

        MyCharDataManager.Instance.BodyColor = defaultBodyColorMat;

        Remake();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 作り直しする時に変更する部位の再設定
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void Remake()
    {
        // 髪の色を設定
        ChangeHairColor(MyCharDataManager.Instance.HairColor);

        // 体型を設定
        ChangeBodyObj(MyCharDataManager.Instance.BodyNumber);

        // 体の色を設定
        ChangeBodyColor(MyCharDataManager.Instance.BodyColor[0], MyCharDataManager.Instance.BodyColor[1]);

        // 目の形を設定
        ChangeEyeLine(MyCharDataManager.Instance.EyeLine);

        // 目の模様と色を設定
        Material[] mat = MyCharDataManager.Instance.EyePattern;
        ChangeEyePattern(mat[MyCharDataManager.LEFT_EYE], mat[MyCharDataManager.RIGHT_EYE]);        
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | デフォルトに戻す
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ResetDefault()
    {
        if (!myChar) myChar = GameObject.Find("skin");

        // 体型を設定
        ChangeBodyObj(BodyNum.NORMAL_BODY);

        // 体の色を設定
        ChangeBodyColor(defaultBodyColorMat[0], defaultBodyColorMat[1]);

        // 髪型を設定       
        ChangeHairObj(defaultHair);

        // 髪の色を設定
        GameObject[] hairs = GameObject.FindGameObjectsWithTag("HairObj");
        foreach (GameObject obs in hairs)
        {
            obs.GetComponent<Renderer>().material = defaultHairColorMat;
        }
        MyCharDataManager.Instance.HairColor = defaultHairColorMat;

        // 目の形を設定
        ChangeEyeLine(defaultEyeLineMat);

        // 目の模様と色を設定
        ChangeEyePattern(defaultEyePatternMat[0], defaultEyePatternMat[1]);
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪型を変える
    // 　引　数   | num：髪型の登録番号
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeHairObj(HairNum num)
    {
        Transform parent = null;
        // 子オブジェクトを検索
        foreach (var child in myChar.GetChildren())
        {
            // 既存の髪オブジェクトを削除
            if (child.tag == "HairObj")
            {
                // メッシュ切り替え
                //child.GetComponent<SkinnedMeshRenderer>().sharedMesh = hairMesh;

                parent = child.transform.parent;
                Destroy(child);
                continue;
            }

            // 定位置に髪オブジェクトを作る
            if (child.name == "HairPos")
            {
                GameObject hair = Instantiate(hairObjs[(int)num], child.transform.position, child.transform.rotation) as GameObject;

                if (parent)
                {
                    hair.transform.SetParent(parent);
                }
                // 髪の色を設定
                hair.GetComponent<Renderer>().material = MyCharDataManager.Instance.HairColor;
                // 髪型の登録番号を設定
                MyCharDataManager.Instance.HairNumber = num;
                break;
            }
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 髪の色を変える
    // 　引　数   | color：髪の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeHairColor(Material color)
    {        
        // マテリアルと色を設定
        for (int i = 0; i < changingHairPoints.Length; i++)
        {
            // デバッグ用 ///////////////////////////////////////////////////////////////////////////////////////
            if (!changingHairPoints[i])
            {
                // 子オブジェクトを検索
                foreach (var child in myChar.GetChildren())
                {
                    // 既存の髪オブジェクトを削除
                    if (child.tag == "HairObj")
                    {
                        //child.GetComponent<Renderer>().material.color = color;
                        child.GetComponent<Renderer>().material = color;
                    }
                }
                break;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            //changingHairPoints[i].GetComponent<Renderer>().material.color = color;
            changingHairPoints[i].GetComponent<Renderer>().material = color;
        }
        MyCharDataManager.Instance.HairColor = color;
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
        changingPoints[(int)ChangingPoint.SKIN].transform.localScale = bodyScale[(int)num];
        MyCharDataManager.Instance.BodyNumber = num;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 体の色を変える
    // 　引　数   | color：体の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeBodyColor(Material skin, Material face)
    {
        // 色を設定
        //changingPoints[(int)ChangingPoint.SKIN].GetComponent<Renderer>().material.color = color;
        //changingPoints[(int)ChangingPoint.EYE_DEF].GetComponent<Renderer>().material.color = color;
        //changingPoints[(int)ChangingPoint.HEAD].GetComponent<Renderer>().material.color = color;
        //MyCharDataManager.Instance.BodyColor = color;
        changingPoints[(int)ChangingPoint.BODY].GetComponent<Renderer>().material = skin;
        //changingPoints[(int)ChangingPoint.MAYU].GetComponent<Renderer>().material = skin;
        changingPoints[(int)ChangingPoint.HEAD].GetComponent<Renderer>().material = face;
        //changingPoints[(int)ChangingPoint.MATSUGE].GetComponent<Renderer>().material = face;

        MyCharDataManager.Instance.BodyColor[0] = skin;
        MyCharDataManager.Instance.BodyColor[1] = face;        
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の模様を変える
    // 　引　数   | mat：目の模様のマテリアル
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyePattern(Material matL, Material matR)
    {
        // マテリアルと色を設定
        changingPoints[(int)ChangingPoint.EYE_PATTERN].GetComponent<Renderer>().material = matL;
        MyCharDataManager.Instance.EyePattern[0] = matL;
        MyCharDataManager.Instance.EyePattern[1] = matR;
        ChangeEyeColor(MyCharDataManager.Instance.EyeColor);
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
        MyCharDataManager.Instance.EyeLine = mat;
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 目の色を変える
    // 　引　数   | color：目の色
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ChangeEyeColor(Color color)
    {
        // マテリアルを設定
        changingPoints[(int)ChangingPoint.EYE_PATTERN].GetComponent<Renderer>().material.color = color;
        MyCharDataManager.Instance.EyeColor = color;
    }

    public GameObject[] ChangingPoints
    {
        get { return changingPoints; }
        set { changingPoints = value; }
    }

}
