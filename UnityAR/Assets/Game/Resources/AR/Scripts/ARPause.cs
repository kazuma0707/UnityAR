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

public class ARPause : MonoBehaviour {
    //=============  定数　===============//
    private const float WAIT_TIME = 3.0f;

    //  ゲームを待つための時間用変数
    private float waitGame = 0.0f;
    private bool startFlag = true;                                  //  スタート時かどうかのフラグ

    private ARGameManager managerScript;
    private GameObject imageTarget;                                 //  フラグ取得のためのimageTarget
    private Vuforia.GameTrackableEventHandler trackingEventHandle;  //  トラッキング関係のスクリプト

    // Use this for initialization
    void Start () {
        //  初期化処理
        managerScript = this.GetComponent<ARGameManager>();
        imageTarget = GameObject.Find("ImageTarget 1");
        trackingEventHandle = imageTarget.GetComponent<Vuforia.GameTrackableEventHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        if (startFlag == true && managerScript.tutorialCloseFlag == true)
        {
            //  カウントダウン処理(ゲーム開始時に行う処理)
            CountDown();
        }
    }

    //  カウントダウン処理(ゲーム開始時に行う処理)
    private void CountDown()
    {
        //  トラッキング成功時に処理を行う
        if (trackingEventHandle.GetTrackingFlag() == true)
        {
            //  Time.Scaleに依存しないタイムを取得
            waitGame += Time.unscaledDeltaTime;

            //  カウントダウンの実行
            managerScript.CountDown();

            //  カウントダウン後にゲームを動かす
            if (waitGame >= WAIT_TIME)
            {
                managerScript.GameStart();
                startFlag = false;
                //trackingEventHandle.trackingFlag = false;
            }
        }
    }
}
