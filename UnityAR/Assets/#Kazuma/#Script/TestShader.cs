using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : MonoBehaviour {


	// Use this for initialization
	void Start () {

        gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
