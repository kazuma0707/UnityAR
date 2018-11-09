//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   Pause.cs
//!
//! @brief  スタート時カウントダウン用スクリプト
//!
//! @date   2018/11/07
//!
//! @author 加藤　竜哉
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
    //=============  定数　===============//
    private const float WAIT_TIME = 3.0f;
    //  ゲームを待つための時間用変数
    private float waitGame = 0.0f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //  Time.Scaleに依存しないタイムを取得
        waitGame += Time.unscaledDeltaTime;

        //  カウントダウンの実行
        this.GetComponent<GameManager>().CountDown();

        //  カウントダウン後にゲームを動かす
        if (waitGame >= WAIT_TIME)
        {
            this.GetComponent<GameManager>().GameStart();
        }
    }
}
