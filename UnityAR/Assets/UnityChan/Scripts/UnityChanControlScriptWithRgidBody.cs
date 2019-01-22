//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 必要なコンポーネントの列記
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (BoxCollider/*CapsuleCollider*/))]
[RequireComponent(typeof (Rigidbody))]

public class UnityChanControlScriptWithRgidBody : MonoBehaviour
{
    //=============  定数　===============//
    private const float JUMP_LIMIT = 2.3f;                  //  ジャンプ可能な高さの限界値
    private const float ADJUSTMENT = 0.01f;                 //  ゲームバランスを調整する定数
    private const float LEVEL2_ANIME_SPEED = 2.4f;          //  レベル2の時のアニメーションスピード


    private float jumpHeight = 1.7f;//1.5f;          //  ジャンプの高さ
    private float animSpeed = 1.5f;				// アニメーション再生速度設定
	public float lookSmoother = 3.0f;			// a smoothing setting for camera motion
	public bool useCurves = true;				// Mecanimでカーブ調整を使うか設定する
												// このスイッチが入っていないとカーブは使われない
	public float useCurvesHeight = 0.5f;		// カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

	// 以下キャラクターコントローラ用パラメタ
	// 前進速度
	public float forwardSpeed = 7.0f;
	// 後退速度
	public float backwardSpeed = 2.0f;
	// 旋回速度
	public float rotateSpeed = 2.0f;
    // ジャンプ威力
    //[SerializeField]
    private float jumpPower = 11.5f;
    //  ジャンプ時の補間用座標
    private Vector3 jumpCenterPos;
	// キャラクターコントローラ（カプセルコライダ）の参照
	private BoxCollider col;
	private Rigidbody rb;
	// キャラクターコントローラ（カプセルコライダ）の移動量
	private Vector3 velocity;
	// CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
	private float orgColHight;
	private Vector3 orgVectColCenter;
	
	private Animator anim;							// キャラにアタッチされるアニメーターへの参照
	private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照

	private GameObject cameraObject;	// メインカメラへの参照
		
    // アニメーター各ステートへの参照
	static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int restState = Animator.StringToHash("Base Layer.Rest");
    static int deadState = Animator.StringToHash("Base Layer.Dead");

    private Vector3 localGravity = new Vector3(0, -3.0f, 0);

    //  ボタン用フラグ
    public bool LeftButtonFlag = false;
    public bool CenterButtonFlag = false;
    public bool RightButtonFlag = false;

    //  線形補間用変数
    float time = 0.7f;
    Vector3 endPosition;
    private float startTime;
    private Vector3 startPosition;
    [SerializeField]
    private GameObject LeftPosition;
    [SerializeField]
    private GameObject CenterPosition;
    [SerializeField]
    private GameObject RightPosition;
    [SerializeField]
    private bool lerpflag = false;
    [SerializeField]
    private GameObject manager;
    
    private bool floorFlag = false;                         //  プレイヤーが床に接触しているかどうか
    private bool obstacleFlag = false;                      //  障害物に当たったかどうかのフラグ
    private bool LRjumpFlag = false;                        //  左右ジャンプ時のフラグ

    //  ジャンプ用変数
    bool jumpFlag = false;                                  //  ジャンプ中かどうかのフラグ
    float jumpVel = 0.0f;
    [SerializeField]
    bool jumpPossibleFlag = false;                          //  ジャンプ可能かどうかのフラグ
    private float downVelocity = 0.4f;                      //  ジャンプ落下時の速度変化

    //特殊障害物に当たったときのフラグ
    private bool isFilipEvent=false;
    private bool isNoizeEye = false;

    public Material NoizeMat;

    //  シーンのロードが複数行われるのの防止
    bool isLoad = false;

    bool deadFlag = false;

