using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseController : MonoBehaviour
{

    [SerializeField]
    private Animator anim; // 対象のAnimatorコンポーネント

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
            int hash = Animator.StringToHash(pose);//ポーズ名
            anim.Play(hash, -1, 0);//ハッシュ、レイヤー、正規化された時間(0-1)
        }
    }
}
