//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   VoiceRec_ButtonScript.cs
//!
//! @brief  音声録音ボタンの処理スクリプト
//!
//! @date   2018/8/7 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceRec_ButtonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject m_obj;
    [SerializeField]
    private WatsonConversation m_watson;

    [SerializeField]
    private GameObject text;

    [SerializeField]
    private Sprite rec_On;
    [SerializeField]
    private Sprite rec_Off;


    private bool m_flag;

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
        m_obj = GameObject.FindWithTag("Player");
        m_watson = m_obj.GetComponent<WatsonConversation>();
        m_flag = true;
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
        if(m_watson == null)
        {
            m_watson = m_obj.GetComponent<WatsonConversation>();
        }
        if (m_flag)
        m_watson.SetVoiceRecFlag(true);
        text.SetActive(true);
        m_flag = false;
        StartCoroutine(Flag());
        StartCoroutine(CahngeSprite());
    }

    //----------------------------------------------------------------------
    //! @brief フラグ処理用
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    IEnumerator Flag()
    {
        yield return new WaitForSeconds(5.0f);
        m_flag = true;
    }


    //----------------------------------------------------------------------
    //! @brief イメージの変更
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    IEnumerator CahngeSprite()
    {
        this.gameObject.GetComponent<Image>().sprite = rec_On;
        yield return new WaitForSeconds(2.0f);
        this.gameObject.GetComponent<Image>().sprite = rec_Off;
    }
}
