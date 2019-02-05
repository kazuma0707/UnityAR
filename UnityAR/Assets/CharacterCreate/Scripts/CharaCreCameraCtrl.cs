using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// キャラクリ用カメラが特定の部位を選択時に移動する位置の登録番号
public enum CCCSetPosNum
{
    FACE_POS,
    BODY_POS
}

public class CharaCreCameraCtrl : MonoBehaviour
{
    // 定数宣言 //////////////////////////////////////////////////////////////////////////////////

    private const string STATUS_BUTTON = "StatusButton";   // StatusButtonのタグ
    private const float ANGLE_LIMIT_MAX = 60.0f;            // 角度の最大値
    private const float ANGLE_LIMIT_MIN = -20.0f;           // 角度の最小値
    private const float ZOOM_LIMIT_MAX = 60.0f;             // 拡大の最大値
    private const float ZOOM_LIMIT_MIN = 2.0f;              // 拡大の最小値
    private const float TOUCH_POS_LIMIT_MAX = 525.0f;       // タッチポジションの最大値
    private const float TOUCH_POS_LIMIT_MIN = 300.0f;       // タッチポジションの最小値
    private const float MOVE_LIMIT_MAX = 5.0f;              // マウス(タッチ)の移動した値の最小値
    private const float MOVE_LIMIT_MIN = -5.0f;             // マウス(タッチ)の移動した値の最小値

    //////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField]
    private Transform targetObj;                               // 注視するオブジェクト
    [SerializeField]
    private GameObject[] charaCreButtons;                      // キャラクリするボタン
    [SerializeField]
    private GameObject[] camSetPositions;                      // キャラクリ用カメラが特定の部位を選択時に移動する位置
    [SerializeField]
    private GameObject[] VPSetPositions;                       // キャラクリ用カメラが特定の部位を選択時のViewPointの位置
    [SerializeField]
    private float rotateSpeed = 5.0f;                          // 回転する速度
    [SerializeField]
    private float translateSpeed = 0.5f;                       // 移動する速度
    [SerializeField]
    private float zoomSpeed = 1.0f;                            // 拡大する速度
    [SerializeField]
    private float lerpTime = 1.0f;                             // 補間する時間
    private float touchPosLimit = TOUCH_POS_LIMIT_MIN;         // タッチポジションの限界値    
    private Vector3 targetPoint;                               // 注視点
    private bool moveFlag = false;                             // グリグリ動かせるかのフラグ
    private Camera cam;                                        // カメラコンポーネント

    [SerializeField]
    private GameObject[] monitors;                             // 部屋のモニター   
        
    private Vector3 defaultTargetPos;                          // ターゲットポジションのデフォルト値
    private CursorHit cursorHit;                               // CursorHitコンポーネント

    private List<RaycastResult> raycastResults = new List<RaycastResult>();

    // Use this for initialization
    void Start ()
    {
        cam = this.GetComponent<Camera>();
        cursorHit = this.GetComponent<CursorHit>();
        defaultTargetPos = this.gameObject.transform.position;
    }
    
    // Update is called once per frame
    void Update ()
    {
        // グリグリ動かせない状態であれば何もしない
        if (!moveFlag) return;

        CursorSetting();

        targetPoint = targetObj.transform.position;

        // キャラクリ用のボタンを検索
        for (int i = 0; i < charaCreButtons.Length; i++)
        {
            // アクティブでなければタッチポジションの最小値を設定
            touchPosLimit = TOUCH_POS_LIMIT_MIN;
        }
        
        // 各デバイスごとに処理を変える
#if UNITY_EDITOR
        UnityEditorMouse();
#else
        SmartPhoneTouch();
#endif
    }

    // 画面上のUIに触れているかを検知
    private bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;
        
        // リストに検知結果を入れる
        EventSystem.current.RaycastAll(eventDataCurrentPosition, raycastResults);
        
        bool over = false;

        // 何もなければfalseを返す
        if (raycastResults.Count <= 0) return over;

        // 検知したオブジェクト分を調べる
        for (int result = 0; result < raycastResults.Count; result++)
        {
            // StatusButtonのタグが付いたオブジェクトかどうか
            if (raycastResults[result].gameObject.tag == STATUS_BUTTON)
            {
                over = true;
                break;
            }
            over = false;
        }
        
