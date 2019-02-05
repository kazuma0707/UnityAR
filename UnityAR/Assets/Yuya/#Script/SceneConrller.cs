﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneConrller : MonoBehaviour
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



    // Use this for initialization
    void Start()
    {
        // UI非表示
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        variable_cs = variable.GetComponent<Variable>();

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

    //----------------------------------------------------------------------
    //! @brief ノーマルボタンを押したときの処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void OnAppreciation()
    {
        FadeManager.Instance.LoadScene(SceneName.Appreciation, 2.0f);
    }

    //----------------------------------------------------------------------
    //! @brief パネルUIを非表示にする処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
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

        arButton.interactable = true;

        variable_cs.Active = !variable_cs.Active;
    }
}
