//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   PoseController.cs
//!
//! @brief  ポーズをコントロールするスクリプト
//!
//! @date   2018/10/31 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseController : MonoBehaviour
{

    [SerializeField]
    private Animator anim; // 対象のAnimatorコンポーネント

    [Header("鑑賞用カメラ")]
    [SerializeField]
    private GameObject aCCamera;        // 鑑賞用カメラ
    [Header("鑑賞用カメラの位置番号")]
    [SerializeField]
    private ACCSetPosNum aCCSetPosNum;  // 鑑賞用カメラの位置番号   

    [SerializeField]
    private GameObject variable;
    private Variable variable_cs;


    private const string key_isPose1 = "Pose1";
    private const string key_isPose2 = "Pose2";
    private const string key_isPose3 = "Pose3";
    private const string key_isPose4 = "Pose4";
    private const string key_isPose5 = "Pose5";
    private const string key_isPose6 = "Pose6";
    private const string key_isPose7 = "Pose7";
    private const string key_isPose8 = "Pose8";
    private const string key_isPose9 = "Pose9";
    private const string key_isPose10 = "Pose10";


    void Start()
    {
        anim = GameObject.Find("skin").GetComponent<Animator>();

        variable_cs = variable.GetComponent<Variable>();

    }

    //----------------------------------------------------------------------
    //! @brief ポーズの変更処理
    //!
    //! @param[in] ポーズの名前
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void ChangePose(string pose)
    {
        AnimatorClipInfo clipInfo = anim.GetCurrentAnimatorClipInfo(0)[0];

        if (pose != clipInfo.clip.name)
        {
            //ポーズ名
            int hash = Animator.StringToHash(pose);

            //ハッシュ、レイヤー、正規化された時間(0-1)
            anim.Play(hash, -1, 0);
        }

        // キャラクリ用カメラとViewPointを特定の位置に移動させる
        aCCamera.GetComponent<AppreciationCameraCtr>().CameraSetPos(aCCSetPosNum);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ1の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose1_OnClick()
    {
        Debug.Log("AA");

        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;

        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose1, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ2の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose2_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }


        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose2, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ3の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose3_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose3, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ4の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose4_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose4, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ5の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose5_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose5, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ6の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose6_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose6, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ7の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose7_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose7, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ8の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose8_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose8, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ9の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose9_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            anim.SetBool(key_isPose10, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose9, variable_cs.Pose_Flag);

    }

    //----------------------------------------------------------------------
    //! @brief ポーズ10の変更処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Pose10_OnClick()
    {
        variable_cs.Pose_Flag = !variable_cs.Pose_Flag;


        // アニメーションのフラグ変更
        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose1))
        {
            anim.SetBool(key_isPose1, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose2))
        {
            anim.SetBool(key_isPose2, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose3))
        {
            anim.SetBool(key_isPose3, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose4))
        {
            anim.SetBool(key_isPose4, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose5))
        {
            anim.SetBool(key_isPose5, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose6))
        {
            anim.SetBool(key_isPose6, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose7))
        {
            anim.SetBool(key_isPose7, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose8))
        {
            anim.SetBool(key_isPose8, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (!variable_cs.Pose_Flag && anim.GetBool(key_isPose9))
        {
            anim.SetBool(key_isPose9, variable_cs.Pose_Flag);
            variable_cs.Pose_Flag = true;
        }

        if (variable_cs.Pose_Flag && anim.GetBool(key_isPose10))
        {
            variable_cs.Pose_Flag = false;
        }

        anim.SetBool(key_isPose10, variable_cs.Pose_Flag);

    }
}
