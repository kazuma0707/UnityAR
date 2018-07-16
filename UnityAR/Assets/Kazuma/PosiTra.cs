using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class PosiTra : MonoBehaviour {

    private Vector3 OrigPos;

	// Use this for initialization
	void Start () {
        OrigPos = gameObject.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
        //if(GoogleARCore== TrackingState.Tracking)
        {
            var pos = Frame.Pose;
            gameObject.transform.position = pos.position + OrigPos;

        }

		
	}
}
