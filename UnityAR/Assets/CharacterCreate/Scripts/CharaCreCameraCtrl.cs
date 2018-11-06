using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharaCreCameraCtrl : MonoBehaviour
{
    private const float ANGLE_LIMIT_MAX = 60.0f;         // 角度の最大値
    private const float ANGLE_LIMIT_MIN = -20.0f;        // 角度の最小値
    private const float ZOOM_LIMIT_MAX = 60.0f;          // 拡大の最大値
    private const float ZOOM_LIMIT_MIN = 2.0f;           // 拡大の最小値
    private const float TOUCH_POS_LIMIT_MAX = 525.0f;    // タッチポジションの最大値
    private const float TOUCH_POS_LIMIT_MIN = 300.0f;    // タッチポジションの最小値
    private const float MOVE_LIMIT_MAX = 5.0f;           // マウス(タッチ)の移動した値の最小値
    private const float MOVE_LIMIT_MIN = -5.0f;          // マウス(タッチ)の移動した値の最小値

    [SerializeField]
    private Transform targetObj;                               // 注視するオブジェクト
    [SerializeField]
    private GameObject[] charaCreButtons;                      // キャラクリするボタン
    [SerializeField]
    private float rotateSpeed = 5.0f;                          // 回転する速度
    [SerializeField]
    private float translateSpeed = 0.5f;                       // 移動する速度
    [SerializeField]
    private float zoomSpeed = 1.0f;                            // 拡大する速度 
    private float touchPosLimit = TOUCH_POS_LIMIT_MIN;         // 拡大する速度    
    private Vector3 targetPoint;                               // 注視点
    private bool moveFlag = false;                             // グリグリ動かせるかのフラグ
    private Camera cam;                                        // カメラコンポーネント

    [SerializeField]
    private Text text;                                         // デバッグ用テキスト

   // Use this for initialization
   void Start ()
    {
        cam = GetComponent<Camera>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        // グリグリ動かせない状態であれば何もしない
        if (!moveFlag) return;       

        targetPoint = targetObj.transform.position;

        // キャラクリ用のボタンを検索
        for (int i = 0; i < charaCreButtons.Length; i++)
        {
            // アクティブであればタッチポジションの最大値を設定
            if (charaCreButtons[i].activeInHierarchy)
            {
                touchPosLimit = TOUCH_POS_LIMIT_MAX;
                break;
            }
            // アクティブでなければタッチポジションの最小値を設定
            touchPosLimit = TOUCH_POS_LIMIT_MIN;
        }
        
#if UNITY_EDITOR

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseWheelScroll = Input.GetAxis("Mouse ScrollWheel");

        // 画面サイズの41%の値を取得
        float posY = (float)Screen.height * 0.41f;

        // 平行移動(ホイール押下でドラッグ)
        if (Input.GetMouseButton(2))
        {            
            // マウスポジションが一定値以下であればカメラを動かさない
            if (Input.mousePosition.y < posY) return;
            this.transform.Translate(mouseX * translateSpeed, mouseY * translateSpeed, 0);
        }

        // ズーム(ホイール回転)
        if (mouseWheelScroll != 0) ZoomCamera(mouseWheelScroll);

        // 注視点の周りを回る(左クリック＋ドラッグ)
        if (Input.GetMouseButton(0))
        {
            // マウスポジションが一定値以下であればカメラを動かさない
            if (Input.mousePosition.y < posY) return;
            RotateCamera(mouseX, mouseY);
        }
#else
        int touchCount = Input.touches.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
        
        text.text = "Y:" + Input.GetTouch(0).position.y.ToString() + ", T:" + touchPosLimit.ToString();
        //text.text = "W:" + Screen.width + ", H:" + Screen.height;
        if (Input.touchCount == 1)
        {
            // タッチポジションが一定値以下であればカメラを動かさない
            if (Input.GetTouch(0).position.y < touchPosLimit) return;

            Touch t = Input.touches.First();

            // カメラ回転
            float xDelta = t.deltaPosition.x * rotateSpeed;
            float yDelta = t.deltaPosition.y * rotateSpeed;
            RotateCamera(xDelta, yDelta);
        }
        else if (Input.touchCount == 2)
        {
            // タッチポジションが一定値以下であればカメラを動かさない
            if (Input.GetTouch(0).position.y < touchPosLimit) return;

            // カメラ移動
            Touch t = Input.touches.First();
            
            float xDelta = t.deltaPosition.x * translateSpeed;
            float yDelta = t.deltaPosition.y * translateSpeed;
            xDelta = Mathf.Clamp(xDelta, -10.0f, 10.0f);
            yDelta = Mathf.Clamp(yDelta, -10.0f, 10.0f);
            this.transform.Translate(xDelta, yDelta, 0);

            // ZOOM
            // 両方のタッチを格納します
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 各タッチの前フレームでの位置をもとめます
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 各フレームのタッチ間のベクター (距離) の大きさをもとめます
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 各フレーム間の距離の差をもとめます
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            ZoomCamera(-deltaMagnitudeDiff);
        }        
#endif
    }

    // カメラの回転
    private void RotateCamera(float x, float y)
    {
        //カメラの回転に制限をつける
        //X軸の角度が - 180～180度の間にする
        float angle_x = 180f <= this.transform.eulerAngles.x ? this.transform.eulerAngles.x - 360 : this.transform.eulerAngles.x;
       
        //下回転
        if (y > 0 && angle_x <= ANGLE_LIMIT_MIN) return;
        //上回転
        if (y < 0 && angle_x >= ANGLE_LIMIT_MAX) return;
        
        // マウス(タッチ)の移動した値が一定値を越えたら最大(最小)値に設定
        if (y > MOVE_LIMIT_MAX)
            y = MOVE_LIMIT_MAX;

        if (y < MOVE_LIMIT_MIN)
            y = MOVE_LIMIT_MIN;        

        // 縦回転
        this.transform.RotateAround(targetPoint, this.transform.right, -y);
        // 横回転
        this.transform.RotateAround(targetPoint, Vector3.up, x);
               
    }

    // カメラのズーム
    private void ZoomCamera(float scroll)
    {        
        // 拡大率を設定
        float view = cam.fieldOfView - scroll * zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(view, ZOOM_LIMIT_MIN, ZOOM_LIMIT_MAX);
    }

    // デバッグ用ギズモを表示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(targetPoint, 0.1f);
    }

    // 動かせるかのフラグのアクセッサ
    public bool MoveFlag
    {
        set { moveFlag = value; }
        get { return moveFlag; }
    }

}
