using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {
    private Text text;
    public static string SetText;

	// Use this for initialization
	void Start () {
        text = this.gameObject.GetComponent<Text>();
        text.text = "DebugText";
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = SetText;
		
	}
}
