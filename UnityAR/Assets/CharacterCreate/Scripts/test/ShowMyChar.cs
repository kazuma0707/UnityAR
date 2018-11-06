using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ShowMyChar : MonoBehaviour
{   
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

            GameObject sotai = GameObject.Find("skin");
            // マイキャラ生成
            MyCharDataManager.Instance.ReCreate(sotai);
            // シーン遷移
            //SceneManager.LoadScene("CharCreate");
        }
    }
    
}
