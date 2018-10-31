using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseController : MonoBehaviour
{

    public Animator anim; // 対象のAnimatorコンポーネント

    public void ChangePose(string pose)
    {
        int hash = Animator.StringToHash(pose);//ポーズ名
        anim.Play(hash, -1, 0);//ハッシュ、レイヤー、正規化された時間(0-1)
    }
}
