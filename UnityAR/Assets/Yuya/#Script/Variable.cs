//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   Variable.cs
//!
//! @brief  変数を管理するスクリプト
//!
//! @date   2018/12/4 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable : MonoBehaviour
{
    [SerializeField]
    private bool active;
    private bool pose_Flag;

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
        active = false;
        Pose_Flag = false;
	}
	

    // activeのアクセッサ
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }

    // ポーズフラグのアクセッサ
    public bool Pose_Flag
    {
        get { return pose_Flag; }
        set { pose_Flag = value; }
    }


}
