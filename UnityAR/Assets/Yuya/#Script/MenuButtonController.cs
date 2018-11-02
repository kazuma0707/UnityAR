using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtonController : MonoBehaviour
{

    private bool isDisplay = false;
    [SerializeField]
    private GameObject MenuScrollView;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Onclick()
    {
        if (!isDisplay)
        {
            MenuScrollView.SetActive(true);
            isDisplay = true;
        }
        else
        {
            MenuScrollView.SetActive(false);
            isDisplay = false;
        }
    }

    public void CCSceneLoad()
    {
        SceneManager.LoadScene("CharCreate");
    }
}
