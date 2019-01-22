//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   DeathEffect.cs
//!
//! @brief  死亡時の演出を行う
//!
//! @date   2018/11/14
//!
//! @author 加藤　竜哉
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDeathEffect : MonoBehaviour {

    //=============  定数　===============//
    private const float DEAD_LINE = 3.0f;       //  シーン変更の判定ライン
    private const float FALL_SPEED = 3.0f;      //  落下時のスピード

    [SerializeField]
    private GameObject player;                  //  プレイヤー
    Animator anim;                              //  プレイヤーのアニメーター
    float fall = 0.0f;                          //  落下時使用変数
    bool isLoad = false;                        //  ロードが多重に行われないようにするフラグ

    // Use this for initialization
    void Start () {
        //  アニメーターの取得
        anim = player.GetComponent<Animator>();	    
	}
	
	// Update is called once per frame
	void Update () {

        //  時間が止まっていたら
		if(Time.timeScale <= 0.0f)
        {
            //  死んでいなければ処理は行わない
            if (player.GetComponent<ARUnityChanControlScriptWithRgidBody>().GetDeadFlag() == false) return;

            //  死亡時のアニメーションを再生
            anim.SetBool("Dead", true);

            //  プレイヤーを落下させる処理
            Vector3 pos = player.transform.localPosition;
            fall = pos.y;
            fall -= Time.unscaledDeltaTime * FALL_SPEED;
            pos.y = fall;
            //player.transform.Translate(pos);
            player.transform.position = pos;

            //  プレイヤーが一定以下なら
            if (player.transform.position.y <= -DEAD_LINE)
            {
                //  シーンが複数ロードされるのの防止
                if (!isLoad)
                {
                    FadeManager.Instance.LoadScene("Ranking", 2.0f);
                }
                isLoad = true;
            }
        }

	}
}
