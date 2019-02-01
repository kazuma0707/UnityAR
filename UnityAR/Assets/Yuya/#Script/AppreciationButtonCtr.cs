using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppreciationButtonCtr : MonoBehaviour
{
    [Header("管理するオブジェクト")]
    [SerializeField]
    private Button[] menuButtons;   // 対応するオブジェクト
    [SerializeField]
    private Button[] poseButtons;   // 対応するオブジェクト
    [SerializeField]
    private Button[] convButtons;   // 対応するオブジェクト
    [SerializeField]
    private Button arButton;

    // 確認パネル
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

    [SerializeField]
    private Animator animator;
    private const string key_isPose = "isPose";
    private const string key_isMenu = "isMenu";
    private const string key_isConv = "isConv";
    private AnimatorStateInfo stateInfo;


    // Use this for initialization
    void Start()
    {
        variable_cs = variable.GetComponent<Variable>();

        // UI非表示
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnMenuClick()
    {
        StartCoroutine(InteractiveButtonChange());

        variable_cs.Active = !variable_cs.Active;

        // アニメーションのフラグ変更
        if (!variable_cs.Active && animator.GetBool(key_isPose))
        {
            animator.SetBool(key_isPose, variable_cs.Active);
            variable_cs.Active = true;
        }
        if (!variable_cs.Active && animator.GetBool(key_isConv))
        {
            animator.SetBool(key_isConv, variable_cs.Active);
            variable_cs.Active = true;
        }

        if (variable_cs.Active && animator.GetBool(key_isMenu))
        {
            variable_cs.Active = false;
        }

        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        animator.SetBool(key_isMenu, variable_cs.Active);
    }

    public void OnPoseClick()
    {
        StartCoroutine(InteractiveButtonChange());

        variable_cs.Active = !variable_cs.Active;

        // アニメーションのフラグ変更
        if (!variable_cs.Active && animator.GetBool(key_isMenu))
        {
            animator.SetBool(key_isMenu, variable_cs.Active);
            variable_cs.Active = true;
        }
        if (!variable_cs.Active && animator.GetBool(key_isConv))
        {
            animator.SetBool(key_isConv, variable_cs.Active);
            variable_cs.Active = true;
        }

        if (variable_cs.Active && animator.GetBool(key_isPose))
        {
            variable_cs.Active = false;
        }

        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        animator.SetBool(key_isPose, variable_cs.Active);
    }

    public void OnConvClick()
    {
        StartCoroutine(InteractiveButtonChange());

        variable_cs.Active = !variable_cs.Active;

        // アニメーションのフラグ変更
        if (!variable_cs.Active && animator.GetBool(key_isMenu))
        {
            animator.SetBool(key_isMenu, variable_cs.Active);
            variable_cs.Active = true;
        }
        if (!variable_cs.Active && animator.GetBool(key_isPose))
        {
            animator.SetBool(key_isPose, variable_cs.Active);
            variable_cs.Active = true;
        }

        if (variable_cs.Active && animator.GetBool(key_isConv))
        {
            variable_cs.Active = false;
        }

        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        animator.SetBool(key_isConv, variable_cs.Active);
    }


    //----------------------------------------------------------------------
    //! @brief ボタンのinteractableをFalseにする処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void NonActiveButton()
    {
        this.GetComponent<Button>().interactable = false;
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].interactable = false;
        }

        for (int i = 0; i < poseButtons.Length; i++)
        {
            poseButtons[i].interactable = false;
        }

        for (int i = 0; i < convButtons.Length; i++)
        {
            convButtons[i].interactable = false;
        }

        arButton.interactable = false;
    }

    //----------------------------------------------------------------------
    //! @brief ボタンのinteractableをTrueにする処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void ActiveButton()
    {
        this.GetComponent<Button>().interactable = true;
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

        arButton.interactable = true ;
    }

    //----------------------------------------------------------------------
    //! @brief ボタンのinteractableを変更するコルーチン
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    IEnumerator InteractiveButtonChange()
    {
        NonActiveButton();

        yield return new WaitForSeconds(0.8f);

        ActiveButton();
    }

    public void OnARPanel()
    {
        variable_cs.Active = !variable_cs.Active;

        // アニメーションのフラグ変更
        if (!variable_cs.Active && animator.GetBool(key_isPose))
        {
            animator.SetBool(key_isPose, variable_cs.Active);
            variable_cs.Active = true;
        }
        if (!variable_cs.Active && animator.GetBool(key_isMenu))
        {
            animator.SetBool(key_isMenu, variable_cs.Active);
            variable_cs.Active = true;
        }
        if (!variable_cs.Active && animator.GetBool(key_isConv))
        {
            animator.SetBool(key_isConv, variable_cs.Active);
            variable_cs.Active = true;
        }

        Panel.SetActive(true);
        YesButton.SetActive(true);
        NoButton.SetActive(true);

        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].interactable = false;
        }

        for (int i = 0; i < poseButtons.Length; i++)
        {
            poseButtons[i].interactable = false;
        }

        for (int i = 0; i < convButtons.Length; i++)
        {
            convButtons[i].interactable = false;
        }
    }

}
