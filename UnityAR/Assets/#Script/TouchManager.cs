using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchManager : MonoBehaviour {
    public GameObject skin;
    public GameObject hitObject { get; private set; }
    TouchInfo touch;
    //回転用
    Vector2 sPos;   //タッチした座標
    Quaternion sRot;//タッチしたときの回転
    float wid, hei, diag;  //スクリーンサイズ
    float tx, ty;    //変数

    //ピンチイン ピンチアウト用
    float vMin = 0.5f, vMax = 2.0f;  //倍率制限
    float sDist = 0.0f, nDist = 0.0f; //距離変数
    Vector3 initScale; //最初の大きさ
    float v = 1.0f; //現在倍率

    // Use this for initialization
    void Start () {
        wid = Screen.width;
        hei = Screen.height;
        diag = Mathf.Sqrt(Mathf.Pow(wid, 2) + Mathf.Pow(hei, 2));
        initScale = skin.transform.localScale;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //タッチ情報の取得
        for(int i=0;i<Input.touchCount;i++)
        {
            touch = AppUtil.GetTouch();
        }
        this.TouchPinchEvent();
        //タッチしたときかどうか
        if (touch == TouchInfo.Began)
        {
            this.TouchRayEvent();
        
        }
		
	}

    private void TouchRayEvent()
    {
        //タッチした位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(AppUtil.GetTouchPosition());
        RaycastHit rayhit = new RaycastHit();
        if (Physics.Raycast(ray, out rayhit))
        {
            hitObject = rayhit.collider.gameObject;
        }
    }
    private void TouchPinchEvent()
    {
       
        if (Input.touchCount == 0)
        {
            //回転
            //TouchInfo info1 = AppUtil.GetTouch();
            //Touch t1 = Input.GetTouch(0);
            
            //if(info1==TouchInfo.Began)
            //{
            //    sPos = AppUtil.GetTouchPosition();
            //    sRot = skin.transform.rotation;
            //}
            //else if(info1==TouchInfo.Moved||info1==TouchInfo.Stationary)
            //{
            //    tx = (AppUtil.GetTouchPosition().x - sPos.x) / wid; //横移動量(-1<tx<1)
            //    ty = (AppUtil.GetTouchPosition().y - sPos.y) / hei; //縦移動量(-1<ty<1)
            //    skin.transform.rotation = sRot;
            //    skin.transform.Rotate(new Vector3(90 * tx, 0, 0), Space.World);
            //    debugText.text = skin.transform.rotation.ToString();
            //}
        }
        else if (Input.touchCount >= 2)
        {
            //ピンチイン ピンチアウト
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);
            if (t2.phase == TouchPhase.Began)
            {
                sDist = Vector2.Distance(t1.position, t2.position);
            }
            else if ((t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary) &&
                     (t2.phase == TouchPhase.Moved || t2.phase == TouchPhase.Stationary))
            {
                nDist = Vector2.Distance(t1.position, t2.position);
                v = v + (nDist - sDist) / diag;
                sDist = nDist;
                if (v > vMax) v = vMax;
                if (v < vMin) v = vMin;
                skin.transform.localScale = initScale * v;
            }
        }

    }
}
