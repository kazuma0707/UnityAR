using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneConrller : MonoBehaviour
{

    // 確認パネル
    [SerializeField]
    private GameObject Panel;
    // はいボタン
    [SerializeField]
    private GameObject YesButton;
    // いいえボタン
    [SerializeField]
    private GameObject NoButton;


    // Use this for initialization
    void Start()
    {
        // UI非表示
        Panel.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
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
    //! @brief パネルを表示する処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void OnARPanel()
    {
        Panel.SetActive(true);
        YesButton.SetActive(true);
        NoButton.SetActive(true);
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
    }
}
