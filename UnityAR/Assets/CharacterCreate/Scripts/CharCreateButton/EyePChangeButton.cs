using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePChangeButton : MonoBehaviour
{      
    [SerializeField]
    private Material[] mat;          // 対応するマテリアル
    [SerializeField]
    private GameObject manager;     // キャラクリマネージャー

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 目の模様を設定        
        manager.GetComponent<CharaCreateManager>().ChangeEyePattern(mat[MyCharDataManager.LEFT_EYE], mat[MyCharDataManager.RIGHT_EYE]);
    }
}