    // 初期化
    void Start ()
	{
		// Animatorコンポーネントを取得する
		anim = GetComponent<Animator>();
		// CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
		col = GetComponent<BoxCollider>();
		rb = GetComponent<Rigidbody>();
		//メインカメラを取得する
		cameraObject = GameObject.FindWithTag("MainCamera");
		// CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
		orgColHight = col.size.y;
		orgVectColCenter = col.center;
        Camera.main.GetComponent<CRT>().enabled = false;
        //rb.useGravity = true;
    }

    void Update()
    {
    
        if (isFilipEvent)
        {
            FilipButton();
        }
        if(isNoizeEye)
        {
            NoizeEye();
        }
    
       

        gameObject.transform.Translate(0, -manager.GetComponent<GameManager>().FallSpeed - ADJUSTMENT, 0);

        //  プレイヤーの上方向に速度が加わったら
        if (rb.velocity.y == jumpPower)
        {
            //  速度の代入処理
            jumpVel = rb.velocity.y;
            //  ジャンプフラグをtrueにする
            jumpFlag = true;
        }

        //  ジャンプ中なら
        if (jumpFlag)
        {
            //  プレイヤーの速度が0以上なら
            if (jumpVel >= 0.0f)
            {
                jumpVel -= downVelocity;
                rb.velocity = new Vector3(0.0f, jumpVel, 0.0f);
            }
            else
            {
                jumpFlag = false;
                rb.useGravity = true;
            }
        }

        //  ゲームレベルが2になったらプレイヤーの速度を上げる
        if(manager.GetComponent<GameManager>().Level == 2)
        {
            //jumpPower = 15.0f;
            //animSpeed = 2.4f;
            //downVelocity = 0.6f;
            jumpHeight = 1.3f;
            jumpPower = 24.0f;
            downVelocity = 1.5f;
            //animSpeed = 2.4f;
            if (LRjumpFlag)
            {
                animSpeed = 2.6f;
            }
            else
            {
                animSpeed = 2.4f;
            }
        }

        //  プレイヤーが一定以下に落ちたら
        if(this.transform.position.y <= -3.0f)
        {
            //  シーンが複数ロードされるのの防止
            if (!isLoad)
            {
                //SceneManager.LoadScene("Ranking");
                FadeManager.Instance.LoadScene("Ranking", 2.0f);
            }
            isLoad = true;
        }

        //  障害物と当たったら
        if(obstacleFlag == true)
        {
            //  TimeScaleに依存しないタイム
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            //  時間を止める
            Time.timeScale = 0.0f;
            //  死亡フラグをtrueに
            deadFlag = true;
            //  ゲームをポーズする
            manager.GetComponent<GameManager>().pauseFlag = true;


            //  シーンが複数ロードされるのの防止
            if (!isLoad)
            {
                //SceneManager.LoadScene("Ranking");
                FadeManager.Instance.LoadScene("Ranking", 2.0f);
            }
            isLoad = true;
        }

        //  プレイヤーの下に床があるかどうか
        if(RaycastFloor() == false)
        {
            FloorFlag = false;
        }
    }


    // 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");              // 入力デバイスの水平軸をhで定義
        float v = Input.GetAxis("Vertical");                // 入力デバイスの垂直軸をvで定義
        anim.SetFloat("Speed", v);                          // Animator側で設定している"Speed"パラメタにvを渡す
        anim.SetFloat("Direction", h);                      // Animator側で設定している"Direction"パラメタにhを渡す
                                                            //anim.speed = animSpeed;	                            // Animatorのモーション再生速度に animSpeedを設定する
        anim.SetFloat("Speed", animSpeed);

        currentBaseState = anim.GetCurrentAnimatorStateInfo(0); // 参照用のステート変数にBase Layer (0)の現在のステートを設定する                                                        //

        rb.useGravity = true;//ジャンプ中に重力を切るので、それ以外は重力の影響を受けるようにする

        // rb.AddForce(localGravity, ForceMode.VelocityChange);        


