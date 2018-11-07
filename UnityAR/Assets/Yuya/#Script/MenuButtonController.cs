using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtonController : MonoBehaviour
{

    private bool isDisplay = false;
    [SerializeField]
    private GameObject MenuScrollView;


    //----------------------------------------------------------------------
    //! @brief クリック処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Onclick()
    {
        if (!isDisplay)
        {
            MenuScrollView.SetActive(true);
            isDisplay = true;
        }
        else
        {
            MenuScrollView.SetActive(false);
            isDisplay = false;
        }
    }

    //----------------------------------------------------------------------
    //! @brief シーンロード処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void CCSceneLoad()
    {
        SceneManager.LoadScene("CharCreate");
    }
}
