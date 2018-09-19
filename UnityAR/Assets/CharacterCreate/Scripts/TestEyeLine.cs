using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEyeLine : MonoBehaviour
{
    [SerializeField]
    private GameObject[] eyeLines;

    [SerializeField]
    private Text text;

    // Use this for initialization
    void Start ()
    {
        for(int i = 1; i < eyeLines.Length; i++)
        {
            eyeLines[i].GetComponent<Renderer>().enabled = false;
        }
        text.text = "1";

    }
	
	// Update is called once per frame
	void Update ()
    {
		// パターン１
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < eyeLines.Length; i++)
            {
                if(i == 0)
                {
                    eyeLines[i].GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    eyeLines[i].GetComponent<Renderer>().enabled = false;
                }
                
            }
            text.text = "1";
        }

        // パターン２
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < eyeLines.Length; i++)
            {
                if (i == 1)
                {
                    eyeLines[i].GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    eyeLines[i].GetComponent<Renderer>().enabled = false;
                }

            }

            text.text = "2";
        }

        // パターン３
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < eyeLines.Length; i++)
            {
                if (i == 2)
                {
                    eyeLines[i].GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    eyeLines[i].GetComponent<Renderer>().enabled = false;
                }

            }
            text.text = "3";
        }
    }
}
