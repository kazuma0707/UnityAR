﻿//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   MenuButtonScript.cs 
//!
//! @brief  メニューボタンの操作スクリプト
//!
//! @date   2018/11/29 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{

    [Header("表示させたいオブジェクト")]
    [SerializeField]
    private GameObject[] objects;   // 対応するオブジェクト

    private GameObject parent;    // 親オブジェクト
    private static bool active = false;  // アクティブ状態を管理


    [SerializeField]
    private GameObject variable;
    private Variable variable_cs;

    [SerializeField]
    private Button poseButton;
    [SerializeField]
    private Button[] menuButtons;


    [SerializeField]
    private Animator animator;
    private const string key_isPose = "isPose";
    private const string key_isMenu = "isMenu";
    private AnimatorStateInfo stateInfo;



    //----------------------------------------------------------------------
    //! @brief Startメソッド
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        // 親オブジェクトを取得
        parent = this.transform.parent.gameObject;

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
    void Update()
    {
        // アクティブ状態を設定
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(active);
        }
    }

    //----------------------------------------------------------------------
    //! @brief 子オブジェクトを非表示にする
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void ActiveFalse()
    {

        // 対応するボタンを非表示にする
        for (int i = 0; i <objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    //----------------------------------------------------------------------
    //! @brief クリック処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void OnClick()
    {
        StartCoroutine(InteractiveButtonChange());

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

        // アニメーションのフラグ変更
        if (!variable_cs.Active && animator.GetBool(key_isPose))
        {
            animator.SetBool(key_isPose, variable_cs.Active);
            variable_cs.Active = true;
        }

        if (variable_cs.Active && animator.GetBool(key_isMenu))
        {
            variable_cs.Active = false;
        }


        animator.SetBool(key_isMenu, variable_cs.Active);

    }

    // activeのアクセッサ
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }


    //----------------------------------------------------------------------
    //! @brief 非アクティブになったときの処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void OnDisable()
    {
        // 非表示にする
        active = false;
        ActiveFalse();
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
        poseButton.interactable = false;
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].interactable = false;
        }

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
        poseButton.interactable = true;
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].interactable = true;
        }
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
}

