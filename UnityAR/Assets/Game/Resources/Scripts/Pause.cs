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
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    //=============  定数　===============//
    private const float WAIT_TIME = 3.0f;

    //  ゲームを待つための時間用変数
    private float waitGame = 0.0f;
    private bool startFlag = true;             //  スタート時かどうかのフラグ

    private GameManager managerScript;
    bool isLodedScene = false;
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    // Use this for initialization
    void Start () {
        managerScript = this.GetComponent<GameManager>();
        

    }

	
	// Update is called once per frame
	void Update () {
        if (startFlag == true && managerScript.tutorialCloseFlag == true)
        {
            if (!isLodedScene) return;

            //  Time.Scaleに依存しないタイムを取得
            waitGame += Time.unscaledDeltaTime;

            //  カウントダウンの実行
            managerScript.CountDown();

            //  カウントダウン後にゲームを動かす
            if (waitGame >= WAIT_TIME)
            {
                managerScript.GameStart();
                startFlag = false;
            }
        }
    }
    float count = 0;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        while(true)
        {
            count += 0.1f;
            Debug.Log(count);
            if(count>5.0f)
            {
                
                isLodedScene = true;

                break;
            }
        }
    }
}
