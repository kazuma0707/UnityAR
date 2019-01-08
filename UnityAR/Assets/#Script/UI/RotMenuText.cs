using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotMenuText : MonoBehaviour {  
    [SerializeField]
    float RotNum = 0.0f;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.gameObject.transform.Rotate(new Vector3(0, 0, RotNum));


		
	}
}
