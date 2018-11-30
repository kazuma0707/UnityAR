using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    public GameObject m_tapEffect;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        TouchInfo info=AppUtil.GetTouch();
        if(info==TouchInfo.Began)
        {
            Debug.Log(AppUtil.GetTouchWorldPosition(Camera.main));
            NewTapEffect(AppUtil.GetTouchPosition());
        }
        
		
	}
    void NewTapEffect(Vector3 pos)
    {
        
        Instantiate(m_tapEffect, pos, Quaternion.identity, transform);
    }
}
