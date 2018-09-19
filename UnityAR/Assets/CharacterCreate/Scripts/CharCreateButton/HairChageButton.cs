using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairChageButton : MonoBehaviour
{    
    [SerializeField]
    private HairNum hairNum;            // 髪型の登録番号
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
        // 髪型を設定
        manager.GetComponent<CharaCreateManager>().ChangeHairObj(hairNum);
    }
}
