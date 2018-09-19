using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCChangeButton : MonoBehaviour
{
    [SerializeField]
    private Material skinMat;          // 対応するマテリアル
    [SerializeField]
    private Material faceMat;          // 対応するマテリアル

    [SerializeField]
    private GameObject manager;     // キャラクリマネージャー

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 体の色を設定        
        manager.GetComponent<CharaCreateManager>().ChangeBodyColor(skinMat, faceMat);
    }
}