        // リストをクリア
        raycastResults.Clear();
        return over;
    }

    // Unityエディタ上のマウス処理
    private void UnityEditorMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseWheelScroll = Input.GetAxis("Mouse ScrollWheel");

        // 画面サイズの41%の値を取得
        float posY = (float)Screen.height * 0.41f;

        // 平行移動(ホイール押下でドラッグ)
        if (Input.GetMouseButton(2))
        {
            // UIに触れていたらカメラを動かさない
            //if (Input.mousePosition.y < posY) return;
            if (IsPointerOverUIObject(Input.mousePosition)) return;
            this.transform.Translate(-mouseX * translateSpeed, -mouseY * translateSpeed, 0);
            targetObj.transform.Translate(-mouseX * translateSpeed, -mouseY * translateSpeed, 0);
        }

        // ズーム(ホイール回転)
        if (mouseWheelScroll != 0) ZoomCamera(mouseWheelScroll);

        // 注視点の周りを回る(左クリック＋ドラッグ)
        if (Input.GetMouseButton(0))
        {
            // UIに触れていたらカメラを動かさない
            //if (Input.mousePosition.y < posY) return;
            if (IsPointerOverUIObject(Input.mousePosition)) return;
            RotateCamera(mouseX, mouseY);
        }
    }

    // スマホ上のタッチ処理
    private void SmartPhoneTouch()
    {
        int touchCount = Input.touches.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);

        if (Input.touchCount == 1)
        {           
            // UIに触れていたらカメラを動かさない
            if (IsPointerOverUIObject(Input.GetTouch(0).position)) return;

            Touch t = Input.touches.First();

            // カメラ回転
            float xDelta = t.deltaPosition.x * rotateSpeed;
            float yDelta = t.deltaPosition.y * rotateSpeed;
            RotateCamera(xDelta, yDelta);
        }
        else if (Input.touchCount == 2)
        {
            if (IsPointerOverUIObject(Input.GetTouch(0).position)) return;

            // カメラ移動
            Touch t = Input.touches.First();

            float xDelta = t.deltaPosition.x * translateSpeed;
            float yDelta = t.deltaPosition.y * translateSpeed;
            xDelta = Mathf.Clamp(xDelta, -5.0f, 5.0f);
            yDelta = Mathf.Clamp(yDelta, -5.0f, 5.0f);
            this.transform.Translate(-xDelta, -yDelta, 0);
            targetObj.transform.Translate(-xDelta, -yDelta, 0);

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
    }

    // カメラの回転
    private void RotateCamera(float x, float y)
    {
        //カメラの回転に制限をつける
        //X軸の角度が - 180～180度の間にする
        float angle_x = 180f <= this.transform.eulerAngles.x ? this.transform.eulerAngles.x - 360 : this.transform.eulerAngles.x;
       
        // 下回転の上限
        if (y > 0 && angle_x <= ANGLE_LIMIT_MIN) return;
        // 上回転の上限
        if (y < 0 && angle_x >= ANGLE_LIMIT_MAX) return;
        
        // マウス(タッチ)の移動した値が一定値を越えたら最大(最小)値に設定
        if (y > MOVE_LIMIT_MAX) y = MOVE_LIMIT_MAX;

        if (y < MOVE_LIMIT_MIN) y = MOVE_LIMIT_MIN;
       
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

    // CursorHitの設定
    private void CursorSetting()
    {
        Vector3 pos = this.gameObject.transform.position;
        // 一定の角度に達したら
        if (pos.z > 2.0f)
        {
            cursorHit.headLook.target = defaultTargetPos;
            // CursorHitコンポーネントを非アクティブ化
            cursorHit.enabled = false;
        }
        else
        {
            // CursorHitコンポーネントをアクティブ化
            cursorHit.enabled = true;
        }
    }
    
    // キャラクリ用カメラとViewPointを特定の位置に移動させる
    public void CameraSetPos(CCCSetPosNum cccSetPosNum)
    {          
        switch (cccSetPosNum)
        {
            case CCCSetPosNum.FACE_POS:
            default:
                // キャラクリ用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)CCCSetPosNum.FACE_POS].transform.position,
                         Vector3.zero, lerpTime, iTween.EaseType.linear);
                // ViewPointの補間移動・回転
                LerpMove(targetObj.gameObject, VPSetPositions[(int)CCCSetPosNum.FACE_POS].transform.position,
                         Vector3.zero, lerpTime, iTween.EaseType.linear);
                break;
                            
            case CCCSetPosNum.BODY_POS:
                // キャラクリ用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)CCCSetPosNum.BODY_POS].transform.position,
                         Vector3.zero, lerpTime, iTween.EaseType.linear);
                // ViewPointの補間移動・回転
                LerpMove(targetObj.gameObject, VPSetPositions[(int)CCCSetPosNum.BODY_POS].transform.position,
                         Vector3.zero, lerpTime, iTween.EaseType.linear);               
                break;
        }

        // 拡大率を初期化
        cam.fieldOfView = ZOOM_LIMIT_MAX;
    }

    // キャラクリ用カメラとViewPointの補間移動・回転
    private void LerpMove(GameObject obj, Vector3 pos, Vector3 rot, float time, iTween.EaseType type)
    {
        // 回転
        iTween.RotateTo(obj, iTween.Hash("x", rot.x, "y", rot.y, "z", rot.z, "time", time));
        
        // 移動
        iTween.MoveTo(obj, iTween.Hash("x", pos.x, "y", pos.y, "z", pos.z,
            "time", time, "EaseType", type));       
    }

    // 非アクティブ化した時に呼び出される関数
    private void OnDisable()
    {
        // フェーズをセレクトに設定
        MyCharDataManager.Instance.phase = Phase.SELECT;
    }

    // アクティブ化した時に呼び出される関数
    private void OnEnable()
    {
        // 初期位置を設定
        this.transform.position = camSetPositions[(int)CCCSetPosNum.BODY_POS].transform.position;
        targetObj.transform.position = VPSetPositions[(int)CCCSetPosNum.BODY_POS].transform.position;
        MyCharDataManager.Instance.phase = Phase.CHARA_CREATE;

        // モニターを非アクティブ化
        foreach (GameObject monitor in monitors)
        {
            monitor.SetActive(false);
        }
    }

    // 動かせるかのフラグのアクセッサ
    public bool MoveFlag
    {
        set { moveFlag = value; }
        get { return moveFlag; }
    }
    
}
