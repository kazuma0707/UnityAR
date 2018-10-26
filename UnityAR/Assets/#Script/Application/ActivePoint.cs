using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePoint : MonoBehaviour {
    [SerializeField]
    private GameObject PointCloud;
    int m_WaitCount=0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ScreenShot.isScreenShot)
        {
            PointCloud.SetActive(false);
            m_WaitCount++;
        }

        if (m_WaitCount / 60 > 3)
        {
            PointCloud.SetActive(true);
            m_WaitCount = 0;
            ScreenShot.isScreenShot = false;
        }
    }
}
