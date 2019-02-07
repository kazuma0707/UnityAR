using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveto : MonoBehaviour {
    public GameObject skin;
    public bool isOnceFlag ;
    float Animtime = 0.0f;//Walkの再生時間
    const float EndPos = 1.0f;//再生終了時間
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (isOnceFlag)
        {
            //移動
            iTween.MoveTo(this.skin, iTween.Hash("z", 1.0f, "time", 3.0f, "EaseType", iTween.EaseType.easeInOutQuart));

            //Animtimeが3秒以上だった場合
            if (skin.transform.position.z != EndPos)
            {
                //歩くアニメーション
                skin.GetComponent<Animator>().SetBool("Walk", true);
         
            }
            else
            {
                skin.GetComponent<Animator>().SetBool("Walk", false);
                isOnceFlag = false;
            }
        }
    }



}