        // 以下、キャラクターの移動処理
        //velocity = new Vector3(0, 0, v);		// 上下のキー入力からZ軸方向の移動量を取得
        // キャラクターのローカル空間での方向に変換
        //velocity = transform.TransformDirection(velocity);
        //以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
        //if (v > 0.1) {
        //	velocity *= forwardSpeed;		// 移動速度を掛ける
        //} else if (v < -0.1) {
        //	velocity *= backwardSpeed;	// 移動速度を掛ける
        //}

        //if (Input.GetButtonDown("Jump"))

        //  プレイヤーの高さが一定以下ならジャンプ処理を行う
        if (this.transform.position.y < JUMP_LIMIT)
        {
            //  ボタン押下時の処理
            PressButton();
        }
        else
        {
            ButtonFlagReset();
            jumpPossibleFlag = false;
        }

        //  フラグがtrueなら補間処理を行う
        if (lerpflag == true)
        {

            float lerpTime = Time.timeSinceLevelLoad - startTime;
            if (lerpTime > time)
            {
                transform.position = endPosition;
                lerpflag = false;
            }

            float rate;
            //  レベル2の状態なら補間スピードを上げる
            if (manager.GetComponent<GameManager>().Level == 2)
            {
                 rate = lerpTime / time * 3.0f;
            }
            else
            {
                 rate = lerpTime / time * 2.0f;
            }
            //transform.position = Vector3.Lerp(startPosition, endPosition, rate);
            //transform.position = Vector3.Slerp(startPosition, endPosition, rate);

            Vector3 firstPos = Vector3.Lerp(startPosition, jumpCenterPos, rate);
            Vector3 secondPos = Vector3.Lerp(jumpCenterPos, endPosition, rate);
            transform.position = Vector3.Lerp(firstPos, secondPos, rate);
        }

        // 上下のキー入力でキャラクターを移動させる
        transform.localPosition += velocity * Time.fixedDeltaTime;

		// 左右のキー入力でキャラクタをY軸で旋回させる
		transform.Rotate(0, h * rotateSpeed, 0);	
	

