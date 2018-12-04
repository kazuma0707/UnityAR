using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{

    [Header("表示させたいボタン")]
    [SerializeField]
    private GameObject[] buttons;   // 対応するボタン

    private GameObject parent;    // 親オブジェクト
    private static bool active = false;  // アクティブ状態を管理


    [SerializeField]
    private GameObject variable;
    private Variable variable_cs;

    [SerializeField]
    private Button poseButton;


    [SerializeField]
    private Animator animator;
    private const string key_isPose = "isPose";
    private const string key_isMenu = "isMenu";
    private AnimatorStateInfo stateInfo;



    // Use this for initialization
    void Start()
    {
        // 親オブジェクトを取得
        parent = this.transform.parent.gameObject;

        variable_cs = variable.GetComponent<Variable>();
    }

    // Update is called once per frame
    void Update()
    {
        // アクティブ状態を設定
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(active);
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 子Buttonを非表示にする
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ActiveFalse()
    {

        // 対応するボタンを非表示にする
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        StartCoroutine(WaitAnim());

        // 子オブジェクトを検索
        ButtonScript[] child = parent.GetComponentsInChildren<ButtonScript>();
        foreach (ButtonScript obj in child)
        {
            // 子オブジェクトのタグがStatusButtonだったら表示設定をする
            if (obj.name != this.name) obj.Active = false;
        }

        // アクティブ状態を変える
        active = !active;

        variable_cs.Active = !variable_cs.Active;

        if (!variable_cs.Active && animator.GetBool(key_isPose))
        {
            animator.SetBool(key_isPose, variable_cs.Active);
            variable_cs.Active = true;
        }

        if (variable_cs.Active && animator.GetBool(key_isMenu))
        {
            variable_cs.Active = false;
        }


        Debug.Log("Menu" + variable_cs.Active);

        animator.SetBool(key_isMenu, variable_cs.Active);

    }

    // activeのアクセッサ
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }


    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 非アクティブになったときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    private void OnDisable()
    {
        // 非表示にする
        active = false;
        ActiveFalse();
    }

    public void NonActiveButton()
    {
        //animator.Update(0);
        //stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        ////if(stateInfo.IsName("")
        this.GetComponent<Button>().interactable = false;
        poseButton.interactable = false;
    }

    public void ActiveButton()
    {
        this.GetComponent<Button>().interactable = true;
        poseButton.interactable = true;
    }

    IEnumerator WaitAnim()
    {
        NonActiveButton();

        yield return new WaitForSeconds(1.5f);

        ActiveButton();
    }
}

