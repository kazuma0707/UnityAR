using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CharaCreCameraCtrl : MonoBehaviour
{
    private const float ANGLE_LIMIT_UP = 60.0f;
    private const float ANGLE_LIMIT_DOWN = -20.0f;
    private const float ZOOM_LIMIT_MAX = 60.0f;
    private const float ZOOM_LIMIT_MIN = 2.0f;

    //制限角度
    [SerializeField]
    private const float maxAngle = 55f;     // 最大角度
    [SerializeField]
    private const float minAngle = 0f;      // 最低角度

    [SerializeField]
    private Transform targetObj;            // 注視するオブジェクト
    [SerializeField]
    private float rotateSpeed = 5.0f;       // 回転する速度
    [SerializeField]
    private float translateSpeed = 0.5f;    // 移動する速度
    [SerializeField]
    private float zoomSpeed = 1.0f;         // 拡大する速度    
    private Vector3 targetPoint;           // 注視点
    private bool moveFlag = false;         // グリグリ動かせるかのフラグ

    private Camera cam;

    [SerializeField]
    private Text text;

   // Use this for initialization
   void Start ()
    {
        targetPoint = targetObj.transform.position;
        cam = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // グリグリ動かせない状態であれば何もしない
        if (!moveFlag) return;

        int touchCount = Input.touches.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
        if (Input.touchCount == 1)
        {
            Touch t = Input.touches.First();

            // カメラ回転
            float xDelta = t.deltaPosition.x * rotateSpeed;
            float yDelta = t.deltaPosition.y * rotateSpeed;
            RotateCamera(xDelta, yDelta);
        }
        else if (Input.touchCount == 2)
        {
            // カメラ移動
            Touch t = Input.touches.First();
            float xDelta = t.deltaPosition.x * translateSpeed;
            float yDelta = t.deltaPosition.y * translateSpeed;
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

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseWheelScroll = Input.GetAxis("Mouse ScrollWheel");

        // 平行移動(ホイール押下でドラッグ)
        if (Input.GetMouseButton(2))
        {
            //targetPoint += transform.right * mouseX * translateSpeed;
            //targetPoint += transform.up * mouseY * translateSpeed;

            this.transform.Translate(mouseX * translateSpeed, mouseY * translateSpeed, 0);
        }

        // ズーム(ホイール回転)
        if (mouseWheelScroll != 0) ZoomCamera(mouseWheelScroll);

        // 注視点の周りを回る(ALTキー＋左クリックでドラッグ)
        if (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            RotateCamera(mouseX, mouseY);
        }
    }

    // カメラの回転
    private void RotateCamera(float mouseX, float mouseY)
    {
        //Vector3 angle = new Vector3(
        //Input.GetAxis("Mouse X") * rotateSpeed,
        //Input.GetAxis("Mouse Y") * rotateSpeed,
        //0);

        //transform.eulerAngles += new Vector3(angle.y, angle.x);
        //カメラの回転に制限をつける
        //X軸の角度が - 180～180度の間にする
        float angle_x = 180f <= this.transform.eulerAngles.x ? this.transform.eulerAngles.x - 360 : this.transform.eulerAngles.x;
        //this.transform.eulerAngles = new Vector3(Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
        //                                this.transform.eulerAngles.y,
        //                                this.transform.eulerAngles.z);
        
        //下回転
        if (mouseY > 0 && angle_x <= ANGLE_LIMIT_DOWN) return;
        //上回転
        if (mouseY < 0 && angle_x >= ANGLE_LIMIT_UP) return;
        Mathf.Clamp(mouseY, -10.0f, 10.0f);
        text.text = "Y:" + mouseY.ToString();
        
        this.transform.RotateAround(targetPoint, this.transform.right, -mouseY * rotateSpeed);
        this.transform.RotateAround(targetPoint, Vector3.up, mouseX * rotateSpeed);
               
    }

    // カメラのズーム
    private void ZoomCamera(float scroll)
    {
        // 拡大率を設定
        float view = cam.fieldOfView - scroll * zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(view, ZOOM_LIMIT_MIN, ZOOM_LIMIT_MAX);
    }

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
