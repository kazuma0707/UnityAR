using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOff : MonoBehaviour {

    private GameObject Parent;

	// Use this for initialization
	void Start () {
        Parent = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Parent.SendMessage("OnTriggerOffEnter", other);
    }
}
