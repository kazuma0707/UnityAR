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


    void Start()
    {
        anim = GameObject.Find("skin").GetComponent<Animator>();
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
}
