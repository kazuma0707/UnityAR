using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable : MonoBehaviour
{
    [SerializeField]
    private bool active;

	// Use this for initialization
	void Start ()
    {
        active = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // activeのアクセッサ
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }

}