		// 以下、Animatorの各ステート中での処理
		// Locomotion中
		// 現在のベースレイヤーがlocoStateの時
		if (currentBaseState.nameHash == locoState){
			//カーブでコライダ調整をしている時は、念のためにリセットする
			if(useCurves){
				resetCollider();
			}
		}
		// JUMP中の処理
		// 現在のベースレイヤーがjumpStateの時
		else if(currentBaseState.nameHash == jumpState)
		{
			//cameraObject.SendMessage("setCameraPositionJumpView");	// ジャンプ中のカメラに変更
			// ステートがトランジション中でない場合
			if(!anim.IsInTransition(0))
			{
				// 以下、カーブ調整をする場合の処理
				if(useCurves){
					// 以下JUMP00アニメーションについているカーブJUMP_HEIGHTとGravityControl
					// JUMP_HEIGHT:JUMP00でのジャンプの高さ（0〜1）
					// GravityControl:1⇒ジャンプ中（重力無効）、0⇒重力有効
					float JUMP_HEIGHT = anim.GetFloat("JumpHeight");
					float gravityControl = anim.GetFloat("GravityControl");
                    //if (gravityControl > 0)
                    //    rb.useGravity = false;	//ジャンプ中の重力の影響を切る

                    // レイキャストをキャラクターのセンターから落とす
                    Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					// 高さが useCurvesHeight 以上ある時のみ、コライダーの高さと中心をJUMP00アニメーションについているカーブで調整する
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							//col.size = new Vector3(col.size.x, orgColHight - JUMP_HEIGHT, col.size.z);			// 調整されたコライダーの高さ
							//float adjCenterY = orgVectColCenter.y + JUMP_HEIGHT;
							//col.center = new Vector3(0, adjCenterY, 0);	// 調整されたコライダーのセンター
						}
						else{
							// 閾値よりも低い時には初期値に戻す（念のため）					
							resetCollider();
						}
					}
				}
				// Jump bool値をリセットする（ループしないようにする）				
				anim.SetBool("Jump", false);
                LRjumpFlag = false;
                //rb.velocity = new Vector3(0, 0, 0);

            }
		}
		// IDLE中の処理
		// 現在のベースレイヤーがidleStateの時
		else if (currentBaseState.nameHash == idleState)
		{
			//カーブでコライダ調整をしている時は、念のためにリセットする
			if(useCurves){
				resetCollider();
			}
			// スペースキーを入力したらRest状態になる
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Rest", true);
			}
		}
		// REST中の処理
		// 現在のベースレイヤーがrestStateの時
		else if (currentBaseState.nameHash == restState)
		{
			//cameraObject.SendMessage("setCameraPositionFrontView");		// カメラを正面に切り替える
			// ステートが遷移中でない場合、Rest bool値をリセットする（ループしないようにする）
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Rest", false);
			}
		}
	}

    //  他のオブジェクトと接触したとき
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagName.Obstacle)
        {
            obstacleFlag = true;
        }
        if (other.gameObject.tag == TagName.FlipButton)
        {
            isFilipEvent = true;
        }
        if(other.gameObject.tag == TagName.BadEye)
        {
            isNoizeEye=true;
 
        }
    }

    //  他のオブジェクトと接触している間
    private void OnCollisionStay(Collision collision)
    {
    
        if(collision.gameObject.tag ==TagName.Floor)
        {
            
            floorFlag = true;
        }
    }

    //  他のオブジェクトとの接触が離れた時
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == TagName.Floor)
        {
            floorFlag = false;
        }
   

    }

    void OnGUI()
	{
		//GUI.Box(new Rect(Screen.width -260, 10 ,250 ,150), "Interaction");
		//GUI.Label(new Rect(Screen.width -245,30,250,30),"Up/Down Arrow : Go Forwald/Go Back");
		//GUI.Label(new Rect(Screen.width -245,50,250,30),"Left/Right Arrow : Turn Left/Turn Right");
		//GUI.Label(new Rect(Screen.width -245,70,250,30),"Hit Space key while Running : Jump");
		//GUI.Label(new Rect(Screen.width -245,90,250,30),"Hit Spase key while Stopping : Rest");
		//GUI.Label(new Rect(Screen.width -245,110,250,30),"Left Control : Front Camera");
		//GUI.Label(new Rect(Screen.width -245,130,250,30),"Alt : LookAt Camera");
	}

    public bool FloorFlag
    {
        get { return floorFlag; }
        set { floorFlag = value; }
    }


    // キャラクターのコライダーサイズのリセット関数
    void resetCollider()
	{
	// コンポーネントのHeight、Centerの初期値を戻す
		//col.size = new Vector3(col.size.x, orgColHight, col.size.z);
		//col.center = orgVectColCenter;
	}


    /****************************************************************
    *|　機能　線形補間
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    void Lerp()
    {
        if (time <= 0)
        {
            transform.position = endPosition;
            enabled = false;
            return;
        }

        startTime = Time.timeSinceLevelLoad;
        startPosition = transform.position;
    }


    /****************************************************************
    *|　機能　ボタン押下時に行う処理
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    void PressButton()
    {
        jumpPossibleFlag = false;

        //  プレイヤーが空中にいるならreturn
        if (floorFlag == false)
        {
            ButtonFlagReset();
            return;
        }


        jumpPossibleFlag = true;

        //  中央のボタンが押されたら
        if (CenterButtonFlag == true)
        {   
            //アニメーションのステートがLocomotionの最中のみジャンプできる
            if (currentBaseState.nameHash == idleState)
            {
                //ステート遷移中でなかったらジャンプできる
                if (!anim.IsInTransition(0))
                {
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    anim.SetBool("Jump", true);     // Animatorにジャンプに切り替えるフラグを送る

                    jumpVel = rb.velocity.y;
                    jumpFlag = true;
                    rb.useGravity = false;
                }

            }
            CenterButtonFlag = false;
        }

        //  左ボタンが押されたら
        if (LeftButtonFlag == true)
        {
            //  左ボタン押下時の処理を行う
            PressButtonLeft();
        }

        //  右ボタンが押されたら
        if (RightButtonFlag == true)
        {
            //  右ボタン押下時の処理を行う
            PressButtonRight();
        }
    }

    /****************************************************************
    *|　機能　左ボタン押下時に行う処理
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    void PressButtonLeft()
    {
        //  プレイヤーが左側にいるときはジャンプを行わない
        if (transform.position.x > LeftPosition.transform.position.x)
        {
            //アニメーションのステートがLocomotionの最中のみジャンプできる
            if (currentBaseState.nameHash == idleState)
            {
                //ステート遷移中でなかったらジャンプできる
                if (!anim.IsInTransition(0))
                {
                    anim.SetBool("Jump", true);     // Animatorにジャンプに切り替えるフラグを送る
                    LRjumpFlag = true;
                    //  補間開始処理
                    Lerp();

                    //  補間処理
                    Vector3 pos = transform.position;
                    pos.y += jumpHeight;

                    //  現在のポジションが右側なら(少数の誤差を埋めるための+0.1f)
                    if (transform.position.x > CenterPosition.transform.position.x + 0.1f)
                    {
                        endPosition = new Vector3(CenterPosition.transform.position.x, pos.y, 3.5f);
                    }
                    //  現在のポジションが中央なら
                    else
                    {
                        endPosition = new Vector3(LeftPosition.transform.position.x, pos.y, 3.5f);
                    }

                    //  ジャンプ時の中間座標
                    Vector3 centerPos =  CenterPointTwoObj(transform.position, endPosition);
                    jumpCenterPos = new Vector3(centerPos.x, centerPos.y + 3.0f, centerPos.z);
                    lerpflag = true;
                }
            }
        }
        //  ボタンのフラグをもとに戻す
        LeftButtonFlag = false;
    }

    /****************************************************************
    *|　機能　右ボタン押下時に行う処理
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    void PressButtonRight()
    {
        //  プレイヤーが右側にいるときはジャンプを行わない
        if (transform.position.x < RightPosition.transform.position.x)
        {
            //アニメーションのステートがLocomotionの最中のみジャンプできる
            if (currentBaseState.nameHash == idleState)
            {
                //ステート遷移中でなかったらジャンプできる
                if (!anim.IsInTransition(0))
                {
                    //rb.AddForce(right * jumpPower, ForceMode.VelocityChange);
                    anim.SetBool("Jump", true);     // Animatorにジャンプに切り替えるフラグを送る
                    LRjumpFlag = true;
                    //  補間開始処理
                    Lerp();

                    //  補間処理
                    Vector3 pos = transform.position;
                    pos.y += jumpHeight;

                    //  現在のポジションが左側なら(少数の誤差を埋めるための-0.1f)
                    if (transform.position.x < CenterPosition.transform.position.x - 0.1f)
                    {
                        endPosition = new Vector3(CenterPosition.transform.position.x, pos.y, 3.5f);
                    }
                    else
                    {
                        endPosition = new Vector3(RightPosition.transform.position.x, pos.y, 3.5f);
                    }

                    //  ジャンプ時の中間座標
                    Vector3 centerPos = CenterPointTwoObj(transform.position, endPosition);
                    jumpCenterPos = new Vector3(centerPos.x, centerPos.y + 3.0f, centerPos.z);
                    lerpflag = true;
                }
            }

        }
        //  ボタンのフラグをもとに戻す
        RightButtonFlag = false;
    }

    /****************************************************************
    *|　機能　二つのオブジェクトの中心
    *|　引数　なし
    *|　戻値　中心座標
    ***************************************************************/
    public Vector3 CenterPointTwoObj(Vector3 pos1, Vector3 pos2)
    {
        Vector3 pos;
        pos.x = (pos1.x + pos2.x) / 2;
        pos.y = (pos1.y + pos2.y) / 2;
        pos.z = (pos1.z + pos2.z) / 2;

        return pos;
    }

    /****************************************************************
    *|　機能　ジャンプ可能かどうかのフラグの取得
    *|　引数　なし
    *|　戻値　フラグ(Bool)
    ***************************************************************/
    public bool GetJumpPossibleFlag()
    {
        return jumpPossibleFlag;
    }

    /****************************************************************
    *|　機能　全ボタンのフラグの初期化
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    private void ButtonFlagReset()
    {
        //  それぞれのボタンの押下状態を戻す
        CenterButtonFlag = false;
        LeftButtonFlag = false;
        RightButtonFlag = false;
    }

    /****************************************************************
    *|　機能　Raycastの当たり判定処理
    *|　引数　なし
    *|　戻値　bool(当たっていればtrue、当たっていなければfalse)
    ***************************************************************/
    bool RaycastFloor()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);

        RaycastHit hit;

        //  当たっているかどうかの判定
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            return true;
        }
        return false;
    }

    /****************************************************************
    *|　機能　プレイヤーが障害物にあたったかどうかのフラグの取得
    *|　引数　なし
    *|　戻値　bool(当たっていればtrue、当たっていなければfalse)
    ***************************************************************/
    public bool GetObstacleFlag()
    {
        return obstacleFlag;
    }

    /****************************************************************
    *|　機能　プレイヤーが死んだときのフラグ
    *|　引数　なし
    *|　戻値　bool(死んだときにtrue、生きていればfalse)
    ***************************************************************/
    public bool GetDeadFlag()
    {
        return deadFlag;
    }

    public GameObject[] buttons;//ジャンプボタン
    private Vector3[] FirstPos=new Vector3[3];
    private const int MAX_BUTTON = 3;//ジャンプボタンの最大数
    bool _once = false;//1フレームだけに制御するフラグ
    [SerializeField,Header("ボタンの反転時間")]
    private int FilipNum;
    float _Time = 0;//タイマー
        /****************************************************************
    *|　機能　特殊障害物に当たった時
    *|　引数　なし
    *|　戻値　なし
    ***************************************************************/
    private void FilipButton()
    {
        _Time+=Time.deltaTime;
        int second = (int)_Time % 60;//秒.timeを60で割った余り.
        if (!_once)//1フレームだけに制御する。
        {
            for (int i = 0; i < MAX_BUTTON; i++)
            {
                //ボタンの初期座標を保存
                FirstPos[i] = buttons[i].transform.position;
            }
            //ボタンの反転
            buttons[0].transform.position = FirstPos[2];
            buttons[2].transform.position = FirstPos[0];
            _once = true;
        }
        //反転終了時間を超えたら
        if(second>FilipNum)
        {
            for (int i = 0; i < MAX_BUTTON; i++)
            {
                //ボタンの座標を初期化
                buttons[i].transform.position = FirstPos[i];
                isFilipEvent = false;
                _once = false;
                _Time = 0;
            }
        }
    }

    float NoizeTimer;//ノイズの時間
    const int EndNoize = 5;//ノイズの終了時間
    private void NoizeEye()
    {
        NoizeTimer += Time.deltaTime;
        int second = (int)NoizeTimer % 60;//秒.timeを60で割った余り.
        Camera.main.GetComponent<CRT>().enabled = true;

        if (second > EndNoize)
        {
            Camera.main.GetComponent<CRT>().enabled = false;

            NoizeTimer = 0;
            isNoizeEye = false;
        }
    }
    
}

