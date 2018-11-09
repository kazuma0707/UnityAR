using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMyCharInGame : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GameObject sotai = GameObject.Find("skin");
        // マイキャラ生成
        MyCharDataManager.Instance.ReCreate(sotai);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
