//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   ModeChange.cs
//!
//! @brief  モード切替をするコード
//!
//! @date   2019/01/08 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ARModeChange : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    private ARGameManager gameManager_cs;

    // Use this for initialization
    void Start()
    {
        gameManager_cs = gameManager.GetComponent<ARGameManager>();
    }

    //----------------------------------------------------------------------
    //! @brief 
    //!
    //! @param[in]
    //!
    //! @return 
    //----------------------------------------------------------------------
    public void OnClick()
    {
        gameManager_cs.ModeFlag = !gameManager_cs.ModeFlag;
    }

}

