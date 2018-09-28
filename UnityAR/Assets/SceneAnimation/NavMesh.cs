// MoveTo.cs
//*************************
//Unityマニュアルより引用
//*************************

using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    //目標としているオブジェクト
    private Transform target;

    void Start()
    {
        //NavMashの情報を取得
        NavMeshAgent enemy = GetComponent<NavMeshAgent>();

        //オブジェクトを目標の座標に移動させる
        //enemy.destination = target.position;
    }
}
