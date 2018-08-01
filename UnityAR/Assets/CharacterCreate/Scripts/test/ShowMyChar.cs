using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShowMyChar : MonoBehaviour
{

    [SerializeField]
    private GameObject myChar;      // マイキャラ
   
    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        // タッチ又は左クリックされたら
        if ((Input.touchCount > 0) || (Input.GetMouseButtonDown(0)))
        {
            Debug.Log("create");
            // マイキャラ生成
            MyCharDataManager.Instance.CreateMyChar(myChar);
        }
	}
    
}
