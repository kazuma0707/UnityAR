//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   PauseManager.cs
//!
//! @brief  ポーズするオブジェクトを管理する
//!
//! @date   2018/11/14
//!
//! @author 加藤　竜哉
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    //  シーン内でポーズを行うオブジェクト
    //[SerializeField]
    private GameObject[] floorObj;          //  床オブジェクト
    [SerializeField]
    private GameObject player;              // プレイヤー

    // Use this for initialization
    void Start () {
        floorObj = GameObject.FindGameObjectsWithTag("Floor");
    }
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<GameManager>().pauseFlag)
        {
            floorObj = GameObject.FindGameObjectsWithTag("Floor");
            player.GetComponent<UnityChanControlScriptWithRgidBody>().enabled = false;
            for (int i = 1; i < floorObj.Length; i++)
            {
                floorObj[i].GetComponent<SlidingFloor>().enabled = false;
            }
            this.GetComponent<GameManager>().enabled = false;
        }
        else
        {
            player.GetComponent<UnityChanControlScriptWithRgidBody>().enabled = true;
            for (int i = 1; i < floorObj.Length; i++)
            {
                if (floorObj[i] != null)
                {
                    floorObj[i].GetComponent<SlidingFloor>().enabled = true;
                }
            }
            this.GetComponent<GameManager>().enabled = true;
        }
    }
}
