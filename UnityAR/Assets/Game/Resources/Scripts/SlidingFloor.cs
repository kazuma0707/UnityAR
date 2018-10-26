//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   SlidingFloor.cs
//!
//! @brief  すり抜け床親オブジェクト適用スクリプト
//!
//! @date   2018/10/05
//!
//! @author 加藤　竜哉
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  すり抜け床親オブジェクト適用スクリプト
public class SlidingFloor : MonoBehaviour {

    private BoxCollider floorCollider;　//   自身の当たり判定

    void Start()
    {
        floorCollider = this.GetComponent<BoxCollider>();
    }

    private void Update()
    {
       
    }

    public void OnChildTriggerEnter(Collider c)
    {
       
        if (c.gameObject.tag == "TriggerCollider")
        {
            floorCollider.isTrigger = true;
        }
    }

    public void OnChildTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "TriggerCollider")
        {
            floorCollider.isTrigger = false;
        }
    }

    public void OnTriggerOffEnter(Collider c)
    {
        if (c.gameObject.tag == "TriggerCollider")
        {
            //floorCollider.isTrigger = false;
        }
    }
    

}
