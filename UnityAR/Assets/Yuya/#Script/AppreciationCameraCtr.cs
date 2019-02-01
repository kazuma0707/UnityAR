//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   AppreciationCameraCtr.cs
//!
//! @brief  アプリケーションシーンのカメラスクリプト
//!
//! @date   2018/11/4 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using ConstantName;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ACCSetPosNum
{
    DEFAULT_POS,
    POSE1_POS,
    POSE2_POS,
    POSE3_POS,
    POSE4_POS,
    POSE5_POS,
    POSE6_POS,
    POSE7_POS,
    POSE8_POS,
    POSE9_POS,
    POSE10_POS
}

public class AppreciationCameraCtr : MonoBehaviour
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
    private GameObject[] AppreciationObjects;                  // 鑑賞シーンのオブジェクト
    [SerializeField]
    private GameObject[] camSetPositions;                      // 鑑賞用カメラが特定の部位を選択時に移動する位置
    [SerializeField]
    private GameObject[] VPSetPositions;                       // 鑑賞用カメラが特定の部位を選択時のViewPointの位置
    [SerializeField]
    private float rotateSpeed = 5.0f;                          // 回転する速度
    [SerializeField]
    private float translateSpeed = 0.5f;                       // 移動する速度
    [SerializeField]
    private float zoomSpeed = 1.0f;                            // 拡大する速度 
    [SerializeField]
    private float lerpTime = 1.0f;                             // 補間する時間
    private float touchPosLimit = TOUCH_POS_LIMIT_MIN;         // 拡大する速度    
    private Vector3 targetPoint;                               // 注視点
    private Camera cam;                                        // カメラコンポーネント

    [Header("管理するオブジェクト")]
    [SerializeField]
    private Button[] menuButtons;   // 対応するオブジェクト
    [SerializeField]
    private Button[] poseButtons;   // 対応するオブジェクト
    [SerializeField]
    private Button[] convButtons;   // 対応するオブジェクト
    [SerializeField]
    private Button arButton;

    [SerializeField]
    private GameObject Panel;
    // はいボタン
    [SerializeField]
    private GameObject YesButton;
    // いいえボタン
    [SerializeField]
    private GameObject NoButton;

    [SerializeField]
    private GameObject variable;
    private Variable variable_cs;


    //----------------------------------------------------------------------
    //! @brief Startメソッド
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
        cam = GetComponent<Camera>();

        // 初期位置を設定
        Debug.Log("this.transform.position : " + this.transform.position);
        Debug.Log("camSetPositions[(int)ACCSetPosNum.DEFAULT_POS].transform.localPosition : " + camSetPositions[(int)ACCSetPosNum.DEFAULT_POS].transform.localPosition);
        this.transform.position = camSetPositions[(int)ACCSetPosNum.DEFAULT_POS].transform.localPosition;
        Debug.Log("this.transform.positionAAAAAAA : " + this.transform.position);

        this.transform.eulerAngles = camSetPositions[(int)ACCSetPosNum.DEFAULT_POS].transform.eulerAngles;
        targetObj.transform.position = VPSetPositions[0].transform.localPosition;


        variable_cs = variable.GetComponent<Variable>();

    }

    //----------------------------------------------------------------------
    //! @brief Updateメソッド
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        targetPoint = targetObj.transform.position;

        // 鑑賞用のオブジェクトを検索
        for (int i = 0; i < AppreciationObjects.Length; i++)
        {
            // アクティブであればタッチポジションの最大値を設定
            if (AppreciationObjects[i].activeInHierarchy)
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

    //----------------------------------------------------------------------
    //! @brief カメラの回転処理
    //!
    //! @param[in] x座標,ｙ座標
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // 
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
    
    //----------------------------------------------------------------------
    //! @brief カメラのズーム処理
    //!
    //! @param[in] スクロールする量
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void ZoomCamera(float scroll)
    {
        // 拡大率を設定
        float view = cam.fieldOfView - scroll * zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(view, ZOOM_LIMIT_MIN, ZOOM_LIMIT_MAX);
    }


    //----------------------------------------------------------------------
    //! @brief 鑑賞用カメラとViewPointを特定の位置に移動させる
    //!
    //! @param[in] ポジションナンバー
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void CameraSetPos(ACCSetPosNum accSetPosNum)
    {
        switch (accSetPosNum)
        {
            case ACCSetPosNum.DEFAULT_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.DEFAULT_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.DEFAULT_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE1_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE1_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE1_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE2_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE2_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE2_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE3_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE3_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE3_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE4_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE4_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE4_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE5_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE5_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE5_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE6_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE6_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE6_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE7_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE7_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE7_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE8_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE8_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE8_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE9_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE9_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE9_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
            case ACCSetPosNum.POSE10_POS:
                // 鑑賞用カメラの補間移動・回転
                LerpMove(this.gameObject, camSetPositions[(int)ACCSetPosNum.POSE10_POS].transform.localPosition,
                         camSetPositions[(int)ACCSetPosNum.POSE10_POS].transform.eulerAngles, lerpTime, iTween.EaseType.linear);
                break;
        }

        // 拡大率を初期化
        cam.fieldOfView = ZOOM_LIMIT_MAX;
    }


    //----------------------------------------------------------------------
    //! @brief ラープ処理
    //!
    //! @param[in] オブジェクト、ポジション、ローテーション、時間、タイプ
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void LerpMove(GameObject obj, Vector3 pos, Vector3 rot, float time, iTween.EaseType type)
    {
        // 回転
        iTween.RotateTo(obj, iTween.Hash("x", rot.x, "y", rot.y, "z", rot.z, "time", time));

        // 移動
        iTween.MoveTo(obj, iTween.Hash("x", pos.x, "y", pos.y, "z", pos.z,
            "time", time, "EaseType", type));
    }




    //----------------------------------------------------------------------
    //! @brief 学校紹介ボタンを押したときの処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void OnSchoolIntroduction()
    {
        SceneManager.LoadScene(SceneName.ARScene);
    }

    //----------------------------------------------------------------------
    //! @brief ゲームボタンを押したときの処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void OnGame()
    {
        FadeManager.Instance.LoadScene(SceneName.Title, 2.0f);
    }

    //----------------------------------------------------------------------
    //! @brief キャラクタークリエイトボタンを押したときの処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void OnReCharacterCreate()
    {
        FadeManager.Instance.LoadScene(SceneName.CharCreate, 2.0f);
    }

    public void OnAppreciationAR()
    {
        FadeManager.Instance.LoadScene(SceneName.AppreciationAR, 2.0f);
    }

    public void NonPanelUI()
    {
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].interactable = true;
        }

        for (int i = 0; i < poseButtons.Length; i++)
        {
            poseButtons[i].interactable = true;
        }

        for (int i = 0; i < convButtons.Length; i++)
        {
            convButtons[i].interactable = true;
        }

        variable_cs.Active = !variable_cs.Active;
    }
}
