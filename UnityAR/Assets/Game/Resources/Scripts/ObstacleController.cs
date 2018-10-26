using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  障害物の処理
public class ObstacleController : MonoBehaviour {

    // 障害物の落下速度
    private float speed = 0.1f;
    //  オブジェクトの消滅地点
    private float deletePos = -1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //  障害物の落下処理
        this.transform.Translate(0, -speed, 0);
        //  一定の座標以下なら障害物を消す
        if(this.transform.position.y <= deletePos)
        {
            Destroy(this.gameObject);
        }
	}
}
