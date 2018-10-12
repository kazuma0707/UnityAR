using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharaCreCameraCtrl : MonoBehaviour
{
    private const float ANGLE_LIMIT_UP = 60.0f;
    private const float ANGLE_LIMIT_DOWN = -60.0f;
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

        //int touchCount = Input.touches.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
        //if (touchCount == 1)
        //{
        //    Touch t = Input.touches.First();
        //    switch (t.phase)
        //    {
        //        case TouchPhase.Moved:

        //            //移動量
        //            float xDelta = t.deltaPosition.x * rotateSpeed;
        //            float yDelta = t.deltaPosition.y * translateSpeed;

        //            //左右回転
        //            this.transform.Rotate(0, xDelta, 0, Space.World);
        //            //上下移動
        //            this.transform.position += new Vector3(0, -yDelta, 0);

        //            break;
        //    }
        //}

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

        // 回転(右クリックでドラッグ)
        //if (Input.GetMouseButton(1))
        //{
        //    float dist = Vector3.Distance(this.transform.position, targetPoint);

        //    this.transform.rotation = Quaternion.AngleAxis(rotateSpeed * -mouseY, transform.right) * transform.rotation;
        //    this.transform.rotation = Quaternion.AngleAxis(rotateSpeed * mouseX, Vector3.up) * transform.rotation;

        //    targetPoint = this.transform.position + this.transform.forward * dist;
        //}

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
        //カメラの回転に制限をつける
        // Mathf.Clamp()での制限を容易にする為、X軸の角度が-180～180度の間にする
        float angle_x = 180f <= this.transform.eulerAngles.x ? this.transform.eulerAngles.x - 360 : this.transform.eulerAngles.x;
        this.transform.eulerAngles = new Vector3(Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
                                        this.transform.eulerAngles.y,
                                        this.transform.eulerAngles.z);

        //上回転
        if (mouseY > 0 && angle_x <= minAngle) return;
        //下回転
        if (mouseY < 0 && angle_x >= maxAngle) return;

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
