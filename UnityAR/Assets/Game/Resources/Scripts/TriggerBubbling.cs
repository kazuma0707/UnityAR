//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   TriggerBubbling.cs
//!
//! @brief  すり抜け床 子オブジェクト適用スクリプト
//!
//! @date   2018/10/05
//!
//! @author 加藤　竜哉
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBubbling : MonoBehaviour
{
    private GameObject Parent;  //  親オブジェクト

    void Start()
    {
        Parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider c)
    {
        Parent.SendMessage("OnChildTriggerEnter", c);
    }

    private void OnTriggerExit(Collider c)
    {

        Parent.SendMessage("OnChildTriggerExit", c);
    }
}