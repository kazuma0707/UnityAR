using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationButtonController : MonoBehaviour
{
    private bool isDisplay = false;
    [SerializeField]
    private GameObject VoiceRecButton;

    //----------------------------------------------------------------------
    //! @brief ボタンをクリックしたときの処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Onclick()
    {
        if (!isDisplay)
        {
            VoiceRecButton.SetActive(true);
            isDisplay = true;
        }
        else
        {
            VoiceRecButton.SetActive(false);
            isDisplay = false;
        }
    }
}
