﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChangeButton : MonoBehaviour
{
    [SerializeField]
    private GameObject resourceObject;            // 差し替えるモデル
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
        manager.GetComponent<ChangeBone>().ChangeClothes(resourceObject);
    }
}
